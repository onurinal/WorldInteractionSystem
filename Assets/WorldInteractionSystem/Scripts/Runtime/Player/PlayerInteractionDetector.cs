using System.Collections.Generic;
using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Player
{
    public class PlayerInteractionDetector : MonoBehaviour
    {
        [SerializeField] private BoxCollider interactionCollider;
        [SerializeField] private float interactionRange = 4f;
        [SerializeField] private float detectionFrequency = 0.1f;

        private readonly HashSet<IInteractable> interactablesInRange = new();
        private IInteractable currentInteractable;
        private float nextDetectionTime;

        private void Awake()
        {
            if (interactionCollider == null)
            {
                Debug.LogError($"{nameof(PlayerInteractionDetector)} interaction collider is null", this);
            }

            ConfigureInteractionRange();
        }

        private void OnEnable()
        {
            EventManager.OnInteract += Interact;
        }

        private void OnDisable()
        {
            EventManager.OnInteract -= Interact;
            ClearCurrentInteractable();
        }

        private void Update()
        {
            if (Time.time < nextDetectionTime)
            {
                return;
            }

            nextDetectionTime = Time.time + detectionFrequency;
            UpdateClosestInteractable();
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                interactablesInRange.Add(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.GetComponentInParent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            interactablesInRange.Remove(interactable);

            if (currentInteractable == interactable)
            {
                ClearCurrentInteractable();
            }
        }

        private void Interact()
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }

        private void UpdateClosestInteractable()
        {
            IInteractable interactable = GetClosestInteractable();

            if (currentInteractable == interactable)
            {
                return;
            }

            ChangeCurrentInteractable(interactable);
        }

        private IInteractable GetClosestInteractable()
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

                var distance = (interactable.GetInteractionPosition() - playerPosition).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }

            return closestInteractable;
        }

        private void ChangeCurrentInteractable(IInteractable interactable)
        {
            currentInteractable?.ToggleHighlight(false);
            currentInteractable = interactable;
            currentInteractable?.ToggleHighlight(true);
        }

        private void ClearCurrentInteractable()
        {
            if (currentInteractable == null)
            {
                return;
            }

            currentInteractable.ToggleHighlight(false);
            currentInteractable = null;
        }

        private void ConfigureInteractionRange()
        {
            interactionCollider.size = Vector3.one * interactionRange;

            var targetY = interactionRange / 2f;
            interactionCollider.center = new Vector3(0, targetY, 0);
        }
    }
}