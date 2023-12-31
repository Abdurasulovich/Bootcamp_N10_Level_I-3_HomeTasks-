﻿using Microsoft.EntityFrameworkCore;
using N70.Domain.Entities;

namespace N70.Persistance.DataContexts;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<AccessToken> AccessTokens => Set<AccessToken>();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}