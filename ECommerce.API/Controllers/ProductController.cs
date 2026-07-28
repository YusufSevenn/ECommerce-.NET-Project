using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Core.DTOs;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateDto productCreateDto)
        {
            var createdProduct = await _productService.CreateProductAsync(productCreateDto);


            if (createdProduct != null)
            {
                return Ok(createdProduct);
            }
            return BadRequest("Ürün eklenirken bir hata oluştu.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var productDto = await _productService.GetProductByIdAsync(id);

            if (productDto == null) return NotFound("Ürün bulunamadı.");

            return Ok(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);


        }
    }
}