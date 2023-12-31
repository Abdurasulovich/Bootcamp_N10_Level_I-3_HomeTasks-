﻿using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using N71.Blog.Application.Foundations;
using N71.Blog.Application.ManagementServices.Interfaces;
using N71.Blog.Domain.Entity;
using N71.Blog.Domain.Enums;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Infrastructure.Common.ManagementService;

public class BlogManagementService : IBlogManagementService
{
    private readonly IBlogService _blogService;
    private readonly IUserService _userService;
    private readonly ICommentService _commentService;

    public BlogManagementService(
        IBlogService blogService,
        IUserService userService,
        ICommentService commentService)
    {
        _blogService = blogService;
        _userService = userService;
        _commentService = commentService;
    }
    public async ValueTask<IList<Blogs>> GetBlogsByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var blogList = _blogService.Get(blog => blog.UserId == userId, true);

        return await blogList.ToListAsync(cancellationToken: cancellationToken);
    }

    public async ValueTask<Comment> CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        _ = await _blogService.GetByIdAsync(comment.BlogId, true, cancellationToken)
            ?? throw new InvalidOperationException();

        _ = await _userService.GetByIdAsync(comment.UserId, true, cancellationToken)
            ?? throw new InvalidOperationException();

        return await _commentService.CreateAsync(comment, cancellationToken: cancellationToken);
    }

    public async ValueTask<IList<Comment>> GetCommentsByBlogsIdAsync(Guid blogId, CancellationToken cancellationToken = default)
    {
        var comments = _commentService.Get(comment => comment.BlogId == blogId, true);

        return await comments.ToListAsync(cancellationToken: cancellationToken);
    }

    public async ValueTask<IList<User>> GetPopularBloggersAsync(CancellationToken cancellationToken = default)
    {
        var users = _userService.Get(user => user.Role.Type == RoleType.Writer);
        var posts = _blogService.Get(self => true).Include(blog => blog.Comments);

        var query = from blogger in users
            join post in posts on blogger.Id equals post.UserId into bloggerPosts
            select new
            {
                Blogger = blogger,
                PostCount = bloggerPosts.Count(self => self.Comments.Count > 20)
            }
            into BlogPosts
            where BlogPosts.PostCount >= 5
            select BlogPosts.Blogger;

        return await query.ToListAsync(cancellationToken);
    }
}