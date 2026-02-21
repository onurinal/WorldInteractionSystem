using System;
using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInteractable
    {
        void InteractStart(GameObject interactor);
        void InteractCancel(GameObject interactor);
        void ToggleHighlight(bool active);
        string GetInteractText(GameObject interactor);
        Vector3 GetInteractionPosition();
        bool CanInteract { get; }
        event Action OnStateChanged;
    }
}