using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ObjectHighlighter : MonoBehaviour
{
    public string objectName;
    public AnimationCurve highlightAnimation;
    protected MeshRenderer mRenderer;
    // Use this for initialization
    void Start()
    {
        GlobalEventManager.RegisterHandler(objectName + ".Highlight", ShowHighlight);
        GlobalEventManager.RegisterHandler(objectName + ".Dehighlight", HideHighlight);
        mRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighting)
        {
            mRenderer.enabled = true;
            currentValue += Time.deltaTime * 0.5f;
            if (currentValue > 1f)
            {
                currentValue -= 1f;
            }
            float v = highlightAnimation.Evaluate(currentValue) * 0.03f;
            float distance = Vector3.Distance(transform.position, VRPlayer.Instance.Head.position);
            mRenderer.sharedMaterial.SetFloat("g_flOutlineWidth", v / distance);
        }
        else
        {
            mRenderer.enabled = false;
            currentValue = 0;
        }
    }

    private void OnDestroy()
    {
        GlobalEventManager.UnregisterHandler(objectName + ".Highlight", ShowHighlight);
        GlobalEventManager.UnregisterHandler(objectName + ".Dehighlight", HideHighlight);
    }

    bool isHighlighting = false;
    float currentValue;
    public void ShowHighlight()
    {
        isHighlighting = true;
    }

    public void HideHighlight()
    {
        isHighlighting = false;
    }
}
