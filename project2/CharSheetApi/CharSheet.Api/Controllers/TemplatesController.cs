using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using CharSheet.Api.Services;
using CharSheet.Api.Models;

namespace CharSheet.Api.Controllers
{
    [ApiController]
    [Route("/api/templates")]
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
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Ok(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult<TemplateModel>> CreateTemplate(TemplateModel templateModel)
        {
            Guid? userId = null;
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                userId = Guid.Parse(identity.Claims.Where(claim => claim.Type == "Id").First().Value);
                templateModel = await _service.CreateTemplate(templateModel, (Guid) userId);
                return CreatedAtAction(nameof(GetTemplates), new { id = templateModel.TemplateId }, templateModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace, userId = userId.ToString() });
            }
        }
        #endregion
    }
}