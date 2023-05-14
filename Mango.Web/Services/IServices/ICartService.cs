﻿using Mango.Web.Models.CartDomain;

namespace Mango.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);

        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);

        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);

        Task<T> RemoveFromCartAsync<T>(long cartId, string token = null);

        Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token = null);

        Task<T> RemoveCouponAsync<T>(string userId, string token = null);
    }
}
