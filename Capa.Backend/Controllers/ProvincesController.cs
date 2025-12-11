using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvincesController : GenericController<Province>
    {
        public ProvincesController(IGenericUnitOfWork<Province> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
