﻿//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
//					Hellgate Framework
// Copyright © Uniqtem Co., Ltd.
//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
using UnityEngine;
using System.Collections;

namespace Hellgate
{
	public class MenuController : SceneController
	{
#region Singleton
		private static MenuController instance;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static MenuController Instance {
			get {
				return instance;
			}
		}
#endregion

		[SerializeField]
		private GameObject top;
		[SerializeField]
		private GameObject bottom;
		[SerializeField]
		private Camera uI3D;

		public Camera UI3D {
			get {
				return uI3D;
			}
		}

		protected virtual void OnDestroy ()
		{
			instance = null;
		}

		public override void Awake ()
		{
			base.Awake ();

			instance = this;
		}

		public override void OnEnable ()
		{
			base.OnEnable ();
			
			if (SSSceneManager.Instance != null) {
				SceneManager.Instance.onScreenStartChange += OnScreenStartChange;
			}
		}
		
		public override void OnDisable ()
		{
			base.OnDisable ();
			
			if (SSSceneManager.Instance != null) {
				SceneManager.Instance.onScreenStartChange -= OnScreenStartChange;
			}
		}

		/// <summary>
		/// Raises the screen start change event.
		/// </summary>
		/// <param name="sceneName">Scene name.</param>
		protected virtual void OnScreenStartChange (string sceneName)
		{
			SetActiveTop ();
			SetActiveBottom ();
		}

		/// <summary>
		/// Sets the active top.
		/// </summary>
		/// <param name="flag">If set to <c>true</c> flag.</param>
		public void SetActiveTop (bool flag = true)
		{
			if (top != null) {
				top.SetActive (flag);
			}
		}

		/// <summary>
		/// Sets the active bottom.
		/// </summary>
		/// <param name="flag">If set to <c>true</c> flag.</param>
		public void SetActiveBottom (bool flag = true)
		{
			if (bottom != null) {
				bottom.SetActive (flag);
			}
		}
	}
}
