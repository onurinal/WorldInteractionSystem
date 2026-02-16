using UnityEngine;
using WorldInteractionSystem.Runtime.Player;

namespace WorldInteractionSystem.Runtime.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        private void Start()
        {
            playerController.Initialize();
        }
    }
}