using Mango.Web.Models;
using Mango.Web.Models.CartDomain;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var UserId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var responce = await _cartService.GetCartByUserIdAsync<ResponseDto>(UserId, accessToken);
            var cartDto = new CartDto();
            if (responce != null && responce.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responce.Result));
            }
            if (cartDto?.CartHeader != null)
            {
                cartDto.CartHeader.OrderTotal = cartDto.CartDetails.Sum(x => (x.Count * x.Product.Price));
            }
            return cartDto;
        }

        public async Task<IActionResult> Remove(long cartDetailsId)
        {
            var UserId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var responce = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsId, accessToken);
            if (responce != null && responce.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
    }
}
