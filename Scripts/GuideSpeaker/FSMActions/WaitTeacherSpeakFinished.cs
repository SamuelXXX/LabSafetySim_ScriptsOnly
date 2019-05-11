// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("GuidingSpeaker")]
    [Tooltip("Wait teacher speaking finished.")]
    public class WaitTeacherSpeakFinished : FsmStateAction
    {
        public override void OnUpdate()
        {
            if (!TeacherVoiceSpeaker.Singleton.IsSpeaking)
            {
                Finish();
            }
        }
    }
}