using Capa.Backend.Repositories.Intefaces;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.UnitsOfWork.Implementations
{
    public class DepartmentsUnitOfWork : GenericUnitOfWork<Department>, IDepartmentsUnitOfWork
    {
        private readonly IDepartmentsRepository _departmentsRepository;

        public DepartmentsUnitOfWork(IGenericRepository<Department> repository, IDepartmentsRepository departmentsRepository) : base(repository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public async Task<IEnumerable<Department>> GetComboAsync() => await _departmentsRepository.GetComboAsync();

        public override async Task<ActionResponse<IEnumerable<Department>>> GetAsync() => await _departmentsRepository.GetAsync();

        public override async Task<ActionResponse<Department>> GetAsync(int id) => await _departmentsRepository.GetAsync(id);
    }
}
