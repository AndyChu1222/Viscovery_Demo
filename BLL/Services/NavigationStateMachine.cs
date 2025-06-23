namespace ViscoveryDemo.BLL.Services
{
    public enum PageState
    {
        Idle,
        Entering,
        Entered,
        Exiting,
        Exited
    }

    public class NavigationStateMachine
    {
        public PageState CurrentState { get; private set; } = PageState.Idle;
        public event System.Action<PageState> StateChanged;

        public void Enter()
        {
            SetState(PageState.Entering);
            SetState(PageState.Entered);
        }

        public void Exit()
        {
            SetState(PageState.Exiting);
            SetState(PageState.Exited);
        }

        private void SetState(PageState state)
        {
            CurrentState = state;
            StateChanged?.Invoke(state);
        }
    }
}
