using System;
using System.IdentityModel.Tokens;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using CharSheet.Api.Services;
using CharSheet.Api.Models;

namespace CharSheet.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ILogger<TemplatesController> _logger;
        private readonly IBusinessService _service;

        public TemplatesController(ILogger<TemplatesController> logger, IBusinessService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region Action Methods
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateModel>> GetTemplates(Guid? id)
        {
            try
            {
                return Ok(await _service.GetTemplate(id));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<TemplateModel>> CreateTemplate(TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = Guid.Parse(User.FindFirst("Id").Value);
                    templateModel = await _service.CreateTemplate(templateModel);
                    return CreatedAtAction(nameof(GetTemplates), new { id = templateModel.TemplateId }, templateModel);
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
