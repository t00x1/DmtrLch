using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class CuponsUsed
{
    public string IdOfCupon { get; set; } = null!;

    public string IdOfUser { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Cupon IdOfCuponNavigation { get; set; } = null!;

    public virtual User IdOfUserNavigation { get; set; } = null!;
}
