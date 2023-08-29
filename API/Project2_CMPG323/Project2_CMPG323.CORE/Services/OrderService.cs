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
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderAsync(short id);
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO orderDTO);
    }

    public class OrderService : IOrderService
    {
        private readonly Project2Context _project2Context;

        public OrderService(Project2Context project2Context) 
        {
            _project2Context = project2Context;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            return await _project2Context.Orders.Select(x => new OrderDTO
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                DeliveryAddress = x.DeliveryAddress,
                OrderDate = x.OrderDate

            }).ToListAsync();
        }

        public async Task<OrderDTO?> GetOrderAsync(short id)
        {
            return await _project2Context.Orders.Where(x => x.OrderId == id).Select(x => new OrderDTO
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                DeliveryAddress = x.DeliveryAddress,
                OrderDate = x.OrderDate

            }).FirstOrDefaultAsync();
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO orderDTO)
        {
            int lastOrderId = 0;

            var lastOrder = await _project2Context.Orders.OrderByDescending(x => x.OrderId).FirstOrDefaultAsync();

            if(lastOrder is null)
            {
                lastOrderId = 1;
            }
            else
            {
                lastOrderId = lastOrder.OrderId;
                ++lastOrderId;
            }

            var addOrderDomainModel = new Order
            {
                OrderId = (short)lastOrderId,
                CustomerId = orderDTO.CustomerId,
                DeliveryAddress = orderDTO.DeliveryAddress,
                OrderDate = orderDTO.OrderDate

            };

            await _project2Context.Orders.AddAsync(addOrderDomainModel);
            await _project2Context.SaveChangesAsync();

            return new OrderDTO
            {
                OrderId = addOrderDomainModel.OrderId,
                CustomerId = addOrderDomainModel.CustomerId,
                DeliveryAddress = addOrderDomainModel.DeliveryAddress,
                OrderDate = addOrderDomainModel.OrderDate
            };
        }
    }
}
