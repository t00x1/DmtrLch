using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Participation
{
    public string IdOfUser { get; set; } = null!;

    public string IdOfRoom { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Room IdOfRoomNavigation { get; set; } = null!;

    public virtual User IdOfUserNavigation { get; set; } = null!;
}
