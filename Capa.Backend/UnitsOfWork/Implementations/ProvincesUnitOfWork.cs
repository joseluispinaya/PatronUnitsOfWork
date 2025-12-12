using Capa.Backend.Repositories.Intefaces;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.Entities;

namespace Capa.Backend.UnitsOfWork.Implementations
{
    public class ProvincesUnitOfWork : GenericUnitOfWork<Province>, IProvincesUnitOfWork
    {
        private readonly IProvincesRepository _provincesRepository;

        public ProvincesUnitOfWork(IGenericRepository<Province> repository, IProvincesRepository provincesRepository) : base(repository)
        {
            _provincesRepository = provincesRepository;
        }

        public async Task<IEnumerable<Province>> GetComboAsync(int departmentId) => await _provincesRepository.GetComboAsync(departmentId);
    }
}
