using Capa.Shared.Entities;

namespace Capa.Backend.UnitsOfWork.Intefaces
{
    public interface IProvincesUnitOfWork
    {
        Task<IEnumerable<Province>> GetComboAsync(int departmentId);
    }
}
