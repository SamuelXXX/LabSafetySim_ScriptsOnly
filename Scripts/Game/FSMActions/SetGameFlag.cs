// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game")]
    public class SetGameFlag : FsmStateAction
    {
        public FsmString flagName;
        public FsmBool value;

        public override void OnEnter()
        {
            GameRunDataHolder.SetFlag(flagName.Value, value.Value);
            Finish();
        }

    }
}
