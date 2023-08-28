﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.CORE.DTO;
using Project2_CMPG323.CORE.Services;

namespace Project2_CMPG323.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //https://localhost:1234/api/Products/Get/All
        [HttpGet]
        [Route("/api/Products/Get/All")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        //https://localhost:1234/api/Products/Get/All/{id}
        [HttpGet]
        [Route("/api/Products/Get/All/{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] short id)
        {
            var productDTO = await _productService.GetProduct(id);

            if (productDTO is null)
            {
                return BadRequest();
            }

            return Ok(productDTO);
        }

        //https://localhost:1234/api/Products/CreateProduct
        [HttpPost]
        [Route("/api/Products/CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreatProductDTO createProductDTO)
        {
            var createdProductDTO = await _productService.CreateProduct(createProductDTO);

            return CreatedAtAction(nameof(GetProduct), new { id = createdProductDTO.ProductId }, createdProductDTO);
        }
    }
}
