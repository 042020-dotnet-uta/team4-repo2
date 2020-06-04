using System;
using System.Collections.Generic;
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
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TemplateModel>>> GetTemplates()
        {
            try
            {
                return Ok(await _service.GetTemplates());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemplate(Guid? id)
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
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = Guid.Parse(identity.Claims.Where(claim => claim.Type == "Id").First().Value);
                templateModel = await _service.CreateTemplate(templateModel, (Guid)userId);
                return CreatedAtAction(nameof(GetTemplates), new { id = templateModel.TemplateId }, templateModel);
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion
    }
}