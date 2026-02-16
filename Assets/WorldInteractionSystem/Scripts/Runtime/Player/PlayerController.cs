using UnityEngine;

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
    }
}