using System;
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
        [AllowAnonymous]
        public async Task<ActionResult<TemplateModel>> GetTemplates(Guid? id)
        {
            try
            {
                return Ok(await _service.GetTemplate(id));
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<TemplateModel>> CreateTemplate(TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = Guid.Parse(User.FindFirst("Id").Value);
                    _logger.LogInformation(userId.ToString());
                    templateModel = await _service.CreateTemplate(templateModel, userId);
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