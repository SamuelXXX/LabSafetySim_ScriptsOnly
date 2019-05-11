using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FracturedObject))]
public class FractureObjectTester : MonoBehaviour
{
    public GameObject explosionEffect;
    protected FracturedObject mObject;
    public GameObject model;
    public float explodeForce = 20f;
    // Use this for initialization
    void Start()
    {
        mObject = GetComponent<FracturedObject>();
        GlobalEventManager.RegisterHandler("Bottle.Explode", ExplodeChunks);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GlobalEventManager.UnregisterHandler("Bottle.Explode", ExplodeChunks);
    }

    [ContextMenu("Explode")]
    void ExplodeChunks()
    {
        mObject.Explode(transform.position, explodeForce);
        GetComponent<AudioSource>().Play();
        GameObject exp = Instantiate(explosionEffect);
        exp.transform.position = transform.position;
        if (model != null)
        {
            model.SetActive(false);
        }
    }
}
