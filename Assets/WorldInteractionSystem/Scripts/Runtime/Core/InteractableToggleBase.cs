using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableToggleBase : InteractableBase, IInteractableToggle
    {
        [SerializeField] private string onText = "Turn On";
        [SerializeField] private string offText = "Turn Off";
        [SerializeField] private bool isOn;

        public sealed override void InteractStart(GameObject interactor)
        {
            bool targetState = !isOn;

            if (TryToggle(interactor, targetState))
            {
                SetToggleState(targetState);
            }
        }

        public override string GetInteractText()
        {
            return isOn ? OffText : OnText;
        }

        public void SetToggleState(bool targetState)
        {
            if (isOn == targetState)
            {
                return;
            }

            isOn = targetState;
            PlayAnimation(targetState);
        }

        protected abstract bool TryToggle(GameObject interactor, bool targetState);
        protected abstract void PlayAnimation(bool state);

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

        public bool IsOn => isOn;
    }
}