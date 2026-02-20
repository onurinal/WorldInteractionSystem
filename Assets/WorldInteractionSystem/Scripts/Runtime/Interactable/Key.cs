using UnityEngine;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Key : InteractableInstantBase
    {
        [SerializeField] private ItemData keyData;

        protected override void Awake()
        {
            base.Awake();
            InteractText = $"Pick Up {keyData.ItemName}";
        }

        public override void InteractStart(GameObject interactor)
        {
            if (interactor.TryGetComponent<IInventory>(out var inventory))
            {
                inventory.AddItem(keyData, 1);
            }

            CanInteract = false;
            Destroy(gameObject);
        }
    }
}