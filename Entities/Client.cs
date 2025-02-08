using System;
using System.Collections.Generic;

namespace kurs.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string Fio { get; set; } = null!;

    public string? Phone { get; set; }

    public int? PaymentTypeId { get; set; }

    public virtual Payment? PaymentType { get; set; }

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
