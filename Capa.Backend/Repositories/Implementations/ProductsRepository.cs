using Capa.Backend.Data;
using Capa.Backend.DTOas;
using Capa.Backend.Helpers;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;

        public ProductsRepository(DataContext context, IImageHelper imageHelper) : base(context)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        public async Task<ActionResponse<Product>> AddAsync(ProductDTO productDTO)
        {

            var category = await _context.Categories.FindAsync(productDTO.CategoryId);
            if (category == null)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Categoria no encontrada"
                };
            }

            string photoPath = string.Empty;

            if (productDTO.PhotoFile != null)
            {
                photoPath = await _imageHelper.UploadImageAsync(productDTO.PhotoFile, "ImagesProd");
            }

            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                //Category = category,
                CategoryId = productDTO.CategoryId,
                Photo = photoPath,
                IsActive = productDTO.IsActive,
            };

            _context.Add(product);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Product>
                {
                    WasSuccess = true,
                    Result = product
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Ya existe el registro."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        //public Task<ActionResponse<Product>> UpdateAsync(ProductDTO productDTO)
        //{
        //    throw new NotImplementedException();
        //}

        public override async Task<ActionResponse<Product>> GetAsync(int id)
        {
            var product = await _context.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new ActionResponse<Product>
                {
                    Message = "Registro no encontrado"
                };
            }
            return new ActionResponse<Product>
            {
                WasSuccess = true,
                Result = product
            };
        }

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync()
        {
            var products = await _context.Products
            .Include(x => x.Category)
            .ToListAsync();
            return new ActionResponse<IEnumerable<Product>>
            {
                WasSuccess = true,
                Result = products
            };
        }

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Products
                .Include(x => x.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
                    EF.Functions.Like(x.Name, $"%{pagination.Filter}%"));
            }

            return new ActionResponse<IEnumerable<Product>>
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
            var queryable = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
                    EF.Functions.Like(x.Name, $"%{pagination.Filter}%"));
            }

            int count = await queryable.CountAsync();

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = count
            };
        }
    }
}
