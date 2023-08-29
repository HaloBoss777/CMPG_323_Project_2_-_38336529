using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_CMPG323.CORE.DTO
{
    public class OrderDTO
    {
        public short OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public short CustomerId { get; set; }
        public string? DeliveryAddress { get; set; }
    }
}
