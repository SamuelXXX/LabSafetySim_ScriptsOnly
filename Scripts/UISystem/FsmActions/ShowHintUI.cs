// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("HintUI")]
    [Tooltip("Show hint UI.")]
    public class ShowHintUI : FsmStateAction
    {
        public FsmString uiTitle = "hehehe";
        public FsmString uiContent = "hahaha";
        public FsmBool waitConfirm = true;

        public override void OnEnter()
        {
            HintUI.Singleton.ShowUI(uiTitle.Value, uiContent.Value.Replace("\\n", "\n").Trim());
            if (!waitConfirm.Value)
                Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (waitConfirm.Value && GlobalEventManager.PeekEvent("HintUI.Confirmed"))
            {
                Finish();
            }
        }
    }
}