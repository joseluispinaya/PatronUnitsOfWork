using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface IProvincesRepository
    {
        Task<ActionResponse<IEnumerable<Province>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

        Task<IEnumerable<Province>> GetComboAsync(int departmentId);
    }
}
