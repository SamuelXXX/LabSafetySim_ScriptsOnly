using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(VRInteractable))]
public class VRToggle : MonoBehaviour
{
    public Color normalColor;
    public Color hoveredColor;
    public Color toggleColor;

    public float transitionTime = 0.2f;

    [System.Serializable]
    public class HandToggleEvent : UnityEvent { }
    public HandToggleEvent onHandToggleOn;
    public HandToggleEvent onHandToggleOff;

    Image mImage;
    VRInteractable mInteractable;
    Color targetColor;
    bool isHovering = false;
    bool isToggleOn = false;

    void Awake()
    {
        mImage = GetComponent<Image>();
        mInteractable = GetComponent<VRInteractable>();
        mImage.color = normalColor;
        mInteractable.OnHoveredInEvent.AddListener(OnHoverIn);
        mInteractable.OnHoveredOutEvent.AddListener(OnHoverOut);
        //mInteractable.OnInteractionStartEvent.AddListener(InteractingStart);
        mInteractable.OnInteractionFinishedEvent.AddListener(InteractionStop);
    }

    private void OnToggleOn()
    {
        onHandToggleOn.Invoke();
    }

    private void OnToggleOff()
    {
        onHandToggleOff.Invoke();
    }

    VRInteractor interactor = null;

    private void InteractionStop(VRInteractor interactor)
    {
        isToggleOn = !isToggleOn;
        if (isToggleOn)
        {
            OnToggleOn();
        }
        else
        {
            OnToggleOff();
        }
        this.interactor = null;
    }

    private void OnHoverIn(VRInteractor interactor)
    {
        if (interactor != null)
        {
            interactor.TriggerVibFeedback(1000);
        }
        isHovering = true;
    }

    private void OnHoverOut(VRInteractor interactor)
    {
        isHovering = false;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            if (isToggleOn)
            {
                targetColor = toggleColor;
            }
            else
            {
                targetColor = hoveredColor;
            }
        }
        else
        {
            if (isToggleOn)
            {
                targetColor = toggleColor;
            }
            else
            {
                targetColor = normalColor;
            }
        }

        UpdateColor();
    }

    void UpdateColor()
    {
        var color = mImage.color;
        color = Color.Lerp(color, targetColor, Time.deltaTime * (1 / transitionTime));
        mImage.color = color;
    }
}
