using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using DG.Tweening;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Door : InteractableToggleBase
    {
        [SerializeField] private Transform hingeTransform;
        [SerializeField] private float rotateAngle = 90f;
        [SerializeField] private float rotateTime = 2f;

        protected override void OnToggle()
        {
            var targetRotationY = isOn ? -rotateAngle : 0;
            hingeTransform.DORotate(new Vector3(transform.rotation.x, targetRotationY, transform.rotation.z),
                rotateTime);
        }
    }
}