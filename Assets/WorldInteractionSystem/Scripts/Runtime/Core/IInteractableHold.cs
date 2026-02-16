namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInteractableHold : IInteractable
    {
        float HoldTime { get; }
    }
}