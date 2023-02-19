using Mango.Services.ProductAPI.Models.Dto;
using System.Collections;

namespace Mango.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<ProductDto> GetProductById(long productId);

        Task<ProductDto> CreateUpdateProduct(ProductDto product);

        Task<bool> DeleteProduct(long productId);
    }
}
