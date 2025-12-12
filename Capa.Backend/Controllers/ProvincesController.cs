using Capa.Backend.UnitsOfWork.Intefaces;
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

    }
}
