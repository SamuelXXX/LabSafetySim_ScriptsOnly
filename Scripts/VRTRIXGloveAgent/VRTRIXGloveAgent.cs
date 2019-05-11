using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRTRIX;
using Valve.VR;

public class VRTRIXGloveAgent : MonoBehaviour
{
    #region Settings
    public bool AdvancedMode;
    public Vector3 modelOffset;
    public HANDTYPE handType = HANDTYPE.NONE;
    #endregion

    #region Runtime data
    private SteamVR_TrackedObject tracker;
    private VRTRIXGloveModel model;
    private bool gloveConnected = false;
    private VRTRIXDataWrapper gloveDataWrapper;
    private VRTRIXGloveGestureRecognition GloveGesture;

    private Vector3 troffset = new Vector3(0.01f, 0, -0.035f);
    private Vector3 tloffset = new Vector3(-0.01f, 0, -0.035f);

    private float qloffset, qroffset;
    private bool qloffset_cal, qroffset_cal;
    float qOffset;

    public Transform Finger
    {
        get
        {
            if (model != null)
                return model.finger;

            return null;
        }
    }
    #endregion

    #region Life Circle
    private void Awake()
    {
        tracker = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        gloveDataWrapper = new VRTRIXDataWrapper(AdvancedMode);
        GloveGesture = new VRTRIXGloveGestureRecognition();
    }

