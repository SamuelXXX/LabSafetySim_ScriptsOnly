// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SoundEffector")]
    public class PlaySoundEffect : FsmStateAction
    {
        public FsmString soundIndex = "StartLine";
        public FsmGameObject followTarget;
        public FsmFloat volume = 1f;
        public FsmBool waitFinished = false;

        SoundEffectPlayer player;

        public override void OnEnter()
        {
            player = SoundEffector.Singleton.Play(soundIndex.Value, followTarget.Value == null ? null : followTarget.Value.transform, volume.Value);
            if (player == null)
            {
                Finish();
                return;
            }

            if (!waitFinished.Value)
            {
                Finish();
                return;
            }
        }

        public override void OnUpdate()
        {
            if (!player.IsPlaying)
            {
                Finish();
            }
        }
    }
}