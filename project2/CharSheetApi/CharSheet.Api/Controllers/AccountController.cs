﻿using System;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CharSheet.Api.Models;
using CharSheet.Api.Services;

namespace CharSheet.Api.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private IConfiguration _configuration;
        public IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public AccountController(ILogger<AccountController> logger, IAccountService account, IConfiguration configuration)
        {
            this._logger = logger;
            this._accountService = account;
            this._configuration = configuration;
        }

        #region POST
        [HttpPost("Register")]
        public async Task<ActionResult<UserModel>> CreateUserLocal(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._logger.LogInformation("Validating model", userModel);
                    userModel = await _accountService.RegisterLocal(userModel);
                    return Ok(userModel);
                }
                catch
                {
                    this._logger.LogError("Invalid model", userModel);
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserModel>> UserLoginLocal(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userModel = await _accountService.LoginLocal(userModel);

                    // Log in successful.
                    return Ok(GetAccessToken(userModel));
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost("GoogleLogin")]
        public async Task<ActionResult<UserModel>> GoogleLogin(UserModel userModel)
        {
            try
            {
                userModel = await _accountService.LoginSocial(userModel);
                return Ok(GetAccessToken(userModel));
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region Helpers
        public string GetAccessToken(UserModel userModel)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", userModel.UserId.ToString()),
                new Claim("Username", userModel.Username),
                new Claim("Email", userModel.Email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
