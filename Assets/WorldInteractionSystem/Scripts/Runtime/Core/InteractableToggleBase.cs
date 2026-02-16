using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableToggleBase : MonoBehaviour, IInteractableToggle
    {
        protected bool isOn;
        [SerializeField] private string onText = "Turn On";
        [SerializeField] private string offText = "Turn Off";

        public bool IsOn => isOn;

        public void Interact()
        {
            isOn = !isOn;
            OnToggle();
        }

        public string GetInteractText()
        {
            return isOn ? onText : offText;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        protected abstract void OnToggle();
    }
}