using UnityEngine;

namespace WorldInteractionSystem.Runtime.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "WorldInteractionSystem/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField, Range(180f, 720f)] private float rotateSpeed = 500f;

        public float MoveSpeed => moveSpeed;
        public float RotateSpeed => rotateSpeed;
    }
}