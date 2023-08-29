using Microsoft.EntityFrameworkCore;
using Project2_CMPG323.CORE.DTO;
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
    }
}
