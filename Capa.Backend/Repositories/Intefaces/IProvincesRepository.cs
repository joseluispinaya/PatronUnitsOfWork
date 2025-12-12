using Capa.Shared.Entities;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface IProvincesRepository
    {
        Task<IEnumerable<Province>> GetComboAsync(int departmentId);
    }
}
