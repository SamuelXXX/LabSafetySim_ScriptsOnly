using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum VRActionType
{
    TriggerDown = 0,
    MenuDown,
    PadDown
}
[System.Serializable]
public class InputEvent
{
    public VRActionType actionType;
    public string actionEvent;
}

[RequireComponent(typeof(VRInteractor))]
public class VRInteractorInputListener : MonoBehaviour
{
    protected VRInteractor mInteractor;

    public List<InputEvent> inputEvents = new List<InputEvent>();

    private void Awake()
    {
        mInteractor = GetComponent<VRInteractor>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var e in inputEvents)
        {
            switch (e.actionType)
            {
                case VRActionType.TriggerDown:
                    if ((!VRPlayer.Instance.IsUsingSimulator && mInteractor.VRInput != null) ? mInteractor.VRInput.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) : Input.GetMouseButtonDown(0))
                    {
                        GlobalEventManager.SendEvent(e.actionEvent);
                    }
                    break;
                case VRActionType.MenuDown:
                    if ((!VRPlayer.Instance.IsUsingSimulator && mInteractor.VRInput != null) ? mInteractor.VRInput.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu) : Input.GetKeyDown(KeyCode.M))
                    {
                        GlobalEventManager.SendEvent(e.actionEvent);
                    }
                    break;
                case VRActionType.PadDown:
                    if ((!VRPlayer.Instance.IsUsingSimulator && mInteractor.VRInput != null) ? mInteractor.VRInput.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad) : Input.GetMouseButtonDown(1))
                    {
                        GlobalEventManager.SendEvent(e.actionEvent);
                    }
                    break;
                default: break;
            }

        }
    }
}
