using Capa.Backend.Data;
using Capa.Backend.Helpers;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
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

        public override async Task<ActionResponse<IEnumerable<Province>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Provinces
                .Where(x => x.Department!.Id == pagination.Id)
                .AsQueryable();

            // if (!string.IsNullOrWhiteSpace(pagination.Filter))
            // {
            //     queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            // }

            return new ActionResponse<IEnumerable<Province>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
        {
            var queryable = _context.Provinces
                .Where(x => x.Department!.Id == pagination.Id)
                .AsQueryable();

            // if (!string.IsNullOrWhiteSpace(pagination.Filter))
            // {
            //     queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            // }

            double count = await queryable.CountAsync();
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
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
