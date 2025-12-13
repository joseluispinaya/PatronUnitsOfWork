using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvincesController : GenericController<Province>
    {
        private readonly IProvincesUnitOfWork _provincessUnitOfWork;
        public ProvincesController(IGenericUnitOfWork<Province> unitOfWork, IProvincesUnitOfWork provincessUnitOfWork) : base(unitOfWork)
        {
            _provincessUnitOfWork = provincessUnitOfWork;
        }

        [HttpGet("combo/{departmentId:int}")]
        public async Task<IActionResult> GetComboAsync(int departmentId)
        {
            return Ok(await _provincessUnitOfWork.GetComboAsync(departmentId));
        }

        [HttpGet("paginated")]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _provincessUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalRecords")]
        public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _provincessUnitOfWork.GetTotalRecordsAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

    }
}
