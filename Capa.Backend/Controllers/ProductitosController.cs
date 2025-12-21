using Capa.Backend.DTOas;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductitosController : ControllerBase
    {
        private readonly IProductitosUnitOfWork _productitosUnitOfWork;

        public ProductitosController(IProductitosUnitOfWork productsUnitOfWork)
        {
            _productitosUnitOfWork = productsUnitOfWork;
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginationDTO pagination)
        {
            var response = await _productitosUnitOfWork.GetListAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalRecords")]
        public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _productitosUnitOfWork.GetTotalRecordsAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _productitosUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var action = await _productitosUnitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostAsync([FromForm] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(errors);
            }

            var action = await _productitosUnitOfWork.AddAsync(productDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> PutAsync([FromForm] ProductDTO productDTO)
        {
            var action = await _productitosUnitOfWork.UpdateAsync(productDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }
    }
}
