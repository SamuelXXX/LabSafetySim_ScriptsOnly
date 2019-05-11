// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("GuidingSpeaker")]
    [Tooltip("Wait guiding speaking finished.")]
    public class WaitSpeakFinished : FsmStateAction
    {
        public override void OnUpdate()
        {
            if(!GuidingSpeaker.Singleton.IsSpeaking)
            {
                Finish();
            }
        }
    }
}