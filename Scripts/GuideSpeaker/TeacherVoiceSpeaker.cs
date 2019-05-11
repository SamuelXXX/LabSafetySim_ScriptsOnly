using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TeacherVoiceSpeaker : MonoBehaviour
{
    #region Singleton
    public static TeacherVoiceSpeaker Singleton
    {
        get;
        protected set;
    }

    #endregion

    #region Basic Settigns
    public List<SpeakerLine> allLines = new List<SpeakerLine>();
    #endregion

    #region Run time data
    AudioSource m_source;
    Dictionary<string, SpeakerLine> lineCache = new Dictionary<string, SpeakerLine>();

    public bool IsSpeaking
    {
        get
        {
            return m_source.isPlaying;
        }
    }
    #endregion

    #region LifeCircle
    private void Awake()
    {
        if (Singleton != null&&Singleton != this)
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
        BuildLineCache();
        m_source = GetComponent<AudioSource>();
        m_source.playOnAwake = false;
        m_source.loop = false;
    }

    bool last_SpeakingStatus = false;
    // Update is called once per frame
    void Update()
    {
        if (!IsSpeaking && last_SpeakingStatus == true)
        {
            OnLineSpeakEnded();
        }

        last_SpeakingStatus = IsSpeaking;
    }

    private void OnEnable()
    {
        GlobalEventManager.RegisterHandler("TeacherVoiceSpeaker.StopSpeaking", StopSpeaking);
    }

    private void OnDisable()
    {
        GlobalEventManager.UnregisterHandler("TeacherVoiceSpeaker.StopSpeaking", StopSpeaking);
    }
    #endregion 

    void BuildLineCache()
    {
        lineCache.Clear();
        foreach (var l in allLines)
        {
            lineCache.Add(l.index, l);
        }
    }

    string currentLine;
    public void Speak(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }
        StopSpeaking();
        SpeakerLine l = lineCache[key];

        if (l == null)
        {
            return;
        }
        currentLine = key;
        m_source.clip = l.clip;
        m_source.Play();
        OnLineSpeakStarted(l);
    }

    public void StopSpeaking()
    {
        if (m_source.isPlaying)
        {
            m_source.Stop();
        }
    }

    public void StopSpeaking(GlobalEvent evt)
    {
        StopSpeaking();
    }

    void OnLineSpeakStarted(SpeakerLine line)
    {
        if (string.IsNullOrEmpty(line.description) || !line.showLine)
        {
            return;
        }

        SubtitleUI.Singleton.PushLine(line.description, line.clip);
    }

    void OnLineSpeakEnded()
    {
    }
}
