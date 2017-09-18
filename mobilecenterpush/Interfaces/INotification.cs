using System;

namespace mobilecenterpush.Interfaces
{
    public interface INotification
    {
        void HandleNotification(string title, string msg);
    }
}
