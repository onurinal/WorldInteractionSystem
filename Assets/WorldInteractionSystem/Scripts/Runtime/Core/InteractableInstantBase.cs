using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableInstantBase : InteractableBase, IInteractableInstant
    {
        [SerializeField] private string interactText;

        public override string GetInteractText()
        {
            return InteractText;
        }

        protected string InteractText
        {
            get => interactText;
            set => interactText = value;
        }
    }
}