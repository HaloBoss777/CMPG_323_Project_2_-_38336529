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
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
    }

    public class CustomerService: ICustomerService
    {
        private readonly Project2Context _project2Context;

        public CustomerService(Project2Context project2Context)
        {
            _project2Context = project2Context;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            return await _project2Context.Customers.Select(x => new CustomerDTO
            {
                CustomerId = x.CustomerId,
                CellPhone = x.CellPhone,
                CustomerName = x.CustomerName,
                CustomerSurname = x.CustomerSurname,
                CustomerTitle = x.CustomerTitle

            }).ToListAsync();
        }
    }
}
