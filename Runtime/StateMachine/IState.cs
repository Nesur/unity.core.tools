namespace Nesur.Core.StateMachine {
    public interface IState {
        void Enter();
        void Exit();
        void Update();
    }

    public interface ITransitionState : IState {
        bool CanTransition(IState toState);
    }
}