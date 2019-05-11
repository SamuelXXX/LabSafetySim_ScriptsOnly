using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotator : MonoBehaviour
{
    public AnimationCurve openCurve;
    #region Life Circle
    // Use this for initialization
    void Start()
    {
        GlobalEventManager.RegisterHandler("Door.Open", Open);
        GlobalEventManager.RegisterHandler("Door.Close", Close);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GlobalEventManager.UnregisterHandler("Door.Open", Open);
        GlobalEventManager.UnregisterHandler("Door.Close", Close);
    }
    #endregion

    #region Global Event
    public void Open()
    {
        if(GetComponent<AudioSource>()!=null)
        {
            GetComponent<AudioSource>().Play();
        }
        StartCoroutine(OpenRoutine());
    }

    IEnumerator OpenRoutine()
    {
        Quaternion originalRotation = transform.rotation;
        float timer = 0f;
        while (timer < 1f)
        {
            transform.rotation = Quaternion.Lerp(originalRotation, fullOpenRotation, openCurve.Evaluate(timer));
            timer += Time.deltaTime * 1f;
            yield return null;
        }
        transform.rotation = fullOpenRotation;
    }

    public void Close()
    {
        StopAllCoroutines();
        transform.rotation = fullCloseRotation;
    }
    #endregion

    #region Build Methods
    [SerializeField]
    protected bool allowBuild;

    [SerializeField, HideInInspector]
    protected Quaternion fullOpenRotation;

    [SerializeField, HideInInspector]
    protected Quaternion fullCloseRotation;

    [ContextMenu("Build Full Open")]
    void BuildFullOpen()
    {
        if (!allowBuild)
            return;
        fullOpenRotation = transform.rotation;
        allowBuild = false;
    }

    [ContextMenu("Build Full Close")]
    void BuildFullClose()
    {
        if (!allowBuild)
            return;
        fullCloseRotation = transform.rotation;
        allowBuild = false;
    }

    [ContextMenu("Apply Full Close")]
    void ApplyFullClose()
    {
        transform.rotation = fullCloseRotation;
    }

    [ContextMenu("Apply Full Open")]
    void ApplyFullOpen()
    {
        transform.rotation = fullOpenRotation;
    }
    #endregion
}
