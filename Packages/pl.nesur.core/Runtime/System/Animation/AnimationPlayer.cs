using System;
using UnityEngine;

namespace Nesur.Core.Animation {
    /// <summary>
    /// A component attached to visuals of an object that plays animations based on events received from an AnimationEventComponent.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimationPlayer : MonoBehaviour {
        public static EventHandler<string> OnSoundRequested;
        [SerializeField] private AnimationEventComponent animationEventComponent;

        private Animator _animator;

        public void OnAnimationStarted() {
            animationEventComponent.OnAnimationStarted();
        }

        public void OnAnimationFinished() {
            animationEventComponent.OnAnimationFinished();
        }

        public void PlayAnimationSound(string animationName) {
            OnSoundRequested?.Invoke(this, animationName);
        }

        private void PlayAnimation(string animationName) {
            _animator.Play(animationName);
        }

        private void PlayAnimationForceChange(string animationName) {
            _animator.Play(animationName, 0, 0f);
        }

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        private void Start() {
            animationEventComponent.OnAnimationEvent += AnimationEventComponentOnAnimationEvent;
        }

        private void AnimationEventComponentOnAnimationEvent(object sender,
            AnimationEventComponent.PlayAnimationEventArgs e) {
            if (e.ForceChange) {
                PlayAnimationForceChange(e.AnimationName.ToString());
            }
            else {
                PlayAnimation(e.AnimationName.ToString());
            }
        }

        private void OnDestroy() {
            animationEventComponent.OnAnimationEvent -= AnimationEventComponentOnAnimationEvent;
        }
    }
}