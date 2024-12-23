using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class EmailConfirmation
{
    public string IdReq { get; set; } = null!;

    public string IdOfUser { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime Expire { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User IdOfUserNavigation { get; set; } = null!;
}
