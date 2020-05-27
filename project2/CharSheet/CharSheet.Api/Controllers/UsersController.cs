using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CharSheet.Api.Models;
using CharSheet.Api.Services;

namespace CharSheet.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IBusinessService _service;

        public UsersController(ILogger<UsersController> logger, IBusinessService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region POST
        [HttpPost("")]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userModel = await _service.NewUser(userModel);
                    return Ok(userModel);
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
