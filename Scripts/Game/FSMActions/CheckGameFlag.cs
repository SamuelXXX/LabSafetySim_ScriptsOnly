// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game")]
    public class CheckGameFlag : FsmStateAction
    {
        public FsmString flagName;
        public FsmEvent trueEvent;
        public FsmEvent falseEvent;

        public override void OnEnter()
        {
            bool v = GameRunDataHolder.GetFlag(flagName.Value);
            if (v)
            {
                Fsm.Event(trueEvent);
            }
            else
            {
                Fsm.Event(falseEvent);
            }
            Finish();
        }

    }
}
