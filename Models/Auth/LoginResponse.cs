﻿namespace UniTrail.Admin.Models.Auth;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string Role { get; set; }
    public string Username { get; set; }
}