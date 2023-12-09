using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndriodNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private string channelId = "energy_channel";
    public void ScheduleNotification(DateTime dateTime)
    {
        //  Channel, Every notification has to have a channel, so we need to make one
        AndroidNotificationChannel notificationChannel= new AndroidNotificationChannel
        {
            Id = channelId,
            Name = "Energy Channel",
            Description = "Let play know when come back",
            Importance = Importance.Default
        };

        // Register the notification channel we made with notification 'center'
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        // We need to create the actual notification to be sent
        AndroidNotification notification = new AndroidNotification
        {
            Title = "Engergy Recharged",
            Text = "Come Back to play!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, channelId);
    }
#endif
}
