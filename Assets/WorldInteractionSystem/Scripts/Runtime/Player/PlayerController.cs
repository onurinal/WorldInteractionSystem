using System;
using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Animator myAnimator;

        private Rigidbody myRigidbody;
        private IInputProvider inputProvider;
        private PlayerMovement playerMovement;

        private IInteractable currentInteractable;

        private void Awake()
        {
            CacheReferences();
            ValidateReferences();
        }

        private void CacheReferences()
        {
            inputProvider = GetComponent<IInputProvider>();
            myRigidbody = GetComponent<Rigidbody>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void ValidateReferences()
        {
            if (cameraTransform == null)
            {
                Debug.LogError($"{nameof(PlayerController)}: Camera Transform is not assigned.", this);
            }

            if (playerData == null)
            {
                Debug.LogError($"{nameof(PlayerController)}: PlayerData is not assigned.", this);
            }

            if (inputProvider == null)
            {
                Debug.LogError($"{nameof(PlayerController)}: IInputProvider component is missing.", this);
            }

            if (myAnimator == null)
            {
                Debug.LogError($"{nameof(PlayerController)}: Animator component not found in children.", this);
            }

            if (playerMovement == null)
            {
                Debug.LogError($"{nameof(PlayerController)}: PlayerMovement component is missing.", this);
            }
        }

        public void Initialize()
        {
            playerMovement.Initialize(inputProvider, playerData, cameraTransform, myRigidbody, myAnimator);
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
    }
}