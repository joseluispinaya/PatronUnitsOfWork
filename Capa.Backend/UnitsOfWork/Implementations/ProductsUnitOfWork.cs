using Capa.Backend.DTOas;
using Capa.Backend.Repositories.Intefaces;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.UnitsOfWork.Implementations
{
    public class ProductsUnitOfWork : GenericUnitOfWork<Product>, IProductsUnitOfWork
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsUnitOfWork(IGenericRepository<Product> repository, IProductsRepository productsRepository) : base(repository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ActionResponse<Product>> AddAsync(ProductDTO productDTO) => await _productsRepository.AddAsync(productDTO);

        public override async Task<ActionResponse<Product>> GetAsync(int id) => await _productsRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync() => await _productsRepository.GetAsync();

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination) => await _productsRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _productsRepository.GetTotalRecordsAsync(pagination);


        //public Task<ActionResponse<Product>> UpdateAsync(ProductDTO productDTO)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
