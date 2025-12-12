using Capa.Backend.Data;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : GenericController<Department>
    {
        private readonly IDepartmentsUnitOfWork _departmentsUnitOfWork;

        public DepartmentsController(IGenericUnitOfWork<Department> unitOfWork, IDepartmentsUnitOfWork departmentsUnitOfWork) : base(unitOfWork)
        {
            _departmentsUnitOfWork = departmentsUnitOfWork;
        }

        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _departmentsUnitOfWork.GetComboAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _departmentsUnitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var action = await _departmentsUnitOfWork.GetAsync(id);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

    }
}
