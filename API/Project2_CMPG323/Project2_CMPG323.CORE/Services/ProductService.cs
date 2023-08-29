using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
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
        Task<ProductDTO?> GetProductAsync(short id);
        Task<ProductDTO> CreateProductAsync(CreatProductDTO creatProductDTO);
        Task<ProductDTO?> UpdatedProductAsync(short id, UpdateProductDTO updateProductDTO);
        Task<ProductDTO?> DeleteProductAsync(short id);

    }

    public class ProductService : IProductService
    {
        private readonly Project2Context _project2Context;

        private async Task<Product?> findProductAsync(short id)
        {
            var foundRecord = await _project2Context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();

            if(foundRecord is null)
            {
                return null;
            }

            return foundRecord;
        }

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

        public async Task<ProductDTO?> GetProductAsync(short id)
        {
            return await _project2Context.Products.Where(x => x.ProductId == id).Select(x => new ProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductDescription = x.ProductDescription,
                UnitsInStock = x.UnitsInStock

            }).FirstOrDefaultAsync();
        }

        public async Task<ProductDTO> CreateProductAsync(CreatProductDTO creatProductDTO)
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


        public async Task<ProductDTO?> UpdatedProductAsync(short id, UpdateProductDTO updateProductDTO)
        {
            var foundRecord = await findProductAsync(id);

            if (foundRecord is null)
            {
                return null;
            }

            if(updateProductDTO.ProductName is not null)
            {
                foundRecord.ProductName = updateProductDTO.ProductName;
            }

            if(updateProductDTO.ProductDescription is not null)
            {
                foundRecord.ProductDescription = updateProductDTO.ProductDescription;
            }

            if(updateProductDTO.UnitsInStock is not null)
            {
                foundRecord.UnitsInStock = updateProductDTO.UnitsInStock;
            }

            await _project2Context.SaveChangesAsync();

            return new ProductDTO
            {
                ProductId= foundRecord.ProductId,
                ProductDescription = foundRecord.ProductDescription,
                ProductName = foundRecord.ProductName,
                UnitsInStock = foundRecord.UnitsInStock
            };
        }

        public async Task<ProductDTO?> DeleteProductAsync(short id)
        {
            var foundRecord = await findProductAsync(id);

            if (foundRecord is null)
            {
                return null;
            }

            _project2Context.Products.Remove(foundRecord);
            await _project2Context.SaveChangesAsync();

            return new ProductDTO
            {
                ProductId = foundRecord.ProductId,
                ProductDescription = foundRecord.ProductDescription,
                ProductName = foundRecord.ProductName,
                UnitsInStock = foundRecord.UnitsInStock
            };

        }
    }
}
