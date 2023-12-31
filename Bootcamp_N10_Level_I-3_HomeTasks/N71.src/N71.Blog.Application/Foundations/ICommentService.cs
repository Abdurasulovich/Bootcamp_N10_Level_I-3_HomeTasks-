﻿using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Application.Foundations;

public interface ICommentService
{
    IQueryable<Comment> Get(Expression<Func<Comment, bool>>? predicate, bool asNoTracking = false);

    ValueTask<Comment?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Comment>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Comment> DeleteAsync(Comment comment, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Comment> DeleteByIdAsync(Guid id, Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default);
}