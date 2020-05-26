using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<TemplateModel>> GetTemplates(Guid? id, Guid? userId = null)
        {
            try
            {
                // Invalid GET request.
                if (userId == null && id == null)
                    return BadRequest();

                // Find by id.
                if (id != null)
                    return Ok(await _service.GetTemplate(id));
                
                // User id query.
                return Ok(await _service.GetTemplates(userId));
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
