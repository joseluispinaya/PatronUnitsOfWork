using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface IDepartmentsRepository
    {
        Task<ActionResponse<IEnumerable<Department>>> GetAsync(PaginationDTO pagination);
        //Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
        Task<IEnumerable<Department>> GetComboAsync();

        Task<ActionResponse<Department>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Department>>> GetAsync();
    }
}
