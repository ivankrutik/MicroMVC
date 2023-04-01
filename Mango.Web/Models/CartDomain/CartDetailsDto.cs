﻿using Mango.Web.Models;

namespace Mango.Web.Models.CartDomain
{
    public class CartDetailsDto
    {
        public long CartDetailsId { get; set; }

        public long CartHeaderId { get; set; }

        public virtual CartHeaderDto CartHeader { get; set; }

        public long ProductId { get; set; }

        public virtual ProductDto Product { get; set; }

        public int Count { get; set; }
    }
}
