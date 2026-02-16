using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private BoxCollider interactionCollider;
        [SerializeField] private float interactionRange = 4f;

        private IInteractable currentInteractable;

        private void Awake()
        {
            if (interactionCollider == null)
            {
                Debug.LogError($"{nameof(PlayerInteraction)} interaction collider is null", this);
            }
        }

        public void Initialize()
        {
            ConfigureInteractionRange();
        }

        private void OnEnable()
        {
            EventManager.OnInteract += Interact;
        }

        private void OnDisable()
        {
            EventManager.OnInteract -= Interact;
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = null;
            }
        }

        private void Interact()
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }

        private void ConfigureInteractionRange()
        {
            interactionCollider.size = Vector3.one * interactionRange;

            var targetY = interactionRange / 2f;
            interactionCollider.center = new Vector3(0, targetY, 0);
        }
    }
}