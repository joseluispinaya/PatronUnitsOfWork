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

        public DepartmentsController(IGenericUnitOfWork<Department> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
