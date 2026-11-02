using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Notifications
{
    public interface INotificationService
    {
        void Send(string recipient, string message);
    }
}
