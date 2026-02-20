using UnityEngine;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Chest : InteractableHoldBase
    {
        private static readonly int OpenChest = Animator.StringToHash("OpenChest");

        [SerializeField] private Animator chestAnimator;


        protected override void OnHoldCompleted(GameObject interactor)
        {
            TriggerAnimation();
            CanInteract = false;
        }

        private void TriggerAnimation()
        {
            chestAnimator.SetTrigger(OpenChest);
        }
    }
}