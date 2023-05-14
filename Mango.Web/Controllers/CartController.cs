using Mango.Web.Models;
using Mango.Web.Models.CartDomain;
using Mango.Web.Models.CouponDomain;
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
        private readonly ICouponService _couponService;

        public CartController(ICartService cartService, IProductService productService, ICouponService couponService)
        {
            _cartService = cartService;
            _productService = productService;
            _couponService = couponService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var UserId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var responce = await _cartService.ApplyCouponAsync<ResponseDto>(cartDto, accessToken);
            if (responce != null && responce.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }


        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            var UserId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var responce = await _cartService.RemoveCouponAsync<ResponseDto>(cartDto.CartHeader.UserId, accessToken);
            if (responce != null && responce.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
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
                if (!string.IsNullOrWhiteSpace(cartDto.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCouponAsync<ResponseDto>(cartDto.CartHeader.CouponCode, accessToken);
                    if (coupon?.Result != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(coupon.Result));
                        cartDto.CartHeader.DiscountTotal = couponObj.CouponAmount;
                    }
                }

                cartDto.CartHeader.OrderTotal = cartDto.CartDetails.Sum(x => (x.Count * x.Product.Price));
                cartDto.CartHeader.OrderTotal = cartDto.CartHeader.OrderTotal - (cartDto.CartHeader.DiscountTotal??0);

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
