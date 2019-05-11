using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InteractingMethod
{
    Click=0,
    Grab
}

public class VRInteractable : MonoBehaviour
{
    #region Interacting Event
    public InteractingMethod interactingMethod = InteractingMethod.Click;
    [System.Serializable]
    public class InteractionEvent : UnityEvent<VRInteractor> { }
    public InteractionEvent OnHoveredInEvent;
    public InteractionEvent OnHoveredOutEvent;
    public InteractionEvent OnInteractionStartEvent;
    public InteractionEvent OnInteractionFinishedEvent;

    public void OnHoveredIn(VRInteractor interactor)
    {
        if (OnHoveredInEvent != null)
        {
            OnHoveredInEvent.Invoke(interactor);
        }
    }

    public void OnHoveredOut(VRInteractor interactor)
    {
        if (OnHoveredOutEvent != null)
        {
            OnHoveredOutEvent.Invoke(interactor);
        }
    }

    public void OnInteractionStart(VRInteractor interactor)
    {
        if (OnInteractionStartEvent != null)
        {
            OnInteractionStartEvent.Invoke(interactor);
        }
    }

    public void OnInteractionFinished(VRInteractor interactor)
    {
        if (OnInteractionFinishedEvent != null)
        {
            OnInteractionFinishedEvent.Invoke(interactor);
        }
    }
    #endregion
}
