using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Room
{
    public string IdOfRoom { get; set; } = null!;

    public string IdOfGame { get; set; } = null!;

    public string NameOfRoom { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Game IdOfGameNavigation { get; set; } = null!;

    public virtual ICollection<Participation> Participations { get; set; } = new List<Participation>();
}
