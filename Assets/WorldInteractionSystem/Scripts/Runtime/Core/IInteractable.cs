using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInteractable
    {
        void Interact();
        void ToggleHighlight(bool active);
        string GetInteractText();
        Transform GetTransform();
    }
}