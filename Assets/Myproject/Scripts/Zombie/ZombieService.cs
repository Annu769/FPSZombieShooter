using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.SingleTon;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.Zombie
{
    public enum ZombieType
    {
        police, monster, lady
    };
    public class ZombieService :GenericMonoSingleTon<ZombieService>
    {
        [SerializeField] private EnemyScriptableObjectList zombieList;
        [SerializeField] private Canvas enemyCanvas;
        private ZombieController zombieController;
        

        private void Start()
        {
            for(int i = 0; i < 10; i++)
            {
                CreateZombie();
            }
            
        }

        public ZombieController CreateZombie()
        {
            ZombieType zombieType = GetRandomZombie();
            EnemyScriptableObject zombieData = zombieList.enemy[(int)zombieType];
            zombieController = new ZombieController(zombieData, enemyCanvas, zombieType);
                return zombieController;
        }
        public ZombieType GetRandomZombie()
        {
            return (ZombieType)Random.Range(0, zombieList.enemy.Length);
        }
    }
}

