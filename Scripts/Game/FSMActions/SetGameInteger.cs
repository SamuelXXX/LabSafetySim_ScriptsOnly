// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game")]
    public class SetGameInteger : FsmStateAction
    {
        public FsmString dataName;
        public FsmInt value;

        public override void OnEnter()
        {
            GameRunDataHolder.SetType(dataName.Value, value.Value);
            Finish();
        }

    }
}
