using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform interactionPoint;

        [Header("Outline Settings")]
        private static readonly int OutlineThicknessId = Shader.PropertyToID("_OutlineThickness");
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float outlineThickness = 1.02f;

        private MaterialPropertyBlock propertyBlock;

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
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

        public abstract void Interact();
        public abstract string GetInteractText();
    }
}