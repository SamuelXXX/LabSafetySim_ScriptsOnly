// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("VRPlayer")]
    public class TransportPlayerRig : FsmStateAction
    {
        public Transform targetPoint;
        public FsmBool maintainRotation=false;

        public override void OnEnter()
        {
            if(targetPoint!=null)
            {
                if(maintainRotation.Value)
                {
                    VRPlayer.Instance.Body.position=targetPoint.position;
                }
                else
                {
                    VRPlayer.Instance.Body.position = targetPoint.position;
                    VRPlayer.Instance.Body.rotation = targetPoint.rotation;
                }
            }
            Finish();
        }

    }
}
