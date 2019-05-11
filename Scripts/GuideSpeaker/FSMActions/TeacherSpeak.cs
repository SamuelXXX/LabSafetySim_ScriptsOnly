// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("GuidingSpeaker")]
    [Tooltip("Teacher speak.")]
    public class TeacherSpeak : FsmStateAction
    {
        public FsmString lineIndex = "StartLine";
        public FsmBool waitFinished = true;

        public override void OnEnter()
        {
            TeacherVoiceSpeaker.Singleton.Speak(lineIndex.Value);
            if (!waitFinished.Value)
                Finish();
        }

        public override void OnUpdate()
        {
            if (waitFinished.Value && !TeacherVoiceSpeaker.Singleton.IsSpeaking)
            {
                Finish();
            }
        }
    }
}