﻿namespace ConsoleApp1;

public abstract class IdentityEvent(Guid userId) : Event
{
    public Guid UserId { get; set; } = userId;
}
