using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.player;
namespace FPSZombie.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSo", menuName = "Player/playerGun")]
    public class PlayerSO : ScriptableObject
    {   [Header ("Player Info")]
        public  PlayerView playerView;
        public  int speed;
        public int health;
        [Header("Shooting")]
        public  int damage;
        public  int maxDistance;
        [Header("Reloading")]
        public  int currentAmmo;
        public  int magSize;
        public float fireRate;
        public float reloadTime;
        public int ammoReserve;
        [HideInInspector]
        public  bool reloading;
    }
}