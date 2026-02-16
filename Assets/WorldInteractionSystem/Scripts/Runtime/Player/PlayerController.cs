using UnityEngine;

namespace WorldInteractionSystem.Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Animator myAnimator;

        [SerializeField] private Rigidbody myRigidbody;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerInteraction playerInteraction;

        private IInputProvider inputProvider;

        private void Awake()
        {
            CacheReferences();
            ValidateReferences();
        }

        private void CacheReferences()
        {
            inputProvider = GetComponent<IInputProvider>();
        }

        private void ValidateReferences()
        {
            if (playerCamera == null)
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

            if (playerInteraction == null)
            {
                Debug.LogError($"{nameof(PlayerController)}: PlayerInteraction component is missing.", this);
            }
        }

        public void Initialize()
        {
            playerMovement.Initialize(inputProvider, playerData, playerCamera, myRigidbody, myAnimator);
            playerInteraction.Initialize();
        }
    }
}