using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRTRIX;

[RequireComponent(typeof(SteamVR_TrackedObject))]
[RequireComponent(typeof(VRTRIXGloveAgent))]
public class VRTRIXGloveInput : MonoBehaviour
{
    public bool InputValid
    {
        get
        {
            return mGloveAgent.handType != HANDTYPE.NONE;
        }
    }

    protected SteamVR_TrackedObject mTrackedObject;
    protected VRTRIXGloveAgent mGloveAgent;
    private VRTRIXGloveGestureRecognition GloveGesture;

    private void Awake()
    {
        mTrackedObject = GetComponent<SteamVR_TrackedObject>();
        mGloveAgent = GetComponent<VRTRIXGloveAgent>();
        GloveGesture = new VRTRIXGloveGestureRecognition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGestureData();
    }

    public VRTRIXGloveGesture GetGesture()
    {
        if (mGloveAgent.handType == HANDTYPE.NONE || mGloveAgent == null)
        {
            return VRTRIXGloveGesture.BUTTONNONE;
        }
        else
        {
            return mGloveAgent.GetGesture();
        }
    }

    #region Gesture 
    VRTRIXGloveGesture lastGesture = VRTRIXGloveGesture.BUTTONNONE;
    VRTRIXGloveGesture currentGesture = VRTRIXGloveGesture.BUTTONNONE;


    void UpdateGestureData()
    {
        lastGesture = currentGesture;
        currentGesture = GetGesture();
    }

    public bool GetGestureEnter(VRTRIXGloveGesture gesture)
    {
        return lastGesture != gesture && currentGesture == gesture;
    }

    public bool GetGestureStay(VRTRIXGloveGesture gesture)
    {
        return currentGesture == gesture;
    }

    public bool GetGestureExit(VRTRIXGloveGesture gesture)
    {
        return lastGesture == gesture && currentGesture != gesture;
    }
    #endregion
}
