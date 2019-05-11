// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("GuidingSpeaker")]
    [Tooltip("Guider speak.")]
    public class GuideSpeak : FsmStateAction
    {
        public FsmString lineIndex = "StartLine";
        public FsmBool waitFinished = true;

        public override void OnEnter()
        {
            GuidingSpeaker.Singleton.Speak(lineIndex.Value);
            if (!waitFinished.Value)
                Finish();
        }

        public override void OnUpdate()
        {
            if (waitFinished.Value && !GuidingSpeaker.Singleton.IsSpeaking)
            {
                Finish();
            }
        }
    }
}