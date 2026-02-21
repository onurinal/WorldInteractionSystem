using UnityEngine;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Player
{
    public class PlayerController : MonoBehaviour, IInventory
    {
        [SerializeField] private PlayerInputHandler inputProvider;
        [SerializeField] private Transform playerCamera;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerInteractor playerInteractor;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Animator myAnimator;
        [SerializeField] private Rigidbody myRigidbody;

        private PlayerInventory playerInventory;

        private void Awake()
        {
            ValidateReferences();
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

            if (playerInteractor == null)
            {
                Debug.LogError($"{nameof(PlayerController)}: PlayerInteraction component is missing.", this);
            }
        }

        public void Initialize()
        {
            playerInventory = new PlayerInventory();
            playerMovement.Initialize(inputProvider, playerData, playerCamera, myRigidbody, myAnimator);
        }

        public void AddItem(ItemData item, int amount) => playerInventory.AddItem(item, amount);
        public void RemoveItem(ItemData item, int amount) => playerInventory.RemoveItem(item, amount);
        public bool HasItem(ItemData item) => playerInventory.HasItem(item);
        public int GetAmount(ItemData item) => playerInventory.GetAmount(item);
    }
}