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
    public class SheetsController : ControllerBase
    {
        private readonly ILogger<SheetsController> _logger;
        private readonly IBusinessService _service;

        public SheetsController(ILogger<SheetsController> logger, IBusinessService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region Action Methods
        [HttpGet("{id}")]
        public async Task<ActionResult<SheetModel>> GetSheets(Guid? id, Guid? userId = null)
        {
            try
            {
                // Invalid GET request.
                if (userId == null && id == null)
                    return BadRequest();

                // Find by id.
                if (id != null)
                    return Ok(await _service.GetSheet(id));

                // User id query.
                return Ok(await _service.GetSheets(userId));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<SheetModel>> CreateSheet(SheetModel sheetModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    sheetModel = await _service.CreateSheet(sheetModel);
                    return CreatedAtAction(nameof(GetSheets), new { id = sheetModel.SheetId }, sheetModel);
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
