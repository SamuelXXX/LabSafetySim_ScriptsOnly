using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRInteractable))]
public class VRPickable : MonoBehaviour
{
    public bool backtoStartPosition = true;
    public string pickEvent;
    public string dropEvent;
    protected VRInteractable mInteractable;
    protected Vector3 startPosition;
    protected Quaternion startRotation;
    private void Awake()
    {
        mInteractable = GetComponent<VRInteractable>();
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        mInteractable.OnInteractionStartEvent.AddListener(OnStartInteracting);
        mInteractable.OnInteractionFinishedEvent.AddListener(OnStopInteracting);
        if (backtoStartPosition)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 lastPosition;
    Vector3 lastVelocity;
    bool shouldApplyVelocity;
    private void LateUpdate()
    {
        if (backtoStartPosition)
        {
            if (mInteractor == null)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * 8f);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, Time.deltaTime * 8f);
            }
            else
            {
                transform.localRotation = mInteractor.acquirePoint.transform.rotation * relativeRotation;
                transform.localPosition = mInteractor.acquirePoint.transform.TransformPoint(relativePosition);
            }
        }
        else
        {
            if (mInteractor == null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                if (shouldApplyVelocity)
                {
                    shouldApplyVelocity = false;
                    GetComponent<Rigidbody>().velocity = lastVelocity;
                    Invoke("BackToStartPosition", 5f);
                }
            }
            else
            {
                shouldApplyVelocity = true;
                lastVelocity = (transform.position - lastPosition) / Time.deltaTime;
                lastPosition = transform.position;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.localRotation = mInteractor.acquirePoint.transform.rotation * relativeRotation;
                transform.localPosition = mInteractor.acquirePoint.transform.TransformPoint(relativePosition);
            }
        }

    }

    void BackToStartPosition()
    {
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
    }

    VRInteractor mInteractor = null;
    Vector3 relativePosition;
    Quaternion relativeRotation;
    void OnStartInteracting(VRInteractor interactor)
    {
        if (mInteractor != null)
            return;

        relativePosition = interactor.acquirePoint.InverseTransformPoint(transform.position);
        relativeRotation = Quaternion.Inverse(interactor.acquirePoint.rotation) * transform.rotation;
        mInteractor = interactor;
        if (!string.IsNullOrEmpty(pickEvent))
        {
            GlobalEventManager.SendEvent(pickEvent);
        }
    }

    void OnStopInteracting(VRInteractor interactor)
    {
        if (mInteractor != interactor)
            return;
        mInteractor = null;
        if (!string.IsNullOrEmpty(dropEvent))
        {
            GlobalEventManager.SendEvent(dropEvent);
        }
    }
}
