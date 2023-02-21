using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProductAsync(ProductDto product)
        {
            var Product = _mapper.Map<ProductDto, Product>(product);
            if (Product.ProductID != 0)
            {
                var res = _dbContext.Products.Add(Product);
                Product = res.Entity;
            }
            else
            {
                _dbContext.Products.Update(Product);
            }
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(Product);
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == productId);
                if (product == null)
                {
                    return false;
                }
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(long productId)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            List<Product> products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
