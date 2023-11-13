﻿namespace N70.Application.Common.Identity.Services;

public interface IPasswordHasherService
{
    string HashPassword(string password);
    
    bool ValidatePassword(string password, string hashedPassword);
}