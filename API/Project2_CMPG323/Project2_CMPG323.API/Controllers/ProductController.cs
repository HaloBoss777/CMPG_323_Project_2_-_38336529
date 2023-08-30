using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.API.Filter;
using Project2_CMPG323.CORE.DTO;
using Project2_CMPG323.CORE.Services;

namespace Project2_CMPG323.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Implement the Product Service (Dependency Ingection)
        public readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            //Injects only the Dependencys that are needed
            _productService = productService;
        }

        //Get all Products
        //https://localhost:1234/api/Products/Get/All
        [HttpGet]
        [Route("/api/Products/Get/All")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        //Get a Specific Product
        //https://localhost:1234/api/Products/Get/All/{id}
        [HttpGet]
        [Route("/api/Products/Get/All/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetProduct([FromRoute] short id)
        {
            var productDTO = await _productService.GetProductAsync(id);

            if (productDTO is null)
            {
                return BadRequest();
            }

            return Ok(productDTO);
        }

        //Create a Product
        //https://localhost:1234/api/Products/CreateProduct
        [HttpPost]
        [Route("/api/Products/CreateProduct")]
        [AuthorizationFilter]
        public async Task<IActionResult> CreateProduct([FromBody] CreatProductDTO createProductDTO)
        {
            var createdProductDTO = await _productService.CreateProductAsync(createProductDTO);

            return CreatedAtAction(nameof(GetProduct), new { id = createdProductDTO.ProductId }, createdProductDTO);
        }

        //Update a Specific Product
        //https://localhost:1234/api/Products/UpdateProduct/{id}
        [HttpPatch]
        [Route("/api/Products/UpdateProduct/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> UpdateProduct([FromRoute] short id, [FromBody] UpdateProductDTO updateProductDTO)
        {
            var updatedProduct = await _productService.UpdatedProductAsync(id, updateProductDTO);

            if (updatedProduct is null) 
            {
                return BadRequest();
            }

            return Ok(updatedProduct);
        }

        //Delete a Product
        //https://localhost1234/api/Products/DeleteProduct/{id}
        [HttpDelete]
        [Route("/api/Products/DeleteProduct/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> DeleteProduct([FromRoute] short id)
        {
            var deletedRecord = await _productService.DeleteProductAsync(id);

            if(deletedRecord is null)
            {
                return NotFound();
            }

            return Ok(deletedRecord);
        }
    }
}
