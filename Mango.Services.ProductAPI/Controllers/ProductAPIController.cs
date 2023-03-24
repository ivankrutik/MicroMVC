using Azure;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> GetAsync()
        {
            try
            {
                IEnumerable<ProductDto> products = await _productRepository.GetProductsAsync();
                _response.Result = products;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetAsync(long id)
        {
            try
            {
                var Product = await _productRepository.GetProductByIdAsync(id);
                _response.Result = Product;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize]
        public async Task<object> PostAsync([FromBody] ProductDto product)
        {
            try
            {
                var Product = await _productRepository.CreateUpdateProductAsync(product);
                _response.Result = Product;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize]
        public async Task<object> PutAsync([FromBody] ProductDto product)
        {
            try
            {
                var Product = await _productRepository.CreateUpdateProductAsync(product);
                _response.Result = Product;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> PutAsync(long id)
        {
            try
            {
                _response.Result = await _productRepository.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
