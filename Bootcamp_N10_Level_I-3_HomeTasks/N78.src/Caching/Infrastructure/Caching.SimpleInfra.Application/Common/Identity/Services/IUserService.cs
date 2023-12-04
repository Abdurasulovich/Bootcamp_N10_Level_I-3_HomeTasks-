﻿using Caching.SimpleInfra.Domain.Entities;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Caching.SimpleInfra.Application.Common.Identity.Services;

public interface IUserService
{
    IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default, 
        bool asNoTracking = false
        );

    ValueTask<User?> GetByIdAsync(
        Guid userId, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default
        );

    ValueTask<User> CreateAsync(
        User user, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default
        );

    ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );

    ValueTask<User?> DeleteByIdAsync(
        Guid userId,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );
}
