using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using CharSheet.Domain;
using CharSheet.Data;
using CharSheet.Api.Models;

namespace CharSheet.Api.Services
{
    public interface IUsersService
    {
        Task<UserModel> NewUser(UserModel userModel);
        Task<UserModel> VerifyLogin(UserModel userModel);
    }

    public class UsersService : IUsersService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<UsersService> _logger;

        public UsersService(ILogger<UsersService> logger, CharSheetContext context)
        {
            this._logger = logger;
            this._unitOfWork = new UnitOfWork(context);
        }

        public async Task<UserModel> NewUser(UserModel userModel)
        {
            // Check username and email are available.
            var check = (await _unitOfWork.UserRepository.Get(user => user.Username == userModel.Username || user.Email == userModel.Email)).FirstOrDefault();

            // Username is available.
            if (check == null)
            {

                var user = new User
                {
                    Username = userModel.Username,
                    Email = userModel.Email
                };

                // Password hashing.
                // Generate salt.
                byte[] salt = new byte[128 / 8];
                int iterationCount;
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                    iterationCount = RandomNumberGenerator.GetInt32(500000, 1000000);
                }

                // Hash password.
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userModel.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: iterationCount,
                    numBytesRequested: 256 / 8
                ));

                // Set Login properties.
                user.Login = new Login
                {
                    Salt = salt,
                    IterationCount = iterationCount,
                    Hashed = hashed
                };

                // Insert user into database.
                await _unitOfWork.UserRepository.Insert(user);
                await _unitOfWork.Save();

                _logger.LogInformation($"New User: {user.UserId} - {user.Username}.");

                return new UserModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email
                };
            } else {
                if (check.Username == userModel.Username) throw new InvalidOperationException("Username already exists.");
                else if (check.Email == userModel.Email) throw new InvalidOperationException("Email already exists.");
                else throw new InvalidOperationException("Unable to create new user.");
            }
        }

        public async Task<UserModel> VerifyLogin(UserModel userModel)
        {
            // Find usern by username.
            var user = (await _unitOfWork.UserRepository.Get(user => user.Username == userModel.Username, null, "Login")).ToList().FirstOrDefault();
            if (user != null)
            {
                // Hash password input.
                var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userModel.Password,
                    salt: user.Login.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: user.Login.IterationCount,
                    numBytesRequested: 256 / 8
                ));

                // Verify hashed password matches database.
                if (hashed == user.Login.Hashed)
                {
                    _logger.LogInformation($"Login: {user.UserId} - {user.Username}.");

                    return new UserModel
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email
                    };
                }
            }
            throw new InvalidOperationException("Matching username or password was not found.");
        }
    }
}