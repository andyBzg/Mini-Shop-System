﻿namespace Application.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string plainPassword);
        bool VerifyPassword(string plainPassword, string storedPassword);
    }
}
