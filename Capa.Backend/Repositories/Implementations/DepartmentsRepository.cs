using Capa.Backend.Data;
using Capa.Backend.Helpers;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class DepartmentsRepository : GenericRepository<Department>, IDepartmentsRepository
    {
        private readonly DataContext _context;

        public DepartmentsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Department>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Departments
                .Include(c => c.Provinces)
                .AsQueryable();

            // if (!string.IsNullOrWhiteSpace(pagination.Filter))
            // {
            //     queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            // }

            return new ActionResponse<IEnumerable<Department>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<Department>> GetComboAsync()
        {
            return await _context.Departments
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<Department>>> GetAsync()
        {
            var departments = await _context.Departments
            .Include(x => x.Provinces)
            .ToListAsync();
            return new ActionResponse<IEnumerable<Department>>
            {
                WasSuccess = true,
                Result = departments
            };
        }

        public override async Task<ActionResponse<Department>> GetAsync(int id)
        {
            var department = await _context.Departments
            .Include(x => x.Provinces)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (department == null)
            {
                return new ActionResponse<Department>
                {
                    Message = "Registro no encontrado"
                };
            }
            return new ActionResponse<Department>
            {
                WasSuccess = true,
                Result = department
            };
        }
    }
}
