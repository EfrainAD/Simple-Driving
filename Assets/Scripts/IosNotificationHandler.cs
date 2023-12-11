using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
    using Unity.Notifications.iOS;
#endif

public class IosNotificationHandler : MonoBehaviour
{
#if UNITY_IOS
    public void ScheduleNotification(int minutes)
    {
        iOSNotification notification = new iOSNotification
        {
            // Notifiction Message
            Title = "Energy Recharged",
            Subtitle = "Your energy is refield",
            Body = "You now can play!",
            // Notification Settings
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "CatigoryNone",
            ThreadIdentifier = "Thread0",
            // When to send 
            Trigger = new iOSNotificationTimeIntervalTrigger{
                TimeInterval = new System.TimeSpan(0, minutes, 0),
                Repeats = false
            }
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}
