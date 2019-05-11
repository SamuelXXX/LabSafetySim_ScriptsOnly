// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game")]
    public class CheckGameInteger : FsmStateAction
    {
        public FsmString dataName;
        public FsmInt compareValue = 1;
        public FsmEvent equalEvent;
        public FsmEvent notEqualEvent;

        public override void OnEnter()
        {
            int v = GameRunDataHolder.GetType(dataName.Value);
            if (v == compareValue.Value)
            {
                Fsm.Event(equalEvent);
            }
            else
            {
                Fsm.Event(notEqualEvent);
            }
            Finish();
        }

    }
}
