using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Scripts.Runtime.UI
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private Transform parentOfInteractionUI;
        [SerializeField] private TextMeshProUGUI interactableText;


        [SerializeField] private Image holdingBarImage;

        private Coroutine holdCoroutine;

        private void OnEnable()
        {
            EventManager.OnInteractDetected += ShowInteractionText;
            EventManager.OnInteractCleared += HideInteractionText;

            EventManager.OnInteractProgress += UpdateProgressBar;
            EventManager.OnInteractCancel += HideProgressBar;
        }

        private void OnDisable()
        {
            EventManager.OnInteractDetected -= ShowInteractionText;
            EventManager.OnInteractCleared -= HideInteractionText;

            EventManager.OnInteractProgress -= UpdateProgressBar;
            EventManager.OnInteractCancel -= HideProgressBar;
        }

        private void ShowInteractionText(string text)
        {
            interactableText.text = text;
        }

        private void HideInteractionText()
        {
            interactableText.text = string.Empty;
        }

        private void UpdateProgressBar(float progress)
        {
            holdingBarImage.gameObject.SetActive(true);
            holdingBarImage.fillAmount = 1f - progress;
        }

        private void HideProgressBar()
        {
            holdingBarImage.gameObject.SetActive(false);
            holdingBarImage.fillAmount = 1f;
        }
    }
}