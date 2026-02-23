using System.Collections.Generic;
using UnityEngine;
using WorldInteractionSystem.Runtime.Core;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Scripts.Runtime.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI slotPrefab;
        [SerializeField] private Transform slotContainer;


        private readonly List<InventorySlotUI> slotsUI = new();

        private void OnEnable()
        {
            EventManager.OnInventoryInitialized += InitializeInventoryUI;
            EventManager.OnInventoryChanged += RefreshUI;
        }

        private void OnDisable()
        {
            EventManager.OnInventoryInitialized -= InitializeInventoryUI;
            EventManager.OnInventoryChanged -= RefreshUI;
        }

        private void InitializeInventoryUI(int capacity)
        {
            slotsUI.Clear();

            for (int i = 0; i < capacity; i++)
            {
                var newSlotUI = Instantiate(slotPrefab, slotContainer);
                slotsUI.Add(newSlotUI);
            }
        }

        private void RefreshUI(IReadOnlyList<InventorySlot> slots)
        {
            for (int i = 0; i < slotsUI.Count; i++)
            {
                if (i < slots.Count && !slots[i].IsEmpty())
                {
                    slotsUI[i].SetSlot(slots[i]);
                }
                else
                {
                    slotsUI[i].ClearSlot();
                }
            }
        }
    }
}