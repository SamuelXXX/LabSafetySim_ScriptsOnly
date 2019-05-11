using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StaticSceneHelper : MonoBehaviour
{
    public float lightmapScale = 0.1f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Kill All Colliders")]
    public void KillAllCollider()
    {
#if UNITY_EDITOR
        Collider[] colliders = GetComponentsInChildren<Collider>(true);
        foreach (var c in colliders)
        {
            DestroyImmediate(c);
        }
#endif
    }
}
