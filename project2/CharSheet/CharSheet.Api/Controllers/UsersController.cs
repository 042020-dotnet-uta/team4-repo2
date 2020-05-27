using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CharSheet.Api.Models;
using CharSheet.Api.Services;

namespace CharSheet.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IBusinessService _service;
        private readonly IAccountService _account;

        public AccountController(ILogger<AccountController> logger, IBusinessService service, IAccountService account)
        {
            this._logger = logger;
            this._service = service;
            this._account = account;
        }

        #region POST
        [HttpPost("Register")]
        public async Task<ActionResult<UserModel>> CreateUserLocal(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userModel = await _account.RegisterLocal(userModel);
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
        public async Task<ActionResult<UserModel>> UserLoginLocal(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userLogin = await _account.LoginLocal(userModel);
                    return Ok(userLogin);
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
