using Microsoft.EntityFrameworkCore;
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
    public interface IOrderDetails
    {
        Task<List<OrderDetailsDTO>> GetAllOrderDetailsAsync();
        Task<OrderDetailsDTO?> GetOrderDetailAsync(short id);
        Task<OrderDetailsDTO> CreateOrderDetailAsync(CreateOrderDetailDTO createOrderDetailsDTO);
        Task<OrderDetailsDTO?> UpdateOrderDetailAsync(short id, UpdateOrderDetailDTO updateOrderDetailDTO);
        Task<OrderDetailsDTO?> DeleteOrderDetailAsync(short id);
        Task<AllProductOfOrderDetailDTO?> GetAllProductsOfOrderAsync(short id);
    }

    public class OrderDetailsService : IOrderDetails
    {
        private readonly Project2Context _project2Context;

        public OrderDetailsService(Project2Context project2Context)
        {
            _project2Context = project2Context;
        }

        public async Task<List<OrderDetailsDTO>> GetAllOrderDetailsAsync()
        {
            return await _project2Context.OrderDetails.Select(x => new OrderDetailsDTO
            {
                OrderDetailsId = x.OrderDetailsId,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Discount = x.Discount

            }).ToListAsync();
        }

        public async Task<OrderDetailsDTO?> GetOrderDetailAsync(short id)
        {
            return await _project2Context.OrderDetails.Where(x => x.OrderDetailsId == id).Select(x => new OrderDetailsDTO
            {
                OrderDetailsId = x.OrderDetailsId,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Discount = x.Discount

            }).FirstOrDefaultAsync();
        }

        public async Task<OrderDetailsDTO> CreateOrderDetailAsync(CreateOrderDetailDTO createOrderDetailsDTO)
        {
            int lastOrderDetailId = 0;

            var lastOrderDetail = await _project2Context.OrderDetails.OrderByDescending(x => x.OrderDetailsId).FirstOrDefaultAsync();


            if (lastOrderDetail is null)
            {
                lastOrderDetailId = 1;
            }
            else
            {
                lastOrderDetailId = lastOrderDetail.OrderDetailsId;
                ++lastOrderDetailId;
            }

            var addOrderDetailModel = new OrderDetail
            {
                OrderDetailsId = (short)lastOrderDetailId,
                OrderId = createOrderDetailsDTO.OrderId,
                ProductId = createOrderDetailsDTO.ProductId,
                Quantity = createOrderDetailsDTO.Quantity,
                Discount = createOrderDetailsDTO.Discount
            };

            await _project2Context.OrderDetails.AddAsync(addOrderDetailModel);
            await _project2Context.SaveChangesAsync();

            return new OrderDetailsDTO
            {
                OrderDetailsId = addOrderDetailModel.OrderDetailsId,
                OrderId = addOrderDetailModel.OrderId,
                ProductId = addOrderDetailModel.ProductId,
                Quantity = addOrderDetailModel.Quantity,
                Discount = addOrderDetailModel.Discount
            };
        }

        public async Task<OrderDetailsDTO?> UpdateOrderDetailAsync(short id, UpdateOrderDetailDTO updateOrderDetailDTO)
        {
            var foundRecord = await _project2Context.OrderDetails.Where(x => x.OrderDetailsId == id).FirstOrDefaultAsync();

            if (foundRecord is null) 
            {
                return null;
            }

            if (updateOrderDetailDTO.OrderId is not null)
            {
                foundRecord.OrderId = (short)updateOrderDetailDTO.OrderId;
            }

            if(updateOrderDetailDTO.ProductId is not null)
            {
                foundRecord.ProductId = (short)updateOrderDetailDTO.ProductId;
            }

            if(updateOrderDetailDTO.Quantity is not null)
            {
                foundRecord.Quantity = (int)updateOrderDetailDTO.Quantity;
            }

            if(updateOrderDetailDTO.Discount is not null)
            {
                foundRecord.Discount = (int)updateOrderDetailDTO.Discount;
            }

            await _project2Context.SaveChangesAsync();

            return new OrderDetailsDTO
            {
                OrderDetailsId = foundRecord.OrderDetailsId,
                OrderId = foundRecord.OrderId,
                ProductId = foundRecord.ProductId,
                Quantity = foundRecord.Quantity,
                Discount = foundRecord.Discount,
            };
        }

        public async Task<OrderDetailsDTO?> DeleteOrderDetailAsync(short id)
        {
            var foundRecord = await _project2Context.OrderDetails.Where(x => x.OrderDetailsId == id).FirstOrDefaultAsync();

            if(foundRecord is null)
            {
                return null;
            }

            _project2Context.OrderDetails.Remove(foundRecord);
            await _project2Context.SaveChangesAsync();

            return new OrderDetailsDTO
            {
                OrderDetailsId = foundRecord.OrderDetailsId,
                OrderId = foundRecord.OrderId,
                ProductId = foundRecord.ProductId,
                Quantity = foundRecord.Quantity,
                Discount = foundRecord.Discount,
            };
        }

        public async Task<AllProductOfOrderDetailDTO?> GetAllProductsOfOrderAsync(short id)
        {
            var products = await _project2Context.OrderDetails.Where(x => x.OrderId == id).Include(x =>  x.Product).Select(x => new ProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.Product.ProductName,
                ProductDescription = x.Product.ProductDescription,
                UnitsInStock = x.Product.UnitsInStock,

            }).ToListAsync();

            return new AllProductOfOrderDetailDTO
            {
                OrderId = id,
                Products = products
            };
        }
    }
}
