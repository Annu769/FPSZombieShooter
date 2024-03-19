using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.Zombie
{
    public class ZombieController 
    {
        public ZombieModel zombieModel { get; }
        public ZombieView zombieView { get; }
        private NavMeshAgent navMesh;
        private Transform playerTransform;
        private int maxHealth;
        private int health;
        private Animator animator;
        private int damage;
        private GameObject[] item;
        private bool hasDroppedItem = false;
        public ZombieController(EnemyScriptableObject enemyData, Canvas _enemyCanvas, ZombieType _zombieType)
        {
            zombieView = GameObject.Instantiate<ZombieView>(enemyData.zombieView);
            zombieModel = new ZombieModel(enemyData, _zombieType);
            navMesh = zombieView.GetNavMeshAgent();
            maxHealth = zombieModel.Health;
            health = maxHealth;
            animator = zombieView.GetAnimator();
            damage = zombieModel._damage;
            item = zombieView.Items();
            zombieView.SetController(this);
            zombieModel.SetController(this);
        }
        public void EnableZombieController(Transform _playerTransform, Vector3 newPosition)
        {
            health = maxHealth;
            zombieView.gameObject.SetActive(true);
            zombieView.transform.position = newPosition;
            playerTransform = _playerTransform;
            zombieView.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                animator.SetTrigger("Death");
                DropItem();
                Debug.Log("DropItem");
            }
        }
        public Transform GetPlayerTransform()
        {
            return playerTransform;
        }
        public Vector3 GetPosition()
        {
            return zombieView.transform.position;
        }
        public void DropItem()
        {
            // Check if the zombie has already dropped an item
            if (hasDroppedItem)
            {
                return; // Exit the method if the zombie has already dropped an item
            }

            float dropChance = Random.value;

            // Adjust the drop chances based on your desired probabilities
            if (dropChance < 0.5f)
            {
                GameObject.Instantiate(item[0], zombieView.transform.position, Quaternion.identity);
                hasDroppedItem = true;
            }
            else
            {
                GameObject.Instantiate(item[1], zombieView.transform.position, Quaternion.identity);
                hasDroppedItem = true;
            }
        }

        public void EnableZombie(Transform _playerTransform, Vector3 newPosition)
        {
            health = maxHealth;
            zombieView.gameObject.SetActive(true);
            zombieView.transform.position = newPosition;
            playerTransform = _playerTransform;
        }
       
        public void DisableZombie()
        {

            zombieView.gameObject.SetActive(false);
        }
       
        public int GetZombieDamage()
        {
            return damage;
        }
        public ZombieType GetZombieType()
        {
            return zombieModel.ZombieType1;
        }
    }

}
