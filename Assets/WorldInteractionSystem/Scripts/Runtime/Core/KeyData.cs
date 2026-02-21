using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    [CreateAssetMenu(fileName = "KeyData", menuName = "WorldInteractionSystem/Item/KeyData")]
    public class KeyData : ItemData
    {
        private void Awake()
        {
            ItemType = ItemType.Key;
        }
    }
}