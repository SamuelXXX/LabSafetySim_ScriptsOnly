using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    public Text titleText;
    public Text contentText;
    protected Canvas mCanvas;
    protected PlayMakerFSM effectFSM;
    protected VRUIPositioner positioner;

    #region Singleton
    protected static HintUI singleton;
    public static HintUI Singleton
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
    public void ShowUI(string title, string content)
    {
        if (effectFSM != null)
        {
            effectFSM.SendEvent("Show");
        }
        if (titleText != null)
        {
            titleText.text = title;
        }
        if (contentText != null)
        {
            contentText.text = content;
        }

        positioner.SetPositionImmediately();
        SoundEffector.Singleton.Play("HintUI", transform, 0.2f);
    }

    public void ConfirmUI()
    {
        GlobalEventManager.SendEvent("HintUI.Confirmed");
        HideUI();
    }

    public void HideUI()
    {
        if (effectFSM != null)
        {
            effectFSM.SendEvent("Hide");
        }
    }
    #endregion
}
