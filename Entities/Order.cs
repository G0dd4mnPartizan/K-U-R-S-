using System;
using System.Collections.Generic;

namespace kurs.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
