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
    public interface IOrderDetails
    {
        Task<List<OrderDetailsDTO>> GetAllOrderDetailsAsync();
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



    }
}
