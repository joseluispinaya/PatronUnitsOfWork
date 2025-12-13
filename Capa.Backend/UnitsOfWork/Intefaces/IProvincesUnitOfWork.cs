using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.UnitsOfWork.Intefaces
{
    public interface IProvincesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Province>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

        Task<IEnumerable<Province>> GetComboAsync(int departmentId);
    }
}
