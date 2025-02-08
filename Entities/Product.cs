using System;
using System.Collections.Generic;

namespace kurs.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? SuppliersId { get; set; }

    public decimal Price { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Supplier? Suppliers { get; set; }
}
