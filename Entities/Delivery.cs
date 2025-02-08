using System;
using System.Collections.Generic;

namespace kurs.Entities;

public partial class Delivery
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
