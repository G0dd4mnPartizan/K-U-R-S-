using System;
using System.Collections.Generic;

namespace kurs.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
