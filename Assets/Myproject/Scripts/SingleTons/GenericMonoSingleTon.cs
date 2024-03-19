using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FPSZombie.SingleTon
{
    public class GenericMonoSingleTon<T> : MonoBehaviour where T :GenericMonoSingleTon<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }
        private void Awake()
        {
            if(instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

}
