using Capa.Backend.Data;
using Capa.Backend.Helpers;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly DataContext _context;

        public CategoriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetComboAsync()
        {
            return await _context.Categories
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Categories
                .Include(x => x.Products)
                .AsQueryable();

            // if (!string.IsNullOrWhiteSpace(pagination.Filter))
            // {
            //     queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            // }

            return new ActionResponse<IEnumerable<Category>>
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
            var queryable = _context.Categories.AsQueryable();

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

        public override async Task<ActionResponse<Category>> GetAsync(int id)
        {
            var categories = await _context.Categories
                 .Include(x => x.Products)
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (categories == null)
            {
                return new ActionResponse<Category>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ActionResponse<Category>
            {
                WasSuccess = true,
                Result = categories
            };
        }

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync()
        {
            var categories = await _context.Categories
                .Include(x => x.Products)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = true,
                Result = categories
            };
        }

    }
}
