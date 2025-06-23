namespace ViscoveryDemo.BLL.Services
{
    public interface INavigable
    {
        NavigationStateMachine StateMachine { get; }
        void OnEnter();
        void OnExit();
    }
}
