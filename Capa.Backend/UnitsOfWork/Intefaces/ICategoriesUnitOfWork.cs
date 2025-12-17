using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.UnitsOfWork.Intefaces
{
    public interface ICategoriesUnitOfWork
    {
        Task<ActionResponse<Category>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Category>>> GetAsync();

        Task<IEnumerable<Category>> GetComboAsync();

        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
