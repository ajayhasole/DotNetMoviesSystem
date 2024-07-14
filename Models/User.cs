using System;
using System.Collections.Generic;

namespace Movies.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int MobileNumber { get; set; }

    public string Password { get; set; } = null!;
}
