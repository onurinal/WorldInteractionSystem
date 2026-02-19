using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInteractable
    {
        void Interact(GameObject interactor);
        void ToggleHighlight(bool active);
        string GetInteractText();
        Vector3 GetInteractionPosition();
        bool CanInteract { get; }
    }
}