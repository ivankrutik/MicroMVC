using Mango.Web.Models;
using Newtonsoft.Json.Linq;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetProductsAllAsync<T>(string token);

        Task<T> GetProductByIdAsync<T>(long productId, string token);

        Task<T> CreateProductAsync<T>(ProductDto product, string token);
        Task<T> UpdateProductAsync<T>(ProductDto product, string token);
        Task<T> DeleteProductAsync<T>(long productId, string token);
    }
}
