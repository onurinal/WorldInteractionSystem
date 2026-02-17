using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class InteractableToggleBase : MonoBehaviour, IInteractableToggle
    {
        [SerializeField] private Transform interactionPoint;

        [Header("Outline Settings")]
        private static readonly int OutlineThicknessId = Shader.PropertyToID("_OutlineThickness");
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float outlineThickness = 1.02f;
        private MaterialPropertyBlock propertyBlock;

        [Header("Toggle Settings")]
        [SerializeField] private string onText = "Turn On";
        [SerializeField] private string offText = "Turn Off";
        protected bool isOn;

        public bool IsOn => isOn;

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
        }

        public void Interact()
        {
            isOn = !isOn;
            OnToggle();
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

        public string GetInteractText()
        {
            return isOn ? onText : offText;
        }

        public Vector3 GetInteractionPosition()
        {
            return interactionPoint.position;
        }

        protected abstract void OnToggle();
    }
}