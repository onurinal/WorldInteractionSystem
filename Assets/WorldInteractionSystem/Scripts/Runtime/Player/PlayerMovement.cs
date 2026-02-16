using UnityEngine;

namespace WorldInteractionSystem.Runtime.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int Running = Animator.StringToHash("Running");
        public const float MovementThreshold = 0.001f;

        private IInputProvider inputProvider;
        private PlayerData playerData;
        private Rigidbody myRigidbody;
        private Transform cameraTransform;
        private Animator myAnimator;

        private bool isMoving;

        public void Initialize(IInputProvider inputProvider, PlayerData playerData, Transform cameraTransform,
            Rigidbody myRigidbody, Animator myAnimator)
        {
            this.inputProvider = inputProvider;
            this.playerData = playerData;
            this.cameraTransform = cameraTransform;
            this.myRigidbody = myRigidbody;
            this.myAnimator = myAnimator;
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = CalculateMovementDirection(inputProvider.MoveInput);
            bool shouldMove = moveDirection.sqrMagnitude > MovementThreshold;

            if (shouldMove)
            {
                HandleMovement(moveDirection);
                HandleRotation(moveDirection);
            }

            UpdateMovementState(shouldMove);
        }

        private Vector3 CalculateMovementDirection(Vector2 input)
        {
            if (input.sqrMagnitude < MovementThreshold)
            {
                return Vector3.zero;
            }

            var cameraForward = cameraTransform.forward;
            var cameraRight = cameraTransform.right;
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            var moveDirection = input.x * cameraRight + input.y * cameraForward;

            return moveDirection;
        }


        private void HandleMovement(Vector3 moveDirection)
        {
            var moveOffset = moveDirection * (Time.fixedDeltaTime * playerData.MoveSpeed);

            myRigidbody.MovePosition(myRigidbody.position + moveOffset);
        }

        private void HandleRotation(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            myRigidbody.MoveRotation(Quaternion.RotateTowards(myRigidbody.rotation, targetRotation,
                playerData.RotateSpeed * Time.fixedDeltaTime));
        }

        private void UpdateMovementState(bool shouldMove)
        {
            if (shouldMove != isMoving)
            {
                isMoving = shouldMove;
                myAnimator.SetBool(Running, isMoving);
            }
        }
    }
}