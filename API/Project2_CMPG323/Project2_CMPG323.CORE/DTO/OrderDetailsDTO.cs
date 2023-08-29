using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_CMPG323.CORE.DTO
{
    public class OrderDetailsDTO
    {
        public short OrderDetailsId { get; set; }

        public short OrderId { get; set; }

        public short ProductId { get; set; }

        public int Quantity { get; set; }

        public int? Discount { get; set; }
    }

    public class CreateOrderDetailDTO
    {
        public short OrderId { get; set; }

        public short ProductId { get; set; }

        public int Quantity { get; set; }

        public int? Discount { get; set; }
    }

    public class UpdateOrderDetailDTO
    {
        public short? OrderId { get; set; }

        public short? ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? Discount { get; set; }
    }

    public class AllProductOfOrderDetailDTO 
    {
        public short OrderId { get; set; }

        public List<ProductDTO>? Products { get; set; }
    }
}
