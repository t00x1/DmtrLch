using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Image
{
    public string IdOfImage { get; set; } = null!;

    public string Directory { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
