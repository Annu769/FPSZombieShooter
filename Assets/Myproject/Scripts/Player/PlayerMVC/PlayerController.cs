using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.Player
{
    public class PlayerController 
    {
        private PlayerView playerView;
        private PlayerModel playerModel;

        public PlayerController(PlayerSO player)
        {
            playerView = GameObject.Instantiate<PlayerView>(player.playerView);
            playerModel = new PlayerModel(player);
            playerModel.SetController(this);
            playerView.SetController(this);
        }
    }

}

