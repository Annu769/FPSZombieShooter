using UnityEngine;
namespace FPSZombie.ScriptableObjects
{
    [CreateAssetMenu (fileName = "EnemyScriptableObjectList", menuName = "ScriptableObject/NewEnemyList")]
    public class EnemyScriptableObjectList : ScriptableObject
    {
        public EnemyScriptableObject[] enemy;
    }
}