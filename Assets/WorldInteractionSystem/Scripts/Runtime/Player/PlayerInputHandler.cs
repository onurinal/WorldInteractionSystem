using UnityEngine;
using UnityEngine.InputSystem;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Player
{
    public class PlayerInputHandler : MonoBehaviour, PlayerInputActions.IPlayerActions, IInputProvider
    {
        private PlayerInputActions playerInputActions;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        private void OnEnable()
        {
            if (playerInputActions == null)
            {
                playerInputActions = new PlayerInputActions();
            }

            playerInputActions.Enable();
            playerInputActions.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            if (playerInputActions != null)
            {
                playerInputActions.Disable();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookInput = context.ReadValue<Vector2>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                EventManager.TriggerOnInteract();
            }
            else if (context.canceled)
            {
                EventManager.TriggerOnInteractCancel();
            }
        }
    }
}