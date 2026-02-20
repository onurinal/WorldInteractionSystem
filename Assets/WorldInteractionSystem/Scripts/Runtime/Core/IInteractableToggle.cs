namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInteractableToggle : IInteractable
    {
        void SetToggleState(bool toggle);
        bool IsOn { get; }
    }
}