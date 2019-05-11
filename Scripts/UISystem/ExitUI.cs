using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    public Text titleText;
    public Text contentText;
    protected Canvas mCanvas;
    protected PlayMakerFSM effectFSM;
    protected VRUIPositioner positioner;

    #region Singleton
    protected static ExitUI singleton;
    public static ExitUI Singleton
    {
        get
        {
            return singleton;
        }
    }

    #endregion

    #region LifeCircle
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        mCanvas = GetComponent<Canvas>();
        effectFSM = GetComponent<PlayMakerFSM>();
        positioner = GetComponent<VRUIPositioner>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Control Methods
    public void SwitchUI()
    {
        if(on)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    bool on = false;
    public void ShowUI()
    {
        if (effectFSM != null)
        {
            effectFSM.SendEvent("Show");
        }

        positioner.SetPositionImmediately();
        SoundEffector.Singleton.Play("HintUI", transform, 0.2f);
        on = true;
    }

    public void HideUI()
    {
        on = false;
        if (effectFSM != null)
        {
            effectFSM.SendEvent("Hide");
        }
    }
    #endregion
}
