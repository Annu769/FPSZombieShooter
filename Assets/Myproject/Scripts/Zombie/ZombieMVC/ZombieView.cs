using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using FPSZombie.player;

namespace FPSZombie.zombie
{
    public class ZombieView : MonoBehaviour,EnemyIDamagable
    {
        private ZombieController zombieController;
        
        private ZombieType zombieType;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private NavMeshAgent navMesh;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem bloodImpact;
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
        public ParticleSystem GetParticle()
        {
            return bloodImpact;
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
        public  void TakeDamage(int damage)
        {
            zombieController.TakeDamage(damage);
        }
        public Animator GetAnimator()
        {
            return animator;
        }
        public void ZombieAttackSound()
        {
            SoundsManager.instance.Play(Sounds.ZombieAttack);
        }
        public void ZombieSound()
        {
            SoundsManager.instance.Play(Sounds.Zombie);
        }
        private void OnCollisionEnter(Collision collision)
        {
           if(collision.gameObject.GetComponent<PlayerView>())
            {
                PlayeryIDamagable idamage = collision.gameObject.GetComponent<PlayeryIDamagable>();
                idamage.TakeDamage(_dmg);
            }
        }
        public void DisableZombie()
        {
            ZombieService.instance.DestroyZombie(zombieController, zombieType);
        }        
    }
}
