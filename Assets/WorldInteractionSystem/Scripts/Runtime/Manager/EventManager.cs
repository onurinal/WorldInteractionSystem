using System;
using System.Collections.Generic;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Manager
{
    public static class EventManager
    {
        public static event Action OnInteractStart;
        public static event Action<float> OnInteractProgress;
        public static event Action OnInteractCancel;
        public static event Action<IInteractable> OnInteractDestroyed;

        public static event Action<string> OnInteractDetected;
        public static event Action OnInteractCleared;

        public static event Action<IReadOnlyList<InventorySlot>> OnInventoryChanged;
        public static event Action<int> OnInventoryInitialized;


        public static void TriggerOnInteractStart()
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

        public static void TriggerOnInteractProgress(float duration)
        {
            OnInteractProgress?.Invoke(duration);
        }

        public static void TriggerOnInventoryChanged(IReadOnlyList<InventorySlot> slots)
        {
            OnInventoryChanged?.Invoke(slots);
        }

        public static void TriggerOnInventoryInitialized(int capacity)
        {
            OnInventoryInitialized?.Invoke(capacity);
        }
    }
}