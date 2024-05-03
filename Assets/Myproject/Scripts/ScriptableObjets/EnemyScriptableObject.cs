using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.Zombie
    ;
namespace FPSZombie.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/NewZombie")]
    public class EnemyScriptableObject : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private int speed;
        [SerializeField] private int damage;
        [SerializeField] public ZombieView zombieView;
        public int Health { get => health; set => health = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Damage { get => damage; set => damage = value; }
       
       
    }
}
