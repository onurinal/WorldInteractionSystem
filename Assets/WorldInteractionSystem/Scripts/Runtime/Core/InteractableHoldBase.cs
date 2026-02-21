using System.Collections;
using UnityEngine;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableHoldBase : InteractableBase, IInteractableHold
    {
        [SerializeField] private float holdTime = 2f;

        private Coroutine holdCoroutine;

        public override void InteractStart(GameObject interactor)
        {
            if (holdCoroutine != null)
            {
                return;
            }

            holdCoroutine = StartCoroutine(HoldCoroutine(interactor));
        }

        public override void InteractCancel(GameObject interactor)
        {
            if (holdCoroutine == null)
            {
                return;
            }

            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }

        private IEnumerator HoldCoroutine(GameObject interactor)
        {
            float elapsedTime = 0f;
            while (elapsedTime < holdTime)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / holdTime;
                EventManager.TriggerOnInteractProgress(progress);
                yield return null;
            }

            holdCoroutine = null;
            OnHoldCompleted(interactor);
        }

        public float HoldTime => holdTime;
        protected abstract void OnHoldCompleted(GameObject interactor);
    }
}