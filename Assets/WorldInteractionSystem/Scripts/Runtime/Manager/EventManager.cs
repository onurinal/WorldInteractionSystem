using System;

namespace WorldInteractionSystem.Runtime.Manager
{
    public static class EventManager
    {
        public static event Action OnInteractStart;
        public static event Action OnInteractCancel;

        public static void TriggerOnInteract()
        {
            OnInteractStart?.Invoke();
        }

        public static void TriggerOnInteractCancel()
        {
            OnInteractCancel?.Invoke();
        }
    }
}