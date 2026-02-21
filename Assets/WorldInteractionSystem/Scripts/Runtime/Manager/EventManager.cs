using System;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Manager
{
    public static class EventManager
    {
        public static event Action OnInteractStart;
        public static event Action OnInteractCancel;
        public static event Action<IInteractable> OnInteractDestroyed;

        public static event Action<string> OnInteractDetected;
        public static event Action OnInteractCleared;

        public static void TriggerOnInteract()
        {
            OnInteractStart?.Invoke();
        }

        public static void TriggerOnInteractCancel()
        {
            OnInteractCancel?.Invoke();
        }

        public static void TriggerOnInteractDestroyed(IInteractable interactable)
        {
            OnInteractDestroyed?.Invoke(interactable);
        }


        public static void TriggerOnInteractDetected(string text)
        {
            OnInteractDetected?.Invoke(text);
        }

        public static void TriggerOnInteractCleared()
        {
            OnInteractCleared?.Invoke();
        }
    }
}