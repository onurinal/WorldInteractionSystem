using UnityEngine;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Interactable
{
    public class Key : InteractableInstantBase
    {
        [SerializeField] private KeyData keyData;
        [SerializeField] private int addAmount;

        protected override void Awake()
        {
            base.Awake();
            InteractText = $"Pick Up {keyData.ItemName}";
        }

        public override void InteractStart(GameObject interactor)
        {
            if (interactor.TryGetComponent<IInventory>(out var inventory))
            {
                int added = inventory.AddItem(keyData, addAmount);

                if (added < addAmount)
                {
                    Debug.Log($"You didn't pick up");
                    return;
                }
            }

            CanInteract = false;
            Destroy(gameObject);
        }
    }
}