using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour
{
    public Transform target;

    protected AudioSource source;

    public bool IsPlaying
    {
        get
        {
            return source.isPlaying;
        }
    }


    #region Life Circle
    bool startPlayed = false;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {

    }

    private void LateUpdate()
    {
        if (source.isPlaying)
        {
            if (startPlayed == false)
            {
                startPlayed = true;
            }
            else
            {
                if (target != null)
                    transform.position = target.position;
            }
        }
        else
        {
            if (startPlayed)
            {
                RecycleSelf();
            }
        }
    }
    #endregion

    #region External Control
    void RecycleSelf()
    {
        startPlayed = false;
        source.Stop();
        SoundEffector.Singleton.RecyclePlayer(this);
    }

    public void Play(AudioClip clip, float volume)
    {
        source.PlayOneShot(clip, volume);
    }
    #endregion
}
