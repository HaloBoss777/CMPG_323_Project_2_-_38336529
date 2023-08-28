using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project2_CMPG323.CORE.DTO;
using Project2_CMPG323.CORE.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_CMPG323.CORE.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProduct(short id);
    }

    public class ProductService : IProductService
    {
        private readonly Project2Context _project2Context;

        public ProductService(Project2Context project2Context)
        {
            _project2Context = project2Context;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            return await _project2Context.Products.Select(x => new ProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductDescription = x.ProductDescription,
                UnitsInStock = x.UnitsInStock,

            }).ToListAsync();
        }

        public async Task<ProductDTO?> GetProduct(short id)
        {
            return await _project2Context.Products.Where(x => x.ProductId == id).Select(x => new ProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductDescription = x.ProductDescription,
                UnitsInStock = x.UnitsInStock

            }).FirstOrDefaultAsync();
        }
    }
}
