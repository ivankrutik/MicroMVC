using Mango.Web.Models;
using Mango.Web.Models.CartDomain;
using Mango.Web.Services.IServices;
namespace Mango.Web.Services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpClientFactory _clientFactory;
        public CartService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }

        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId,
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(long cartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
                AccessToken = token
            });
        }
    }
}
