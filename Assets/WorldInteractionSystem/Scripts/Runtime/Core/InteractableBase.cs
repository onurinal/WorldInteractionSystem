using System;
using UnityEngine;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform interactionPoint;

        [Header("Outline Settings")]
        private static readonly int OutlineThicknessId = Shader.PropertyToID("_OutlineThickness");
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private float outlineThickness = 1.02f;

        private MaterialPropertyBlock propertyBlock;

        public event Action OnStateChanged;

        protected virtual void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
        }

        protected void OnDisable()
        {
            EventManager.TriggerOnInteractDestroyed(this);
        }

        public void ToggleHighlight(bool active)
        {
            if (meshRenderer == null)
            {
                return;
            }

            meshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat(OutlineThicknessId, active ? outlineThickness : 0.0f);
            meshRenderer.SetPropertyBlock(propertyBlock);
        }

        public Vector3 GetInteractionPosition()
        {
            return interactionPoint.position;
        }

        public bool CanInteract { get; protected set; } = true;
        public abstract void InteractStart(GameObject interactor);

        public virtual void InteractCancel(GameObject interactor)
        {
        }

        public abstract string GetInteractText(GameObject interactor);

        protected void TriggerOnStateChanged()
        {
            OnStateChanged?.Invoke();
        }
    }
}