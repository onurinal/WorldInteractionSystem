using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using DG.Tweening;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Door : InteractableToggleBase
    {
        [Header("Door Strings")]
        [SerializeField] private string onText = "Open Door";
        [SerializeField] private string offText = "Close Door";
        [SerializeField] private string lockedText = "Locked (Require Key)";

        [Header("Settings")]
        [SerializeField] private bool isLocked = false;
        [SerializeField] private Transform hingeTransform;
        [SerializeField] private float rotateAngle = 90f;
        [SerializeField] private float rotateTime = 2f;

        public override string GetInteractText()
        {
            if (isLocked && !IsOn)
            {
                return lockedText;
            }

            return base.GetInteractText();
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

        private bool HasKey()
        {
            // add key check later
            return false;
        }

        protected override string OnText => onText;
        protected override string OffText => offText;
    }
}