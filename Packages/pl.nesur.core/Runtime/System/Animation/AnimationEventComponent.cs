using System;
using UnityEngine;

namespace Nesur.Core.Animation {
    /// <summary>
    /// Component to handle animation events from logic and invoke them in the animator on visuals
    /// </summary>
    public class AnimationEventComponent : MonoBehaviour {
        public EventHandler OnAnimationStartedEvent;
        public EventHandler OnAnimationFinishedEvent;
        public EventHandler<PlayAnimationEventArgs> OnAnimationEvent;

        public void PlayAnimation(Enum animationName) {
            OnAnimationEvent?.Invoke(this, new PlayAnimationEventArgs(animationName, false));
        }

        public void PlayAnimationForceChange(Enum animationName) {
            OnAnimationEvent?.Invoke(this, new PlayAnimationEventArgs(animationName, true));
        }

        public void OnAnimationStarted() {
            OnAnimationStartedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void OnAnimationFinished() {
            OnAnimationFinishedEvent?.Invoke(this, EventArgs.Empty);
        }

        public class PlayAnimationEventArgs : EventArgs {
            public Enum AnimationName;
            public bool ForceChange;

            public PlayAnimationEventArgs(Enum animationName, bool forceChange) {
                AnimationName = animationName;
                ForceChange = forceChange;
            }
        }
    }
}