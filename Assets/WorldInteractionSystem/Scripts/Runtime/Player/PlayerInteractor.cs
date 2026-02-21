using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Player
{
    [RequireComponent(typeof(InteractionSensor))]
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private float detectionFrequency = 0.1f;

        private InteractionSensor interactionSensor;
        private IInteractable currentInteractable;
        private float nextDetectionTime;

        private void Awake()
        {
            interactionSensor = GetComponent<InteractionSensor>();
        }

        private void OnEnable()
        {
            EventManager.OnInteractStart += TryInteract;
            EventManager.OnInteractCancel += CancelInteraction;
            EventManager.OnInteractDestroyed += RemoveInteractable;
        }

        private void OnDisable()
        {
            EventManager.OnInteractStart -= TryInteract;
            EventManager.OnInteractCancel -= CancelInteraction;
            EventManager.OnInteractDestroyed -= RemoveInteractable;
            RemoveInteractable(currentInteractable);
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

        private void UpdateClosestInteractable()
        {
            IInteractable closest = interactionSensor.GetClosestInteractable();

            if (currentInteractable == closest)
            {
                return;
            }

            ChangeCurrentInteractable(closest);
        }

        private void ChangeCurrentInteractable(IInteractable newInteractable)
        {
            DeSelectInteractable(currentInteractable);
            currentInteractable = newInteractable;

            if (currentInteractable != null)
            {
                currentInteractable.OnStateChanged += RefreshInteractionUI;
                currentInteractable.ToggleHighlight(true);
                EventManager.TriggerOnInteractDetected(currentInteractable.GetInteractText(gameObject));
            }
            else
            {
                EventManager.TriggerOnInteractCleared();
            }
        }

        private void RemoveInteractable(IInteractable interactable)
        {
            interactionSensor.RemoveInteractable(interactable);

            if (currentInteractable == interactable)
            {
                DeSelectInteractable(interactable);
                currentInteractable = null;
                EventManager.TriggerOnInteractCleared();
            }
        }

        private void DeSelectInteractable(IInteractable interactable)
        {
            if (interactable == null)
            {
                return;
            }

            interactable.InteractCancel(gameObject);
            interactable.ToggleHighlight(false);
        }

        private void RefreshInteractionUI()
        {
            if (currentInteractable != null)
            {
                EventManager.TriggerOnInteractDetected(currentInteractable.GetInteractText(gameObject));
            }
        }

        private void TryInteract() => currentInteractable?.InteractStart(gameObject);
        private void CancelInteraction() => currentInteractable?.InteractCancel(gameObject);
    }
}