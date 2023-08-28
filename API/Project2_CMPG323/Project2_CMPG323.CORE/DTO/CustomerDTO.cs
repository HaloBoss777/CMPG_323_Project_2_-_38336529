using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_CMPG323.CORE.DTO
{
    public class CustomerDTO
    {
        public short CustomerId { get; set; }
        public string? CustomerTitle { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public string? CellPhone { get; set; }
    }

    public class AddCustomerDTO
    { 
        public string? CustomerTitle { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public string? CellPhone { get; set; }
    }

    public class UpdatedCustomerDTO
    {
        public string? CustomerTitle { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public string? CellPhone { get; set; }
    }

}
