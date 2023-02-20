using Mango.Services.ProductAPI.Models.Dto;
using System.Collections;

namespace Mango.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetProductByIdAsync(long productId);

        Task<ProductDto> CreateUpdateProductAsync(ProductDto product);

        Task<bool> DeleteProductAsync(long productId);
    }
}
