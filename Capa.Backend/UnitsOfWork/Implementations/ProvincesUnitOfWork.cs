using Capa.Backend.Repositories.Intefaces;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

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

        public override async Task<ActionResponse<IEnumerable<Province>>> GetAsync(PaginationDTO pagination) => await _provincesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _provincesRepository.GetTotalRecordsAsync(pagination);
    }
}
