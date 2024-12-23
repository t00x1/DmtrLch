using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Session
{
    public string IdOfSession { get; set; } = null!;

    public string IdOfUser { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Token { get; set; } = null!;

    public virtual User IdOfUserNavigation { get; set; } = null!;
}
