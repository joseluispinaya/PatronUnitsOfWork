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
    public class ProductitosRepository : IProductitosRepository
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;

        public ProductitosRepository(DataContext context, IImageHelper imageHelper)
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

        public async Task<ActionResponse<Product>> GetAsync(int id)
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

        public async Task<ActionResponse<IEnumerable<Product>>> GetAsync()
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

        public async Task<ActionResponse<IEnumerable<ListProductDTO>>> GetListAsync(PaginationDTO pagination)
        {
            var queryable = _context.Products
                .Include(x => x.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
                    EF.Functions.Like(x.Name, $"%{pagination.Filter}%"));
            }

            var products = await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .Select(x => new ListProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Photo = x.Photo,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new ActionResponse<IEnumerable<ListProductDTO>>
            {
                WasSuccess = true,
                Result = products
            };
        }

        //public Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
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

        public async Task<ActionResponse<Product>> UpdateAsync(ProductDTO productDTO)
        {
            var product = await _context.Products.FindAsync(productDTO.Id);

            if (product == null)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            // Mantener imagen actual por defecto
            var imageUrl = product.Photo;

            // Procesar nueva imagen si existe
            if (productDTO.PhotoFile != null)
            {
                var newImageUrl = await _imageHelper.UploadImageAsync(productDTO.PhotoFile, "ImagesProd");

                if (!string.IsNullOrWhiteSpace(newImageUrl))
                {
                    if (!string.IsNullOrWhiteSpace(product.Photo))
                    {
                        await _imageHelper.DeleteImage(product.Photo, "ImagesProd");
                    }

                    imageUrl = newImageUrl;
                }
            }

            // Actualizar propiedades escalares
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.CategoryId = productDTO.CategoryId;
            product.Photo = imageUrl;
            product.IsActive = productDTO.IsActive;

            //_context.Update(product);

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
                    Message = "Ya existe un registro con los mismos datos."
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = ex.Message
                };
            }
        }

    }
}
