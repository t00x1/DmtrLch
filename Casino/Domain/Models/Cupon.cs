using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Cupon
{
    public string IdOfCupon { get; set; } = null!;

    public bool Reusable { get; set; }

    public DateTime? ExpireAt { get; set; }

    public int Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<CuponsUsed> CuponsUseds { get; set; } = new List<CuponsUsed>();
}
