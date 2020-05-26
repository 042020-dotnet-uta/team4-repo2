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
        public async Task<ActionResult<TemplateModel>> GetTemplate(Guid? id)
        {
            try
            {
                return await _service.GetTemplate(id);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
