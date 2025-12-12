using Capa.Backend.Data;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class ProvincesRepository : GenericRepository<Province>, IProvincesRepository
    {
        private readonly DataContext _context;

        public ProvincesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Province>> GetComboAsync(int departmentId)
        {
            return await _context.Provinces
            .Where(s => s.DepartmentId == departmentId)
            .OrderBy(s => s.Name)
            .ToListAsync();
        }
    }
}
