// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UISystem")]
    public class AlphaTurningMaterial : FsmStateAction
    {
        [RequiredField]
        public FsmFloat time;
        public FsmColor targetColor;
        public FsmEvent finishEvent;
        public FsmString colorProperty = "_Color";
        public FsmGameObject targetObject;
        protected MeshRenderer[] targetRenderers;

        private float timer;
        Color[] originalColor;

        void BuildTarget()
        {
            targetRenderers = targetObject.Value.GetComponentsInChildren<MeshRenderer>();
            originalColor = new Color[targetRenderers.Length];
            for (int i = 0; i < targetRenderers.Length; i++)
            {
                originalColor[i] = targetRenderers[i].material.GetColor(colorProperty.Value);
            }
        }

        void SetColor(float timer)
        {

            for (int i = 0; i < targetRenderers.Length; i++)
            {
                targetRenderers[i].material.SetColor(colorProperty.Value, Color.Lerp(originalColor[i], targetColor.Value, timer));
            }
        }


        public override void Reset()
        {
            time = 1f;
            finishEvent = null;
        }

        public override void OnEnter()
        {

            BuildTarget();

            if (time.Value <= 0)
            {
                Fsm.Event(finishEvent);
                SetColor(1f);
                Finish();
                return;
            }

            timer = 0f;
        }

        public override void OnUpdate()
        {
            // update time
            timer += Time.deltaTime;

            if (timer >= time.Value)
            {
                SetColor(1f);
                Finish();
                if (finishEvent != null)
                {
                    Fsm.Event(finishEvent);
                }
            }
            else
            {
                float normalize = timer / time.Value;
                SetColor(normalize);
            }
        }

    }
}
