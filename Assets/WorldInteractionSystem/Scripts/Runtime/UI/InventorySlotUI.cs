using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Scripts.Runtime.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI slotStackSize;

        public void SetSlot(InventorySlot item)
        {
            if (item == null)
            {
                return;
            }

            slotImage.sprite = item.ItemData.ItemSprite;
            slotStackSize.text = item.Amount.ToString();
        }

        public void ClearSlot()
        {
            slotImage.sprite = null;
            slotStackSize.text = "";
        }
    }
}