using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Domain.Entities;
using ProductManagement.API.Domain.Models;
using ProductManagement.API.Domain.Providers;
using ProductManagement.API.Domain.Services;


namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            await _productService.Insert(productDto);
            return Ok("Created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }
    }
}