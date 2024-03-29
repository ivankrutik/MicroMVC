﻿using Mango.Services.ShoppingCartAPI.Messages;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Repository;
using MessageBus.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IMessageProducer _messageProducer;
        protected ResponseDto _response;

        public CartAPIController(ICartRepository cartRepository, IMessageProducer messageProducer, ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository;
            _response = new ResponseDto();
            _messageProducer = messageProducer;
            _couponRepository = couponRepository;
        }

        [HttpGet("GetCart/{UserId}")]
        public async Task<object> GetCart(string UserId)
        {
            try
            {
                CartDto cart = await _cartRepository.GetCartByUserIdAsync(UserId);
                _response.Result = cart;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cartRepository.CreateUpdateCartAsync(cartDto);
                _response.Result = cart;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cartRepository.CreateUpdateCartAsync(cartDto);
                _response.Result = cart;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] long cartId)
        {
            try
            {
                _response.Result = await _cartRepository.RemoveFromCartAsync(cartId);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost("ClearCart")]
        public async Task<object> ClearCart([FromBody] string UserId)
        {
            try
            {
                _response.Result = await _cartRepository.ClearCartAsync(UserId);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                _response.Result = await _cartRepository.ApplyCouponAsync(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                _response.Result = await _cartRepository.RemoveCouponAsync(userId);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost("CheckOut")]
        public async Task<object> CheckOut(CheckOutHeaderDto checkOutHeader)
        {
            try
            {
                var cartDto = await _cartRepository.GetCartByUserIdAsync(checkOutHeader.UserId);
                if (cartDto == null)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(checkOutHeader.CouponCode))
                {
                    var coupon = await _couponRepository.GetCoupon(checkOutHeader.CouponCode);
                    if (coupon.CouponAmount != checkOutHeader.DiscountTotal)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string> { "Coupon price has changed" };
                        _response.DisplayMessage = "Coupon price has changed";
                        return _response;
                    }
                }

                checkOutHeader.CartDetails = cartDto.CartDetails;

                ///logic to add message
                _messageProducer.SendMessage(checkOutHeader);

                await _cartRepository.ClearCartAsync(checkOutHeader.UserId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
