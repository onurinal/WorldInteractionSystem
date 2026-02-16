using UnityEngine;

namespace WorldInteractionSystem.Runtime.Player
{
    public interface IInputProvider
    {
        Vector2 MoveInput { get; }

        Vector2 LookInput { get; }
    }
}