    //数据更新与骨骼赋值。
    void Update()
    {
        if (tracker != null)
        {
            CheckTrackerEveryFrame();
        }
        if (gloveConnected && gloveDataWrapper.GetReceivedStatus() == VRTRIXGloveStatus.NORMAL && handType == HANDTYPE.RIGHT_HAND)
        {
            SetPosition(VRTRIXBones.R_Hand, tracker.transform.position, tracker.transform.rotation, troffset);

            SetRotation(VRTRIXBones.R_Forearm, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Forearm), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Forearm));
            SetRotation(VRTRIXBones.R_Hand, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Hand), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Hand));

            SetRotation(VRTRIXBones.R_Thumb_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Thumb_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Thumb_1));
            SetRotation(VRTRIXBones.R_Thumb_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Thumb_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Thumb_2));
            SetRotation(VRTRIXBones.R_Thumb_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Thumb_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Thumb_3));

            SetRotation(VRTRIXBones.R_Index_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Index_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Index_1));
            SetRotation(VRTRIXBones.R_Index_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Index_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Index_2));
            SetRotation(VRTRIXBones.R_Index_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Index_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Index_3));

            SetRotation(VRTRIXBones.R_Middle_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Middle_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Middle_1));
            SetRotation(VRTRIXBones.R_Middle_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Middle_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Middle_2));
            SetRotation(VRTRIXBones.R_Middle_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Middle_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Middle_3));

            SetRotation(VRTRIXBones.R_Ring_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Ring_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Ring_1));
            SetRotation(VRTRIXBones.R_Ring_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Ring_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Ring_2));
            SetRotation(VRTRIXBones.R_Ring_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Ring_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Ring_3));

            SetRotation(VRTRIXBones.R_Pinky_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Pinky_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Pinky_1));
            SetRotation(VRTRIXBones.R_Pinky_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Pinky_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Pinky_2));
            SetRotation(VRTRIXBones.R_Pinky_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Pinky_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.R_Pinky_3));
        }

        if (gloveConnected && gloveDataWrapper.GetReceivedStatus() == VRTRIXGloveStatus.NORMAL && handType == HANDTYPE.LEFT_HAND)
        {

            SetPosition(VRTRIXBones.L_Hand, tracker.transform.position, tracker.transform.rotation, tloffset);

            SetRotation(VRTRIXBones.L_Forearm, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Forearm), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Forearm));
            SetRotation(VRTRIXBones.L_Hand, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Hand), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Hand));

            SetRotation(VRTRIXBones.L_Thumb_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Thumb_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Thumb_1));
            SetRotation(VRTRIXBones.L_Thumb_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Thumb_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Thumb_2));
            SetRotation(VRTRIXBones.L_Thumb_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Thumb_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Thumb_3));

            SetRotation(VRTRIXBones.L_Index_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Index_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Index_1));
            SetRotation(VRTRIXBones.L_Index_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Index_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Index_2));
            SetRotation(VRTRIXBones.L_Index_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Index_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Index_3));

            SetRotation(VRTRIXBones.L_Middle_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Middle_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Middle_1));
            SetRotation(VRTRIXBones.L_Middle_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Middle_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Middle_2));
            SetRotation(VRTRIXBones.L_Middle_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Middle_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Middle_3));

            SetRotation(VRTRIXBones.L_Ring_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Ring_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Ring_1));
            SetRotation(VRTRIXBones.L_Ring_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Ring_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Ring_2));
            SetRotation(VRTRIXBones.L_Ring_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Ring_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Ring_3));

            SetRotation(VRTRIXBones.L_Pinky_1, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Pinky_1), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Pinky_1));
            SetRotation(VRTRIXBones.L_Pinky_2, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Pinky_2), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Pinky_2));
            SetRotation(VRTRIXBones.L_Pinky_3, gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Pinky_3), gloveDataWrapper.DataValidStatus(VRTRIXBones.L_Pinky_3));
        }
    }

    void OnApplicationQuit()
    {
        if (gloveConnected && gloveDataWrapper.GetReceivedStatus() != VRTRIXGloveStatus.CLOSED)
        {
            gloveDataWrapper.ClosePort();
        }
    }
    #endregion

    #region Global Event Handling
    bool trackerValid = false;
    void CheckTrackerEveryFrame()
    {
        var valid = tracker.isValid;
        if (valid != trackerValid)
        {
            if (valid)
            {
                OnTrackerActivated();
            }
            else
            {
                OnTrackerDeactivated();
            }
            trackerValid = valid;
        }
    }

    void OnTrackerActivated()
    {
        if (CheckGlovePairedTrackerOn())//Is using glove trackers
        {
            Debug.Log("Glove trackers detected, connecting now!");
            ConnectGlove();
            if (!gloveConnected)
            {
                if (handType == HANDTYPE.LEFT_HAND)
                {
                    Debug.Log("Left Hand Connect Failed!Will keep try connecting!");
                }
                else if (handType == HANDTYPE.RIGHT_HAND)
                {
                    Debug.Log("Right Hand Connected Failed!Will keep try connecting!");
                }
                StartCoroutine(ConnectTryRoutine());
                return;
            }
            if (handType == HANDTYPE.LEFT_HAND)
            {
                Debug.Log("Left Hand Connected!");
            }
            else if (handType == HANDTYPE.RIGHT_HAND)
            {
                Debug.Log("Right Hand Connected!");
            }
        }
        else//No glove trackers found
        {
            Debug.Log("Glove trackers not detected");
        }
    }

    void OnTrackerDeactivated()
    {
        DisconnectGlove();
        if (model)
        {
            model.gameObject.SetActive(false);
            model.transform.parent = VRTRIXGloveModelManager.Instance.transform;
        }
    }

    void OnGloveConnected()
    {
        GameRunDataHolder.SetType("InteractingSource", (int)InteractingSource.VRGlove);
        Teleporter teleporter = GetComponent<Teleporter>();
        if (teleporter != null && model != null)
        {
            teleporter.LineDrawer.emittingPoint = model.finger;
        }

        VRInteractor interactor = GetComponent<VRInteractor>();
        if(interactor!=null&&model!=null)
        {
            interactor.acquirePoint = model.finger;
            transform.Find("Contactor").gameObject.SetActive(false);
        }
    }

    void OnGloveDisconnected()
    {

    }

    /// <summary>
    /// Call this method when tracker is activated
    /// </summary>
    void OnTrackerReady()
    {
        if (CheckGlovePairedTrackerOn())//Is using glove trackers
        {
            Debug.Log("Glove trackers detected, connecting now!");
            ConnectGlove();
            if (!gloveConnected)
            {
                if (handType == HANDTYPE.LEFT_HAND)
                {
                    Debug.Log("Left Hand Connect Failed!Will keep try connecting!");
                }
                else if (handType == HANDTYPE.RIGHT_HAND)
                {
                    Debug.Log("Right Hand Connected Failed!Will keep try connecting!");
                }
                StartCoroutine(ConnectTryRoutine());
                return;
            }
            if (handType == HANDTYPE.LEFT_HAND)
            {
                Debug.Log("Left Hand Connected!");
            }
            else if (handType == HANDTYPE.RIGHT_HAND)
            {
                Debug.Log("Right Hand Connected!");
            }
        }
        else//No glove trackers found
        {
            Debug.Log("Glove trackers not detected");
        }
    }

    IEnumerator ConnectTryRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            ConnectGlove();
            if (gloveConnected)
                break;
        }
    }
    #endregion

    #region Glove Control
    //数据手套初始化，硬件连接
    [ContextMenu("Connect")]
    public void ConnectGlove()
    {
        try
        {
            if (gloveConnected)
                return;

            if (tracker != null)
            {
                gloveConnected = gloveDataWrapper.Init(handType);
                if (gloveConnected)
                {
                    OnGloveConnected();
                    gloveDataWrapper.registerCallBack();
                    gloveDataWrapper.startStreaming();
                }
            }
        }
        catch (Exception e)
        {
            print("Exception caught: " + e);
        }
    }

    //数据手套反初始化，硬件断开连接
    public void DisconnectGlove()
    {
        if (gloveConnected)
        {
            if (gloveDataWrapper.ClosePort())
            {
                gloveDataWrapper = new VRTRIXDataWrapper(AdvancedMode);
            }
            OnGloveDisconnected();
            gloveConnected = false;
        }
    }

    //数据手套硬件校准，仅在磁场大幅度变化后使用。
    public void HardwareCalibrate()
    {
        if (gloveConnected)
        {
            gloveDataWrapper.calibration();
        }
    }

    //数据手套振动
    public void Vibrate()
    {
        if (gloveConnected)
        {
            gloveDataWrapper.vibrate();
        }
    }

    //数据手套软件对齐手指，仅在磁场大幅度变化后使用。
    public void AlignFingers()
    {
        if (gloveConnected)
        {
            qOffset = Math.Abs(tracker.transform.rotation.eulerAngles.z) - 180;
            gloveDataWrapper.alignmentCheck(handType);
        }
    }
    #endregion

    #region Private Tool Methods
    Dictionary<string, Transform> nodeMapper = new Dictionary<string, Transform>();


    Transform FindNode(string name)
    {
        if (nodeMapper.ContainsKey(name))
            return nodeMapper[name];

        var go = GameObject.Find(name);
        if (go == null)
            nodeMapper.Add(name, null);
        else
            nodeMapper.Add(name, go.transform);

        return nodeMapper[name];
    }
    private void SetRotation(VRTRIXBones bone, Quaternion rotation, bool valid)
    {
        string bone_name = VRTRIXUtilities.GetBoneName((int)bone);

        Transform t = FindNode(bone_name);

        if (t != null)
        {
            if (!float.IsNaN(rotation.x) && !float.IsNaN(rotation.y) && !float.IsNaN(rotation.z) && !float.IsNaN(rotation.w))
            {
                if (valid)
                {
                    t.rotation = CalculateDynamicOffset() * (rotation * Quaternion.Euler(modelOffset));
                }
            }
        }
    }

    private void SetPosition(VRTRIXBones bone, Vector3 pos, Quaternion rot, Vector3 offset)
    {
        string bone_name = VRTRIXUtilities.GetBoneName((int)bone);
        Transform t = FindNode(bone_name);

        if (t != null)
        {
            t.position = pos + rot * offset;
        }
    }

    //用于计算左手/右手腕关节姿态（由动捕设备得到）和左手手背姿态（由数据手套得到）之间的四元数差值，该方法为动态调用，即每一帧都会调用该计算。
    //适用于：当动捕设备有腕关节/手背节点时
    private Quaternion CalculateDynamicOffset()
    {
        if (handType == HANDTYPE.RIGHT_HAND)
        {
            //MapToVRTRIX_BoneName: 此函数用于将任意的手部骨骼模型中关节名称转化为VRTRIX数据手套可识别的关节名称。
            //GameObject R_hand = MyHandsMapToVrtrixHand.UniqueStance.MapToVRTRIX_BoneName(BoneNameForR_hand);

            //计算场景中角色右手腕在unity世界坐标系下的旋转与手套的右手腕在手套追踪系统中世界坐标系下右手腕的旋转之间的角度差值，意在匹配两个坐标系的方向；
            //return tracker.transform.rotation * Quaternion.Inverse(glove.GetReceivedRotation(VRTRIXBones.R_Hand) * Quaternion.Euler(qr_modeloffset));
            Quaternion offsetTracker = (tracker.transform.rotation * new Quaternion(0, 1f, 0, 0)) * Quaternion.Inverse(gloveDataWrapper.GetReceivedRotation(VRTRIXBones.R_Hand) * Quaternion.Euler(modelOffset));
            if (!qroffset_cal)
            {
                qroffset = Math.Abs(tracker.transform.rotation.eulerAngles.z) - 180;
                qroffset_cal = true;
            }
            return Quaternion.AngleAxis(-qroffset, Vector3.Normalize(tracker.transform.rotation * Vector3.forward)) * offsetTracker;
        }
        else if (handType == HANDTYPE.LEFT_HAND)
        {
            //MapToVRTRIX_BoneName: 此函数用于将任意的手部骨骼模型中关节名称转化为VRTRIX数据手套可识别的关节名称。
            //GameObject L_hand = MyHandsMapToVrtrixHand.UniqueStance.MapToVRTRIX_BoneName(BoneNameForL_hand);

            //计算场景中角色左手腕在unity世界坐标系下的旋转与手套的左手腕在手套追踪系统中世界坐标系下左手腕的旋转之间的角度差值，意在匹配两个坐标系的方向；
            //return tracker.transform.rotation * Quaternion.Inverse(LH.GetReceivedRotation(VRTRIXBones.L_Hand) * Quaternion.Euler(ql_modeloffset));
            Quaternion offsetTracker = tracker.transform.rotation * Quaternion.Inverse(gloveDataWrapper.GetReceivedRotation(VRTRIXBones.L_Hand) * Quaternion.Euler(modelOffset));
            if (!qloffset_cal)
            {
                qloffset = Math.Abs(tracker.transform.rotation.eulerAngles.z) - 180;
                qloffset_cal = true;
            }
            return Quaternion.AngleAxis(-qloffset, tracker.transform.rotation * Vector3.forward) * offsetTracker;
        }
        else
        {
            return Quaternion.identity;
        }
    }
    #endregion

    #region Public Methods
    public VRTRIXGloveGesture GetGesture()
    {
        return GloveGesture.GestureDetection(gloveDataWrapper, HANDTYPE.LEFT_HAND);
    }

    /// <summary>
    /// Check if glove paired tracker is being used
    /// </summary>
    /// <returns></returns>
    public bool CheckGlovePairedTrackerOn()
    {
        var openVR = OpenVR.System;
        if (openVR == null)
            return false;

        var error = ETrackedPropertyError.TrackedProp_Success;
        var capacity = openVR.GetStringTrackedDeviceProperty((uint)tracker.index, ETrackedDeviceProperty.Prop_RenderModelName_String, null, 0, ref error);
        if (capacity <= 1)
        {
            return false;
        }

        var buffer = new System.Text.StringBuilder((int)capacity);
        openVR.GetStringTrackedDeviceProperty((uint)tracker.index, ETrackedDeviceProperty.Prop_RenderModelName_String, buffer, capacity, ref error);
        var s = buffer.ToString();

        if (s.Contains("LH"))
        {
            handType = HANDTYPE.LEFT_HAND;
            model = VRTRIXGloveModelManager.Instance.leftHand;
            model.transform.parent = transform;
            model.gameObject.SetActive(true);
            return true;
        }

        if (s.Contains("RH"))
        {
            handType = HANDTYPE.RIGHT_HAND;
            model = VRTRIXGloveModelManager.Instance.rightHand;
            model.transform.parent = transform;
            model.gameObject.SetActive(true);
            return true;
        }

        return false;
    }
    #endregion
}
