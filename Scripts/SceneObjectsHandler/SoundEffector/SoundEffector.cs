using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffector : MonoBehaviour
{
    public List<SoundEffectPack> soundPacks = new List<SoundEffectPack>();
    public SoundEffectPlayer playerPrefab;

    #region Singleton
    public static SoundEffector Singleton
    {
        get;
        protected set;
    }

    #endregion

    #region Life Circle
    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
        }
        else if (Singleton == null)
        {
            Singleton = this;
        }
    }

    private void OnDestroy()
    {
        if (Singleton == this)
            Singleton = null;
    }
    // Use this for initialization
    void Start()
    {
        BuildPackDict();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Dictionary
    Dictionary<string, SoundEffectPack> packsDict = new Dictionary<string, SoundEffectPack>();
    void BuildPackDict()
    {
        packsDict.Clear();
        foreach (var s in soundPacks)
        {
            if (string.IsNullOrEmpty(s.index))
                continue;
            if (packsDict.ContainsKey(s.index))
            {
                Debug.LogWarning("Same sound index found!!!");
                continue;
            }

            packsDict.Add(s.index, s);
        }
    }
    #endregion

    #region Players Management
    public Queue<SoundEffectPlayer> playerCache = new Queue<SoundEffectPlayer>();
    public void RecyclePlayer(SoundEffectPlayer player)
    {
        if (player == null)
            return;

        playerCache.Enqueue(player);
    }

    public SoundEffectPlayer Play(string index, Transform followTarget, float volume = 1f)
    {
        if (!packsDict.ContainsKey(index))
            return null;

        AudioClip clip = packsDict[index].GetRandomClip();
        SoundEffectPlayer player = null;
        if (playerCache.Count != 0)
            player = playerCache.Dequeue();
        else
            player = Instantiate(playerPrefab.gameObject).GetComponent<SoundEffectPlayer>();

        player.transform.parent = transform;
        player.target = followTarget;
        player.Play(clip, volume);
        return player;
    }
    #endregion
}
