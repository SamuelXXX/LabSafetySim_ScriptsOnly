using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleTeacherAI : MonoBehaviour
{
    [Header("Pose Settings")]
    public float maxLookAtIKWeight = 0.8f;

    [Header("Cloth Settings")]
    public GameObject glove;
    public GameObject uniform;
    public GameObject sweater;
    public GameObject pants;

    protected IKSolverLookAt m_IKLookAtSolver;

    #region Life Circle
    // Use this for initialization
    void Start()
    {
        if (GetComponentInChildren<LookAtIK>() != null)
            m_IKLookAtSolver = GetComponentInChildren<LookAtIK>().solver;

        GlobalEventManager.RegisterHandler("Teacher.GazePlayer", GazePlayer);
        GlobalEventManager.RegisterHandler("Teacher.CancelGaze", CancelGaze);
        GlobalEventManager.RegisterHandler("Teacher.DressUp", DressUp);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GlobalEventManager.UnregisterHandler("Teacher.GazePlayer", GazePlayer);
        GlobalEventManager.UnregisterHandler("Teacher.CancelGaze", CancelGaze);
        GlobalEventManager.UnregisterHandler("Teacher.DressUp", DressUp);
    }
    #endregion

    #region External Methods
    float currentGazeWeight = 0;
    public void GazePlayer()
    {
        Gaze(VRPlayer.Instance.Head);
    }

    public void DressUp()
    {
        glove.gameObject.SetActive(true);
        uniform.gameObject.SetActive(true);

        sweater.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Cutoff", 0.8f);
        pants.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Cutoff", 0.8f);
    }

    public void Gaze(Transform target)
    {
        StopAllCoroutines();
        if (m_IKLookAtSolver != null)
        {
            m_IKLookAtSolver.SetLookAtWeight(currentGazeWeight);
            m_IKLookAtSolver.target = target;
            StartCoroutine(GazeRoutine());
        }
    }

    IEnumerator GazeRoutine()
    {
        while (currentGazeWeight < maxLookAtIKWeight)
        {
            currentGazeWeight += Time.deltaTime * 4f;
            m_IKLookAtSolver.SetLookAtWeight(currentGazeWeight);
            yield return null;
        }
        currentGazeWeight = maxLookAtIKWeight;
        m_IKLookAtSolver.SetLookAtWeight(currentGazeWeight);
        yield return null;
    }

    public void CancelGaze()
    {
        StopAllCoroutines();
        if (m_IKLookAtSolver != null)
        {
            m_IKLookAtSolver.SetLookAtWeight(currentGazeWeight);
            StartCoroutine(CancelGazeRoutine());
        }
    }

    IEnumerator CancelGazeRoutine()
    {
        while (currentGazeWeight > 0)
        {
            currentGazeWeight -= Time.deltaTime * 4f;
            m_IKLookAtSolver.SetLookAtWeight(currentGazeWeight);
            yield return null;
        }
        currentGazeWeight = 0;
        m_IKLookAtSolver.SetLookAtWeight(0);
        m_IKLookAtSolver.target = null;
        yield return null;
    }
    #endregion
}
