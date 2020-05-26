using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CharSheet.Data;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateModel>> GetTemplate(Guid? id, Guid? userId = null)
        {
            try
            {
                // No user id query.
                if (userId == null)
                    return Ok(await _service.GetTemplate(id));

                // User id query.
                var templateModels = await _service.GetTemplates(userId);
                if (id == null)
                    return Ok(templateModels);
                return Ok(templateModels.Where(template => template.UserId == userId));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TemplateModel>> CreateTemplate(TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    templateModel = await _service.CreateTemplate(templateModel);
                    return CreatedAtAction(nameof(GetTemplate), new { id = templateModel.TemplateId }, templateModel);
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
