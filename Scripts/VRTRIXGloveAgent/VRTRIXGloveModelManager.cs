using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTRIXGloveModelManager : MonoBehaviour {
    public VRTRIXGloveModel leftHand;
    public VRTRIXGloveModel rightHand;

    public static VRTRIXGloveModelManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        leftHand.gameObject.SetActive(false);
        rightHand.gameObject.SetActive(false);
    }
}
