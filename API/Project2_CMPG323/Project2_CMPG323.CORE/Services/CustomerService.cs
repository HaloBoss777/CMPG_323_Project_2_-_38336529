using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
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
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerAsync(short Id);
        Task<CustomerDTO> CreateCustomerAsync(AddCustomerDTO addCustomerDTO);
        Task<CustomerDTO?> UpdateCustomer(short id, UpdatedCustomerDTO _updatedCustomerDTO);
        Task<CustomerDTO?> DeleteCustomerAsync(short id);
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

        public async Task<CustomerDTO?> GetCustomerAsync(short Id)
        {
            return await _project2Context.Customers.Where(x => x.CustomerId == Id).Select(x => new CustomerDTO
            {
                CustomerId = x.CustomerId,
                CellPhone = x.CellPhone,
                CustomerName = x.CustomerName,
                CustomerSurname = x.CustomerSurname,
                CustomerTitle = x.CustomerTitle

            }).FirstOrDefaultAsync();
        }

        public async Task<CustomerDTO> CreateCustomerAsync(AddCustomerDTO addCustomerDTO)
        {
            int lastCustomerId = 0;

            var lastCustomer = await _project2Context.Customers.OrderByDescending(x => x.CustomerId).FirstOrDefaultAsync();


            if(lastCustomer is null)
            {
                lastCustomerId = 1;
            }
            else
            {
                lastCustomerId = lastCustomer.CustomerId;
                ++lastCustomerId;
            }

            var addCustomerDomainModel = new Customer
            {
                CustomerId = (Int16)(lastCustomerId),
                CellPhone = addCustomerDTO.CellPhone,
                CustomerName = addCustomerDTO.CustomerName,
                CustomerSurname = addCustomerDTO.CustomerSurname,
                CustomerTitle = addCustomerDTO.CustomerTitle
            };

            await _project2Context.Customers.AddAsync(addCustomerDomainModel);
            await _project2Context.SaveChangesAsync();

            return new CustomerDTO
            {
                CustomerId = addCustomerDomainModel.CustomerId,
                CellPhone = addCustomerDomainModel.CellPhone,
                CustomerName = addCustomerDomainModel.CustomerName,
                CustomerSurname = addCustomerDomainModel.CustomerSurname,
                CustomerTitle = addCustomerDomainModel.CustomerTitle
            };
        }

        public async Task<CustomerDTO?> UpdateCustomer(short id, UpdatedCustomerDTO _updatedCustomerDTO)
        {
            var customerRecord = await _project2Context.Customers.Where(x => x.CustomerId == id).FirstOrDefaultAsync();

            if(customerRecord is null)
            {
                return null;
            }

            if(_updatedCustomerDTO.CustomerName is not null)
            {
                customerRecord.CustomerName = _updatedCustomerDTO.CustomerName;
            }

            if(_updatedCustomerDTO.CustomerSurname is not null)
            {
                customerRecord.CustomerSurname = _updatedCustomerDTO.CustomerSurname;
            }

            if(_updatedCustomerDTO.CustomerTitle is not null)
            {
                customerRecord.CustomerTitle = _updatedCustomerDTO.CustomerTitle;
            }

            if(_updatedCustomerDTO.CellPhone is not null)
            {
                customerRecord.CellPhone = _updatedCustomerDTO.CellPhone;
            }

            await _project2Context.SaveChangesAsync();

            return new CustomerDTO 
            {
                CustomerId = customerRecord.CustomerId,
                CustomerName = customerRecord.CustomerName,
                CustomerSurname = customerRecord.CustomerSurname,
                CustomerTitle = customerRecord.CustomerTitle,
                CellPhone = customerRecord.CellPhone,
            };

        }

        public async Task<CustomerDTO?> DeleteCustomerAsync(short id)
        {
            var customerRecord = await _project2Context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

            if(customerRecord is null)
            {
                return null;
            }

            _project2Context.Customers.Remove(customerRecord);
            await _project2Context.SaveChangesAsync();

            return new CustomerDTO
            {
                CustomerId = customerRecord.CustomerId,
                CustomerName = customerRecord.CustomerName,
                CellPhone= customerRecord.CellPhone,
                CustomerSurname = customerRecord.CustomerSurname,
                CustomerTitle = customerRecord.CustomerTitle,
            };
        }
    }
}
