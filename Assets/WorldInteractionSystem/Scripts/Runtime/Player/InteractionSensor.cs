using System.Collections.Generic;
using UnityEngine;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Player
{
    public class InteractionSensor : MonoBehaviour
    {
        [SerializeField] private BoxCollider interactionCollider;
        [SerializeField] private float interactionRange = 4f;

        private readonly HashSet<IInteractable> interactablesInRange = new();

        private void Awake()
        {
            if (interactionCollider == null)
            {
                Debug.LogError($"{nameof(PlayerInteractor)} interaction collider is null", this);
            }

            ConfigureInteractionRange();
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                if (interactable.CanInteract)
                {
                    interactablesInRange.Add(interactable);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.GetComponentInParent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            RemoveInteractable(interactable);
        }

        public void RemoveInteractable(IInteractable interactable)
        {
            interactablesInRange.Remove(interactable);
        }

        public IInteractable GetClosestInteractable()
        {
            if (interactablesInRange.Count == 0)
            {
                return null;
            }

            var playerPosition = transform.position;
            var closestDistance = float.MaxValue;
            IInteractable closestInteractable = null;

            foreach (var interactable in interactablesInRange)
            {
                if (interactable == null)
                {
                    continue;
                }

                if (!interactable.CanInteract)
                {
                    continue;
                }

                var distance = (interactable.GetInteractionPosition() - playerPosition).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }

            return closestInteractable;
        }

        private void ConfigureInteractionRange()
        {
            interactionCollider.size = Vector3.one * interactionRange;
            var newPos = interactionRange / 2f;
            interactionCollider.center = new Vector3(interactionCollider.center.x, newPos, newPos);
        }
    }
}