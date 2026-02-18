using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableToggleBase : InteractableBase, IInteractableToggle
    {
        [SerializeField] private string onText = "Turn On";
        [SerializeField] private string offText = "Turn Off";
        [SerializeField] private bool isOn;

        public sealed override void Interact()
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

        protected abstract bool TryToggle(bool targetState);

        protected string OnText
        {
            get => onText;
            set => onText = value;
        }

        protected string OffText
        {
            get => offText;
            set => offText = value;
        }
    }
}