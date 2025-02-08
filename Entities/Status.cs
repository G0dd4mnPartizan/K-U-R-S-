using System;
using System.Collections.Generic;

namespace kurs.Entities;

public partial class Status
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ClientId { get; set; }

    public string? DeliveryAddress { get; set; }

    public int? DeliveryId { get; set; }

    public decimal? Amount { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Delivery? Delivery { get; set; }

    public virtual Order? Order { get; set; }
}
