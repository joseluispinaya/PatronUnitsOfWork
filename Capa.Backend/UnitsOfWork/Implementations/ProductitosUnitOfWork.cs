using Capa.Backend.DTOas;
using Capa.Backend.Repositories.Intefaces;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.UnitsOfWork.Implementations
{
    public class ProductitosUnitOfWork : IProductitosUnitOfWork
    {
        private readonly IProductitosRepository _productitosRepository;
        public ProductitosUnitOfWork(IProductitosRepository productitosRepository)
        {
            _productitosRepository = productitosRepository;
        }

        public async Task<ActionResponse<Product>> AddAsync(ProductDTO productDTO) => await _productitosRepository.AddAsync(productDTO);

        public async Task<ActionResponse<Product>> GetAsync(int id) => await _productitosRepository.GetAsync(id);

        public async Task<ActionResponse<IEnumerable<Product>>> GetAsync() => await _productitosRepository.GetAsync();

        public async Task<ActionResponse<IEnumerable<ListProductDTO>>> GetListAsync(PaginationDTO pagination) => await _productitosRepository.GetListAsync(pagination);

        public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _productitosRepository.GetTotalRecordsAsync(pagination);

        public async Task<ActionResponse<Product>> UpdateAsync(ProductDTO productDTO) => await _productitosRepository.UpdateAsync(productDTO);
    }
}
