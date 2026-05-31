using UnityEngine;

namespace Nesur.Core.Animation {
    public class TrackAnimationExit : StateMachineBehaviour {
        private AnimationPlayer _cachedAnimationPlayer;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            AnimationPlayer animationPlayer = animator.GetComponent<AnimationPlayer>();
            if (animationPlayer != null) {
                if (_cachedAnimationPlayer == null) {
                    _cachedAnimationPlayer = animationPlayer;
                }
                animationPlayer.OnAnimationFinished();
            }
        }
    }
}