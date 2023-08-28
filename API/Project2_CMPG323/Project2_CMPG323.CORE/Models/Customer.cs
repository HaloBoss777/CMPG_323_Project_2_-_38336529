using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project2_CMPG323.CORE.Models;

public partial class Customer
{
    [Key]
    public short CustomerId { get; set; }

    [StringLength(50)]
    public string? CustomerTitle { get; set; }

    [StringLength(50)]
    public string? CustomerName { get; set; }

    [StringLength(50)]
    public string? CustomerSurname { get; set; }

    [StringLength(50)]
    public string? CellPhone { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
