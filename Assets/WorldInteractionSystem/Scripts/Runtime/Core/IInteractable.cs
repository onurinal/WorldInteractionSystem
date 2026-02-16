using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInteractable
    {
        void Interact();
        string GetInteractText();
        Transform GetTransform();
    }
}