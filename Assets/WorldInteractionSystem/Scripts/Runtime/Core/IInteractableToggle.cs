namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInteractableToggle : IInteractable
    {
        bool IsOn { get; }
    }
}