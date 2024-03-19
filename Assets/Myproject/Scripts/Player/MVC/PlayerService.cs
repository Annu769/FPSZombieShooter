using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.SingleTon;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.Player
{
    public class PlayerService :GenericMonoSingleTon<PlayerService>
    {
        [SerializeField] private PlayerSO playerSO;
        private PlayerController playerController;

        private void Start()
        {
            CreatePlayer();
        }

        public PlayerController CreatePlayer()
        {
            playerController = new PlayerController(playerSO);
            return playerController;
        }

        public Transform GetPlayerTransform()
        {
            Debug.Log(playerController.GetTransform());
            return playerController.GetTransform();
        }
    }

}
