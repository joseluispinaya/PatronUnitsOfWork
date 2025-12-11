using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface IDepartmentsRepository
    {
        Task<IEnumerable<Department>> GetComboAsync();

        Task<ActionResponse<Department>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Department>>> GetAsync();
    }
}
