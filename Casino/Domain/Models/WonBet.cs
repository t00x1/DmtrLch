using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class WonBet
{
    public string IdOfGame { get; set; } = null!;

    public string WonWeb { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Game IdOfGameNavigation { get; set; } = null!;
}
