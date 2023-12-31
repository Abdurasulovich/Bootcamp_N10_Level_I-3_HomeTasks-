﻿using FluentValidation;
using Microsoft.Extensions.Options;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Infrastructure.Common.Settings;
using System.Text;
using System.Text.RegularExpressions;
using Twilio.Rest.Verify.V2;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services;

public class SmsRenderingService : ISmsRenderingService
{
    private readonly IValidator<SmsMessage> _smsMessageValidator;
    private readonly TemplateRenderingSettings _templateRenderingSettings;

    public SmsRenderingService(
        IOptions<TemplateRenderingSettings> templateRenderingSettings,
        IValidator<SmsMessage> smsMessageValidator)
    {
        _templateRenderingSettings = templateRenderingSettings.Value;
        _smsMessageValidator = smsMessageValidator;
    }
    public ValueTask<string> RenderAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _smsMessageValidator.Validate(smsMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnRendering.ToString()));
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var placeholderRegex = new Regex(_templateRenderingSettings.PlaceholderRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var placeholderValueRegex = new Regex(_templateRenderingSettings.PlaceholderValueRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var matches = placeholderRegex.Matches(smsMessage.Template.Content);

        if (matches.Any() && !smsMessage.Variables.Any())
            throw new InvalidOperationException("Variables are required for this template.");

        var templatePlaceholders = matches.Select(match =>
        {
            var placeholder = match.Value;
            var placeholderValue = placeholderValueRegex.Match(placeholder).Groups[1].Value;
            var valid = smsMessage.Variables.TryGetValue(placeholderValue, out var value);

            return new TemplatePlaceholder
            {
                Placeholder = placeholder,
                PlaceholderValue = placeholderValue,
                Value = value,
                IsValid = valid
            };
        }).ToList();

        ValidatePlaceholders(templatePlaceholders);

        var messageBuilder = new StringBuilder(smsMessage.Template.Content);
        templatePlaceholders.ForEach(placeholder => messageBuilder.Replace(placeholder.Placeholder, placeholder.Value));

        var message = messageBuilder.ToString();
        smsMessage.Message = message;

        return ValueTask.FromResult(message);
    }

    private void ValidatePlaceholders(IEnumerable<TemplatePlaceholder> templatePlaceholder)
    {
        var missingPlaceholders = templatePlaceholder.Where(placeholder => !placeholder.IsValid)
            .Select(placeholder => placeholder.PlaceholderValue)
            .ToList();
        
        if(!missingPlaceholders.Any()) return;
        var errorMessage = new StringBuilder();
        missingPlaceholders.ForEach(placeholderValue => errorMessage.Append(placeholderValue).Append(','));

        throw new InvalidOperationException(
            $"Variable for given placeholder is not found - {string.Join(',', missingPlaceholders)}");
    }
}
