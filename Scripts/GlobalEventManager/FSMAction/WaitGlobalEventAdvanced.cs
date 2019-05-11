// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("GlobalEvent")]
    [Tooltip("Wait global event.")]
    public class WaitGlobalEventAdvanced : FsmStateAction
    {
        public FsmString eventName = "";
        public FsmBool ignorePastEvent = true;
        public FsmEvent succeedFsmEvent;

        public override void OnEnter()
        {
            if (ignorePastEvent.Value)
                GlobalEventManager.RegisterHandler(eventName.Value, EventHandler);
        }

        public override void OnUpdate()
        {
            if (!ignorePastEvent.Value && GlobalEventManager.PeekEvent(eventName.Value))
            {
                Fsm.Event(succeedFsmEvent);
                Finish();
            }
        }

        public override void OnExit()
        {
            if (ignorePastEvent.Value)
                GlobalEventManager.UnregisterHandler(eventName.Value, EventHandler);
        }

        void EventHandler()
        {
            Fsm.Event(succeedFsmEvent);
            Finish();
        }
    }
}