using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(VRInteractable))]
public class VRButtonImageSwapper : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite hoverImage;
    public Sprite clickImage;

    [System.Serializable]
    public class HandClickEvent : UnityEvent { }
    public HandClickEvent onHandClick;

    Image mImage;
    VRInteractable mInteractable;
    Color targetColor;
    bool isHovering = false;
    bool isClicking = false;

    void Awake()
    {
        mImage = GetComponent<Image>();
        mInteractable = GetComponent<VRInteractable>();
        mImage.sprite = normalImage;
        mInteractable.OnHoveredInEvent.AddListener(OnHoverIn);
        mInteractable.OnHoveredOutEvent.AddListener(OnHoverOut);
        mInteractable.OnInteractionStartEvent.AddListener(InteractingStart);
        mInteractable.OnInteractionFinishedEvent.AddListener(InteractionStop);
    }

    private void OnButtonClick()
    {
        onHandClick.Invoke();
    }

    VRInteractor interactor = null;
    private void InteractingStart(VRInteractor interactor)
    {
        this.interactor = interactor;
        isClicking = true;
    }

    private void InteractionStop(VRInteractor interactor)
    {
        isClicking = false;
        OnButtonClick();
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
            if (isClicking)
            {
                mImage.sprite = clickImage;
            }
            else
            {
                mImage.sprite = hoverImage;
            }
        }
        else
        {
            mImage.sprite = normalImage;
        }
    }
}
