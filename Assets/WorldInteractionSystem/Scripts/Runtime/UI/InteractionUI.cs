using TMPro;
using UnityEngine;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Scripts.Runtime.UI
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private Transform parentOfInteractionUI;
        [SerializeField] private TextMeshProUGUI interactableText;

        private void OnEnable()
        {
            EventManager.OnInteractDetected += ShowInteractionText;
            EventManager.OnInteractCleared += HideInteractionText;
        }

        private void OnDisable()
        {
            EventManager.OnInteractDetected -= ShowInteractionText;
            EventManager.OnInteractCleared -= HideInteractionText;
        }

        private void ShowInteractionText(string text)
        {
            interactableText.text = text;
        }

        private void HideInteractionText()
        {
            interactableText.text = string.Empty;
        }
    }
}