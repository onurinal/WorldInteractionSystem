using System;

namespace WorldInteractionSystem.Runtime.Manager
{
    public static class EventManager
    {
        public static event Action OnInteract;

        public static void TriggerOnInteract()
        {
            OnInteract?.Invoke();
        }
    }
}