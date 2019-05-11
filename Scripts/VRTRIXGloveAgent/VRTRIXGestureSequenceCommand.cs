using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTRIX;

[RequireComponent(typeof(VRTRIXGloveAgent))]
public class VRTRIXGestureSequenceCommand : MonoBehaviour
{
    public VRTRIXGloveGesture preGesture;
    public VRTRIXGloveGesture afterGesture;
    public string gestureEvent;

    VRTRIXGloveAgent mAgent;

    private void Awake()
    {
        mAgent = GetComponent<VRTRIXGloveAgent>();
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateGesture();
    }

    int fulfilCount = 0;
    void UpdateGesture()
    {
        VRTRIXGloveGesture ges = mAgent.GetGesture();
        if (ges == preGesture)
        {
            fulfilCount = 1;
            return;
        }

        if (ges == afterGesture)
        {
            if(fulfilCount==1)
            {
                fulfilCount = 0;
                OnCommand();
                return;
            }
        }

        if (ges != VRTRIXGloveGesture.BUTTONNONE)
            fulfilCount = 0;
    }

    void OnCommand()
    {
        GlobalEventManager.SendEvent(gestureEvent);
    }
}
