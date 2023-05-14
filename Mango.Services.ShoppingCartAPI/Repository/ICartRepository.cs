﻿using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartByUserIdAsync(string userId);

        Task<CartDto> CreateUpdateCartAsync(CartDto cartDto);

        Task<bool> RemoveFromCartAsync(long cartDetailsId);

        Task<bool> ClearCartAsync(string UserId);

        Task<bool> ApplyCouponAsync(string userId, string couponCode);
        Task<bool> RemoveCouponAsync(string userId);
    }
}
