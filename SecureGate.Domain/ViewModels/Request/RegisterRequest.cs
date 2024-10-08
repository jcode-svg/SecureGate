﻿namespace SecureGate.Domain.ViewModels.Request
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get;  set; }
        public DateTime DateOfBirth { get; set; }
    }
}
