using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBusinessService _businessService;
        private readonly IAccountService _accountService;
        public IConfiguration _configuration;

        public AccountController(ILogger<AccountController> logger, IBusinessService service, IAccountService account, IConfiguration configuration)
        {
            this._logger = logger;
            this._businessService = service;
            this._accountService = account;
            this._configuration = configuration;
        }

        #region POST
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> CreateUserLocal(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userModel = await _accountService.RegisterLocal(userModel);
                    return Ok(userModel);
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> UserLoginLocal(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userLogin = await _accountService.LoginLocal(userModel);

                    // Log in successful.
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", userLogin.UserId.ToString()),
                        new Claim("Username", userLogin.Username),
                        new Claim("Email", userLogin.Email)
                    };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion
    }
}
