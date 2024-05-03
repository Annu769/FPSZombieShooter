
using UnityEngine;
using FPSZombie.Zombie;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.PoolObject
{
    public class LadyZombiePool : GenericPooledService<ZombieController>
    {
        EnemyScriptableObject enemyData;
        Canvas enemyCanvas;
        ZombieType zombieType;
        public ZombieController GetZombie(EnemyScriptableObject _enemyData, Canvas _enemyCanvas, ZombieType _zombieType)
        {
            enemyData = _enemyData;
            enemyCanvas = _enemyCanvas;
            zombieType = _zombieType;
            return GetItem();
        }
        protected override ZombieController CreateItem()
        {
            ZombieController zombieController = new ZombieController(enemyData, enemyCanvas, zombieType);
            return zombieController;

        }
    }
}
