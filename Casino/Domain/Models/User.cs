using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class User
{
    public string IdOfUser { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Password { get; set; } = null!;

    public string? Bio { get; set; }

    public bool? Admin { get; set; }

    public string? Avatar { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Location { get; set; }

    public bool? Male { get; set; }

    public string Email { get; set; } = null!;

    public bool ConfirmedEmail { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Image? AvatarNavigation { get; set; }

    public virtual Balance? Balance { get; set; }

    public virtual Config? Config { get; set; }

    public virtual ICollection<CuponsUsed> CuponsUseds { get; set; } = new List<CuponsUsed>();

    public virtual ICollection<EmailConfirmation> EmailConfirmations { get; set; } = new List<EmailConfirmation>();

    public virtual ICollection<Participation> Participations { get; set; } = new List<Participation>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual Statistic? Statistic { get; set; }
}
