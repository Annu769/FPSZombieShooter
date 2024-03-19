using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.Zombie;
using FPSZombie.SingleTon;
using FPSZombie.PoolObject;
using FPSZombie.Player;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.Zombie
{
    public enum ZombieType
    {
        police, monster, lady
    };
    public class ZombieService : GenericMonoSingleTon<ZombieService>
    {
        private List<ZombieController> zombies;
        private int maxZombieCount = 50;
        private List<Transform> SpawnPoints;
        private List<Transform> pointsAlreadySapwn;
        private PoliceZombiePool policePoolSevice;
        private MonsterZombiePool monsterPoolService;
        private LadyZombiePool ladyPoolService;
        private Transform playertransform;
        [SerializeField] private Transform parentwaypoints;
        [SerializeField] private EnemyScriptableObjectList enemyList;
        [SerializeField] private Transform SpawnPoinsParents;
        [SerializeField] private Canvas enemyCanvas;
        [SerializeField] private int zombieCount = 0;
        [SerializeField] private Terrain terrain;
        private void Start()
        {
            zombies = new List<ZombieController>();
            SpawnPoints = new List<Transform>();
            pointsAlreadySapwn = new List<Transform>();
            ladyPoolService = new LadyZombiePool();
            policePoolSevice = new PoliceZombiePool();
            monsterPoolService = new MonsterZombiePool();
            playertransform = PlayerService.Instance.GetPlayerTransform();
            zombieCount = Mathf.Min(zombieCount, maxZombieCount);
            foreach (Transform item in SpawnPoinsParents)
            {
                SpawnPoints.Add(item);
            }

            StartCoroutine(SpawnZombie(maxZombieCount));
        }

        public IEnumerator SpawnZombie()
        {

            yield return new WaitForSeconds(2f);

            Vector3 newPosition = GetRandomSpawnPoints();
            ZombieType zombieType = GetRandomZombie();

            if (newPosition == Vector3.zero)
                yield break;

            ZombieController zombieController = CreateZombie(zombieType, newPosition);
            zombies.Add(zombieController);

        }
        public IEnumerator SpawnZombie(int count)
        {
            terrain = Terrain.activeTerrain;
            if (terrain == null)
            {
                Debug.LogError("Terrain not found!");
                yield break;
            }

            for (int i = 0; i < count; i++)
            {
                float xPos = Random.Range(terrain.transform.position.x, terrain.terrainData.size.x);
                float zPos = Random.Range(terrain.transform.position.z, terrain.terrainData.size.z);
                Vector3 spawnPosition = new Vector3(xPos, 0f, zPos);

                // Sample the height of the terrain at the random position
                spawnPosition.y = terrain.SampleHeight(spawnPosition);

                ZombieType zombieType = GetRandomZombie();

                ZombieController zombieController = CreateZombie(zombieType, spawnPosition);
                zombies.Add(zombieController);
                yield return new WaitForSeconds(0.1f);
            }
        }
        public Transform GetWayPoints()
        {
            return parentwaypoints;
        }
        public Vector3 GetRandomSpawnPoints()
        {
            if (SpawnPoints.Count == 0)
                return Vector3.zero;
            int SpawnPointIndex = Random.Range(0, SpawnPoints.Count);
            Transform newSpawnPoint = SpawnPoints[SpawnPointIndex];
            pointsAlreadySapwn.Add(newSpawnPoint);
            SpawnPoints.RemoveAt(SpawnPointIndex);
            return newSpawnPoint.position;
        }
        public ZombieType GetRandomZombie()
        {
            return (ZombieType)Random.Range(0, enemyList.enemy.Length);
        }
        public ZombieController CreateZombie(ZombieType zombieType, Vector3 newPosition)
        {
            EnemyScriptableObject zombieData = enemyList.enemy[(int)zombieType];
            ZombieController zombieController = null;

            switch (zombieType)
            {
                case ZombieType.police:

                    zombieController = policePoolSevice.GetZombie(zombieData, enemyCanvas, zombieType);
                    break;
                case ZombieType.lady:

                    zombieController = ladyPoolService.GetZombie(zombieData, enemyCanvas, zombieType);
                    break;
                case ZombieType.monster:

                    zombieController = monsterPoolService.GetZombie(zombieData, enemyCanvas, zombieType);
                    break;
                default: break;
            }

            zombieController.EnableZombieController(playertransform, newPosition);
            return zombieController;
        }
        public void DestroyZombie(ZombieController _zombieController, ZombieType zombieType)
        {
            Vector3 pos = _zombieController.GetPosition();
            _zombieController.DisableZombie();
            switch (zombieType)
            {
                case ZombieType.lady:
                    ladyPoolService.ReturnItem(_zombieController);

                    break;
                case ZombieType.monster:
                    monsterPoolService.ReturnItem(_zombieController);

                    break;
                case ZombieType.police:
                    policePoolSevice.ReturnItem(_zombieController);

                    break;
            }
            zombies.Remove(_zombieController);

            Debug.Log(playertransform);
            if (playertransform == null)
            {
                return;
            }
            else
            {
                StartCoroutine(SpawnZombie());
            }

        }
    }
}
