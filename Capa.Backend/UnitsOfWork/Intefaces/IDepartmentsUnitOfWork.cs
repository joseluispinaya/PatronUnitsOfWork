using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.UnitsOfWork.Intefaces
{
    public interface IDepartmentsUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Department>>> GetAsync(PaginationDTO pagination);

        Task<IEnumerable<Department>> GetComboAsync();

        Task<ActionResponse<Department>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Department>>> GetAsync();
    }
}
