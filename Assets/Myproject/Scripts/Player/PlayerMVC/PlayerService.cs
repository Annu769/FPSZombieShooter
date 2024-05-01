using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.Generic;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.player
{
    public class PlayerService : GenericSingleTon<PlayerService>
    {
        [SerializeField] private PlayerSO player;
        private PlayerController playerController;
        private void Start()
        {
          
            CreatePlayer();
            
        }
        private PlayerController CreatePlayer()
        {
            playerController = new PlayerController(player, this.transform);
            return playerController;
        }
        public Transform GetPlayerTransform()
        {
           
            return playerController.GetTransform();
        }
        public Camera GetCamera()
        {
            return playerController.GetCamera();
        }
    }
}