using Capa.Backend.Data;
using Capa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DataContext _context;

        public DepartmentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return Ok(department);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.Message.Contains("IX_Departments_Name"))
                {
                    return BadRequest($"El departamento '{department.Name}' ya existe.");
                }

                return BadRequest("Error al guardar el departamento.");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostAsync(Department department)
        //{
        //    _context.Departments.Add(department);
        //    await _context.SaveChangesAsync();
        //    return Ok(department);
        //}
    }
}
