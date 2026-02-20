using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using DG.Tweening;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Door : InteractableToggleBase
    {
        [Header("Settings")]
        [SerializeField] private Transform hingeTransform;
        [SerializeField] private float rotateAngle = 90f;
        [SerializeField] private float rotateTime = 1f;

        [Header("Lock Settings")]
        [SerializeField] private bool isLocked = false;
        [SerializeField] private ItemData requiredKey;

        protected override void Awake()
        {
            base.Awake();

            InitializeInteractableTexts();
        }

        private void InitializeInteractableTexts()
        {
            if (isLocked)
            {
                OnText = "Locked (Requires " + requiredKey.ItemName + ")";
                OffText = string.Empty;
            }
            else
            {
                OnText = "Open Door";
                OffText = "Close Door";
            }
        }

        protected override bool TryToggle(GameObject interactor, bool targetState)
        {
            if (!CanInteract)
            {
                return false;
            }

            if (isLocked && targetState)
            {
                if (interactor.TryGetComponent<IInventory>(out var inventory))
                {
                    if (inventory.HasItem(requiredKey))
                    {
                        UnlockAndOpen();
                        inventory.RemoveItem(requiredKey, 1);
                        return true;
                    }

                    return false;
                }
            }

            return true;
        }

        private void UnlockAndOpen()
        {
            isLocked = false;
            CanInteract = false;
            ToggleHighlight(false);
        }

        protected override void PlayAnimation(bool state)
        {
            if (hingeTransform == null)
            {
                return;
            }

            hingeTransform.DOKill();
            var targetRotationY = state ? -rotateAngle : 0;
            var currentEulerAngles = hingeTransform.localEulerAngles;
            hingeTransform.DOLocalRotate(new Vector3(currentEulerAngles.x, targetRotationY, currentEulerAngles.z),
                rotateTime).SetEase(Ease.InOutQuad);
        }
    }
}