using UnityEngine;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.player
{
    public class PlayerModel
    {
        private PlayerController playerController;
        private float Speed = 5f;
        private int health = 100;
        private int magzine;
        public PlayerModel(PlayerSO playerSO)
        {
            Speed = playerSO.speed;
            health = playerSO.health;
            magzine = playerSO.magSize;
        }
        public void SetController(PlayerController _playerController)
        {
            playerController = _playerController;
        }
        public float speed { get => Speed; }
        public int Health { get => health; }
        public int Magsize { get => magzine; }
    }
}
