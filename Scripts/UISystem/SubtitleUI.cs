using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleUI : MonoBehaviour
{
    #region Runtime data
    protected Text m_text;
    protected Canvas m_canvas;
    #endregion

    #region Singleton
    public static SubtitleUI Singleton
    {
        get;
        protected set;
    }

    #endregion

    public delegate bool HideLineCondition();
    HideLineCondition currentHideCondition = null;

    #region Life Circle
    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        m_text = GetComponentInChildren<Text>();
        m_canvas = GetComponentInChildren<Canvas>();
        if (m_canvas)
        {
            m_canvas.worldCamera = VRPlayer.Instance.UICamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lineShowed)
        {
            if (currentHideCondition == null || currentHideCondition())
            {
                HideSubtitleUI();
                currentHideCondition = null;
            }
        }
    }
    #endregion

    #region Internal Control
    bool lineShowed = false;
    void ShowSubtitleUI()
    {
        lineShowed = true;
        if (m_canvas != null)
            m_canvas.enabled = true;
    }

    void HideSubtitleUI()
    {
        lineShowed = false;
        if (m_canvas != null)
            m_canvas.enabled = false;
    }
    #endregion

    #region External Control

    /// <summary>
    /// Push line and it is interruptable
    /// </summary>
    /// <param name="line"></param>
    /// <param name="hideCondition"></param>
    public void PushLine(string line, HideLineCondition hideCondition)
    {
        if (hideCondition == null || string.IsNullOrEmpty(line))
        {
            Debug.LogWarning("No hidden condition added, will not shouw assigned line");
            return;
        }

        if (m_text != null)
        {
            m_text.text = line.Replace("\\n", "\n");
        }

        ShowSubtitleUI();
        currentHideCondition = hideCondition;
    }

    public void PushLine(string line, float time)
    {
        float currentTime = Time.time;
        PushLine(line, () => { return currentTime + time < Time.time; });
    }

    public void PushLine(string line, AudioSource listenSource)
    {
        if (listenSource == null)
            return;
        PushLine(line, () => { return !listenSource.isPlaying; });
    }

    public void PushLine(string line, AudioClip clip)
    {
        if (clip == null)
            return;

        PushLine(line, clip.length);
    }
    #endregion
}
