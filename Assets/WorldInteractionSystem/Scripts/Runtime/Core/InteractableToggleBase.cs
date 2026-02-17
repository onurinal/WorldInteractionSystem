using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableToggleBase : InteractableBase, IInteractableToggle
    {
        [SerializeField] private bool isOn;

        public bool IsOn => isOn;

        public override void Interact()
        {
            bool targetState = !isOn;

            if (TryToggle(targetState))
            {
                isOn = targetState;
            }
        }

        public override string GetInteractText()
        {
            return isOn ? OffText : OnText;
        }

        protected abstract string OnText { get; }
        protected abstract string OffText { get; }
        protected abstract bool TryToggle(bool targetState);
    }
}