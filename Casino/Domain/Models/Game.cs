using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Game
{
    public string IdOfGame { get; set; } = null!;

    public string? TypeOfGame { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual WonBet? WonBet { get; set; }
}
