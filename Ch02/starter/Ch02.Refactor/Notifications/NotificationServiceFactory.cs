using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Notifications
{
    public enum NotificationChannel
    {
        Email,
        Sms,
        Slack
    }

    public class EmailNotificationService : INotificationService
    {
        public void Send(string recipient, string message) => Console.WriteLine($"[EMAIL] To {recipient}: {message}");
    }

    public class SmsNotificationService : INotificationService
    {
        public void Send(string recipient, string message) => Console.WriteLine($"[SMS] To {recipient}: {message}");
    }

    public class SlackNotificationService : INotificationService
    {
        public void Send(string recipient, string message) => Console.WriteLine($"[SLACK] To {recipient}: {message}");
    }

    public static class NotificationServiceFactory
    {
        public static INotificationService Create(NotificationChannel channel) => channel switch
        {
            NotificationChannel.Email => new EmailNotificationService(),
            NotificationChannel.Sms => new SmsNotificationService(),
            NotificationChannel.Slack => new SlackNotificationService(),
            _ => throw new ArgumentException("Kênh thông báo không hợp lệ", nameof(channel))
        };
    }
}
