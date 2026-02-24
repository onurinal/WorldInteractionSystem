using UnityEngine;
using UnityEngine.Events;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Chest : InteractableHoldBase
    {
        private static readonly int OpenChest = Animator.StringToHash("OpenChest");

        [Header("References")]
        [SerializeField] private Animator chestAnimator;

        [SerializeField] private UnityEvent onChestOpened;

        protected override void OnHoldCompleted(GameObject interactor)
        {
            CanInteract = false;
            TriggerAnimation();
            onChestOpened?.Invoke();
        }

        public override string GetInteractText(GameObject interactor)
        {
            return $"Hold Interact button {HoldTime} seconds to open";
        }

        private void TriggerAnimation()
        {
            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger(OpenChest);
            }
        }
    }
}