using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FPSZombie.Player;
namespace FPSZombie.Zombie
{
    public class ZombieView : MonoBehaviour,EnemyIDamagable
    {
        private ZombieController zombieController;

        private ZombieType zombieType;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private NavMeshAgent navMesh;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject[] item;
        private int _dmg;
        public void SetController(ZombieController _zombieController)
        {
            zombieController = _zombieController;
        }
        private void Start()
        {
            zombieType = zombieController.GetZombieType();
            _dmg = zombieController.GetZombieDamage();

        }
        public GameObject[] Items()
        {
            return item;
        }
        public NavMeshAgent GetNavMeshAgent()
        {
            return navMesh;
        }
        public Rigidbody GetRigidBody()
        {
            return rb;
        }
        public Transform GetPlayerTransform()
        {
            return zombieController.GetPlayerTransform();
        }
        public float GetZombieBPM()
        {
            return 9;
        }
        public void TakeDamage(int damage)
        {
            zombieController.TakeDamage(damage);
        }
        public Animator GetAnimator()
        {
            return animator;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerView>())
            {
                PlayerView playerView = GetComponent<PlayerView>();
                if(playerView != null)
                {
                    playerView.TakeDamage(_dmg);
                }
            }
        }
    }

}
