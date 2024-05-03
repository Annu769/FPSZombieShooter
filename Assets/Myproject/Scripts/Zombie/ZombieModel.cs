using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.Zombie
{
    public class ZombieModel
    {
        private ZombieController zombieController;
        private int health;
        private int Damage;
        private int speed;
        private ZombieType zombieType1;

        public ZombieModel(EnemyScriptableObject enemydata, ZombieType zombieType)
        {
            health = enemydata.Health;
            Damage = enemydata.Damage;
            speed = enemydata.Speed;
            zombieType1 = zombieType;
        }
        public void SetController(ZombieController _zombieController)
        {
            zombieController = _zombieController;
        }
        public ZombieType ZombieType1
        {
            get => zombieType1;
        }
        public int Health { get => health; }
        public int _damage { get => Damage; }
        public int _speed { get => speed; }
    }
}

