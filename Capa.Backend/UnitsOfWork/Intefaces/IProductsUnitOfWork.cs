using Capa.Backend.DTOas;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.UnitsOfWork.Intefaces
{
    public interface IProductsUnitOfWork
    {
        Task<ActionResponse<Product>> AddAsync(ProductDTO productDTO);

        //Task<ActionResponse<Product>> UpdateAsync(ProductDTO productDTO);

        Task<ActionResponse<Product>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Product>>> GetAsync();

        Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
