using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project2_CMPG323.CORE.DTO;
using Project2_CMPG323.CORE.Models;
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
        Task<ProductDTO> CreateProduct(CreatProductDTO creatProductDTO);
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

        public async Task<ProductDTO> CreateProduct(CreatProductDTO creatProductDTO)
        {
            int lastProductId = 0;

            var lastProduct = await _project2Context.Products.OrderByDescending(x => x.ProductId).FirstOrDefaultAsync();

            if (lastProduct is null)
            {
                lastProductId = 1;
            }
            else
            {
                lastProductId = lastProduct.ProductId;
                ++lastProductId;
            }

            var addProductDomainModel = new Product
            {
                ProductId = (Int16)(lastProductId),
                ProductName = creatProductDTO.ProductName,
                ProductDescription = creatProductDTO.ProductDescription,
                UnitsInStock = creatProductDTO.UnitsInStock,
            };

            await _project2Context.Products.AddAsync(addProductDomainModel);
            await _project2Context.SaveChangesAsync();

            return new ProductDTO
            {
                ProductId = addProductDomainModel.ProductId,
                ProductName = addProductDomainModel.ProductName,
                ProductDescription = addProductDomainModel.ProductDescription,
                UnitsInStock = addProductDomainModel.UnitsInStock,
            };
        }

    }
}
