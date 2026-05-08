using UnityEngine;

namespace Nesur.Core.StateMachine {
    public class StateMachine<T> where T : IState {

        public IState CurrentState => _currentState;
        
        private T _currentState;


        public StateMachine(T defaultState) {
            _currentState = defaultState;
        }

        public void ChangeState(T newState) {
            if (_currentState is ITransitionState transitionState) {
                if (!transitionState.CanTransition(newState)) {
                    Debug.Log($"Transition to the new state: {newState} from state: {_currentState} is not allowed.");
                    return;
                }
            }

            if (_currentState != null) {
                _currentState.Exit();
            }

            _currentState = newState;
            if (_currentState != null) {
                _currentState.Enter();
            }
        }

        public void Update() {
            if (_currentState != null) {
                _currentState.Update();
            }
        }
    }
}