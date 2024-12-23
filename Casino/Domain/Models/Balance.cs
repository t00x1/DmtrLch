using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Balance
{
    public string IdOfUser { get; set; } = null!;

    public int Balance1 { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User IdOfUserNavigation { get; set; } = null!;
}
