﻿using Sms.Infrastructure.Domain.Common.Exceptions;

namespace Sms.Infrastructure.Domain.Extensions;

public static class ExceptionExtensions
{
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<Task<T>> func) where T : struct
    {
        FuncResult<T> result;

        try
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception ex)
        {
            result = new FuncResult<T>(ex);
        }

        return result;
    }
}