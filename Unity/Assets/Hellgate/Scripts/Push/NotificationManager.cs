//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
//					Hellgate Framework
// Copyright © Uniqtem Co., Ltd.
//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
using UnityEngine;
using System;
using System.Collections;

namespace Hellgate
{
	public class NotificationManager : Notification
	{
#region Const
		public const string NOTIFICATION_MANAGER = "NotificationManager";
#endregion

#region Singleton
		private static NotificationManager instance = null;
		
		public static NotificationManager Instance {
			get {
				if (instance == null) {
					GameObject gObj = new GameObject ();
					instance = gObj.AddComponent<NotificationManager> ();
					gObj.name = NOTIFICATION_MANAGER;

					DontDestroyOnLoad (gObj);
				}
				
				return instance;
			}
		}
#endregion

		public event Action<string> deviceTokenReceivedEvent;
		public event Action<string> localNotificationReceivedEvent;
		public event Action<string> remoteNotificationReceivedEvent;

		protected virtual void Awake ()
		{
			if (instance == null) {
				instance = this;
				DontDestroyOnLoad (this.gameObject);
			}
		}

		protected override void DeviceTokenReceived (string tokenID)
		{
			if (deviceTokenReceivedEvent != null) {
				deviceTokenReceivedEvent (tokenID);
			}
		}

		protected override void LocalNotificationReceived (string text)
		{
			if (localNotificationReceivedEvent != null) {
				localNotificationReceivedEvent (text);
			}
		}

		protected override void RemoteNotificationReceived (string text)
		{
			if (remoteNotificationReceivedEvent != null) {
				remoteNotificationReceivedEvent (text);
			}
		}

		public override void Register (string gcmSenderId = "")
		{
			base.Register (gcmSenderId);
		}

		public override string GetRegistrationId ()
		{
			return base.GetRegistrationId();
		}

		public override void ScheduleLocalNotification (DateTime dateTime, string text, string title = "")
		{
			base.ScheduleLocalNotification (dateTime, text, title);
		}

		public override void CancelLocalNotification ()
		{
			base.CancelLocalNotification ();
		}

		public override void CancelAllLocalNotifications ()
		{
			base.CancelAllLocalNotifications ();
		}
		
#if UNITY_ANDROID
		public override void Unregister ()
		{
			base.Unregister ();
		}

		public override void SetNotificationsEnabled (bool enabled)
		{
			base.SetNotificationsEnabled (enabled);
		}
#endif
	}
}
