using UnityEngine;

namespace WorldInteractionSystem.Runtime.Player
{
    public interface IInputProvider
    {
        public Vector2 MoveInput { get; }
        public Vector2 LookInput { get; }
        public bool ConsumeInteractPressed();
    }
}