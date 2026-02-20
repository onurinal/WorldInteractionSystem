using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Switch : InteractableToggleBase
    {
        [Header("Settings")]
        [SerializeField] private Transform switchHandleTransform;
        [SerializeField] private float rotateAngle = 30f;
        [SerializeField] private float rotateTime = 0.5f;

        [SerializeField] private UnityEvent<bool> onSwitchToggled;

        protected override bool TryToggle(GameObject interactor, bool targetState)
        {
            if (onSwitchToggled == null)
            {
                return false;
            }

            onSwitchToggled.Invoke(targetState);
            return true;
        }

        protected override void PlayAnimation(bool state)
        {
            if (switchHandleTransform == null)
            {
                return;
            }

            switchHandleTransform.DOKill();
            var targetRotationX = state ? -rotateAngle : rotateAngle;
            var currentEulerAngles = switchHandleTransform.localEulerAngles;
            switchHandleTransform.DOLocalRotate(
                new Vector3(targetRotationX, currentEulerAngles.y, currentEulerAngles.z),
                rotateTime).SetEase(Ease.InOutQuad);
        }
    }
}