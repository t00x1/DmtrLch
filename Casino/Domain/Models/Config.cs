using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Config
{
    public string IdOfUser { get; set; } = null!;

    public bool Notifications { get; set; }

    public bool SaveHistory { get; set; }

    public bool DarkTheme { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User IdOfUserNavigation { get; set; } = null!;
}
