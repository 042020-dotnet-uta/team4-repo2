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
        public async Task<ActionResult<SheetModel>> GetSheets(Guid? id)
        {
            try
            {
                // Find by id.
                if (id != null)
                    return Ok(await _service.GetSheet(id));
                return NotFound();
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

        [HttpPut("{id}")]
        public async Task<ActionResult<SheetModel>> UpdateSheet(Guid? id, SheetModel sheetModel)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                    return BadRequest();
                sheetModel.SheetId = (Guid)id;
                try
                {
                    return await _service.UpdateSheet(sheetModel);
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSheet(Guid? id)
        {
            if (id == null)
                return BadRequest();
            try
            {
                await _service.DeleteSheet(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
