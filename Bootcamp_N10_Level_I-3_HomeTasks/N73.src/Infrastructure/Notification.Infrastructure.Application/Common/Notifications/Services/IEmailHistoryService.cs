﻿using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailHistoryService
{
    ValueTask<IList<EmailHistory>> GetByFilterAsync(
        FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<EmailHistory> CreateAsync(
        EmailHistory emailHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}