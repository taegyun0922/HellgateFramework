﻿using UnityEngine;
using System.Collections;
using Hellgate;

public class HellgateWebViewEx : SceneController
{
    [SerializeField]
    private UIButton backButton;
    [SerializeField]
    private UIButton forwardButton;
    private string url;

    public override void OnSet (object data)
    {
        base.OnSet (data);

        url = data as string;

        WebViewManager.Instance.ProgressReceivedEvent += OnProgress;
        WebViewManager.Instance.ErrorReceivedEvent += OnError;
        WebViewManager.Instance.LoadURL (url, 50, 150, 50, 50);
    }

    private void SetButton (UIButton button, bool flag)
    {
        button.enabled = flag;

        byte alpha = 255;
        if (!flag) {
            alpha = 128;
        }
        button.defaultColor = new Color32 (255, 255, 255, alpha);
    }

    private void OnProgress (int progress)
    {
        if (progress >= 100) {
            SetButton (backButton, WebViewManager.Instance.CanGoBack ());
            SetButton (forwardButton, WebViewManager.Instance.CanGoForward ());
        }

        HDebug.Log ("OnProgress : " + progress);
    }

    private void OnError (string message)
    {
        HDebug.Log ("OnError : " + message);
    }

    public void OnClick ()
    {
        WebViewManager.Instance.ProgressReceivedEvent -= OnProgress;
        WebViewManager.Instance.ErrorReceivedEvent -= OnError;
        WebViewManager.Instance.Destroy ();
        OnClickClose ();
    }

    public void OnClickBack ()
    {
        WebViewManager.Instance.GoBack ();
    }

    public void OnClickForward ()
    {
        WebViewManager.Instance.GoForward ();
    }

    public void OnClickReload ()
    {
        WebViewManager.Instance.Reload ();
    }
}
