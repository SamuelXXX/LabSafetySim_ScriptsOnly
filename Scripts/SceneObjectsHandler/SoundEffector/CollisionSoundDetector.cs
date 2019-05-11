using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundDetector : MonoBehaviour
{
    public string collisionSoundIndex;
    public LayerMask targetLayer;

    [Header("Collision Settings")]
    public Vector3 detectDirection = new Vector3(0, -1, 0);
    public float enterLength = 0.01f;
    public float leaveLength = 0.02f;


    public bool touching = true;
    protected Collider mCollider;
    // Use this for initialization
    void Start()
    {
        mCollider = GetComponent<Collider>();
        Invoke("SetReady", 1f);
    }


    // Update is called once per frame
    void Update()
    {
        if (mCollider == null)
        {
            if (touching)
            {
                if (!Physics.Raycast(transform.position, detectDirection, leaveLength, targetLayer))
                {
                    touching = false;
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, detectDirection, enterLength, targetLayer))
                {
                    touching = true;
                    SoundEffector.Singleton.Play(collisionSoundIndex, transform);
                }
            }
        }
    }

    void SetReady()
    {
        ready = true;
    }

    bool ready = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!ready)
            return;
        if ((1 << other.gameObject.layer & targetLayer) != 0)
            SoundEffector.Singleton.Play(collisionSoundIndex, transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!ready)
            return;
        if ((1 << collision.collider.gameObject.layer & targetLayer) != 0)
            SoundEffector.Singleton.Play(collisionSoundIndex, transform, collision.impulse.magnitude / 5f);
    }

    private void OnValidate()
    {
        if (leaveLength < enterLength)
        {
            leaveLength = enterLength + 0.001f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + detectDirection.normalized * enterLength);
    }
}
