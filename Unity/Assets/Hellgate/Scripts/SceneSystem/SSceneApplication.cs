﻿//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
//					Hellgate Framework
// Copyright © Uniqtem Co., Ltd.
//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hellgate
{
	public class SSceneApplication
	{
		public delegate void OnLoadDelegate (GameObject root);

		private static Dictionary<string, OnLoadDelegate> onLoadeds = new Dictionary<string, OnLoadDelegate> ();

		private static bool AddScene (string sceneName, OnLoadDelegate onLoaded)
		{
			if (onLoadeds.ContainsKey (sceneName)) {
				return false;
			}

			onLoadeds.Add (sceneName, onLoaded);
			return true;
		}

		/// <summary>
		/// Loads the level.
		/// </summary>
		/// <param name="sceneName">Scene name.</param>
		/// <param name="onLoaded">On loaded.</param>
		/// <param name="isAdditive">If set to <c>true</c> is additive.</param>
		public static void LoadLevel (string sceneName, OnLoadDelegate onLoaded = null, bool isAdditive = false)
		{
			if (AddScene (sceneName, onLoaded)) {
#if UNITY_5_3
				UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
#elif
				if (isAdditive) {
					Application.LoadLevelAdditive (sceneName);
				} else {
					Application.LoadLevel (sceneName);
				}
#endif
			}
		}

		/// <summary>
		/// Loads the level async.
		/// </summary>
		/// <param name="sceneName">Scene name.</param>
		/// <param name="onLoaded">On loaded.</param>
		/// <param name="isAdditive">If set to <c>true</c> is additive.</param>
		public static void LoadLevelAsync (string sceneName, OnLoadDelegate onLoaded = null, bool isAdditive = false)
		{
			if (AddScene (sceneName, onLoaded)) {
#if UNITY_5_3
				UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (sceneName);
#elif
			if (isAdditive) {
				Application.LoadLevelAdditiveAsync (sceneName);
			} else {
				Application.LoadLevelAsync (sceneName);
			}
#endif
			}
		}

		/// <summary>
		/// Loaded the specified root.
		/// </summary>
		/// <param name="root">Root.</param>
		public static void Loaded (GameObject root)
		{
			if (onLoadeds [root.name] != null) {
				onLoadeds [root.name] (root);
			}
		}

		/// <summary>
		/// Unloaded the specified root.
		/// </summary>
		/// <param name="root">Root.</param>
		public static void Unloaded (GameObject root)
		{
			if (onLoadeds.ContainsKey (root.name)) {
				onLoadeds.Remove (root.name);
			}
		}
	}
}
