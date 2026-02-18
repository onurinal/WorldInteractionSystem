using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using DG.Tweening;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Door : InteractableToggleBase
    {
        [Header("Settings")]
        [SerializeField] private bool isLocked = false;
        [SerializeField] private Transform hingeTransform;
        [SerializeField] private float rotateAngle = 90f;
        [SerializeField] private float rotateTime = 2f;

        protected override void Awake()
        {
            base.Awake();

            InitializeDoorTexts();
        }

        private void InitializeDoorTexts()
        {
            if (isLocked)
            {
                OnText = "Locked ( Require Key) !";
                OffText = string.Empty;
            }
            else
            {
                OnText = "Open Door";
                OffText = "Close Door";
            }
        }

        protected override bool TryToggle(bool targetState)
        {
            if (isLocked && targetState == true)
            {
                if (HasKey())
                {
                    UnlockAndOpen();
                    return true;
                }

                return false;
            }

            PlayAnimation(targetState);
            return true;
        }

        private void UnlockAndOpen()
        {
            isLocked = false;
            PlayAnimation(true);

            this.enabled = false;
            Debug.Log("Unlocked door, no more interaction");
        }

        private void PlayAnimation(bool state)
        {
            hingeTransform.DOKill();
            var targetRotationY = state ? -rotateAngle : 0;
            hingeTransform.DORotate(new Vector3(transform.rotation.x, targetRotationY, transform.rotation.z),
                rotateTime);
        }

        private static bool HasKey()
        {
            // add key check later
            return false;
        }
    }
}