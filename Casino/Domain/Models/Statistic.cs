using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Statistic
{
    public string IdOfUser { get; set; } = null!;

    public int ChipsWon { get; set; }

    public int BetsWon { get; set; }

    public int Bet { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User IdOfUserNavigation { get; set; } = null!;
}
