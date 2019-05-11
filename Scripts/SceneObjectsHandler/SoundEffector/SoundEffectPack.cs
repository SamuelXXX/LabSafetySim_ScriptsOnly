using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SoundEffectPack")]
public class SoundEffectPack : ScriptableObject {
    public string index;
    public List<AudioClip> clips;

    public AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Count)];
    }
}
