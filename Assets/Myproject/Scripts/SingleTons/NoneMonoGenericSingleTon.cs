
using UnityEngine;
namespace FPSZombie.SingleTon
{
    public class NoneMonoGenericSingleTon<T> where T : NoneMonoGenericSingleTon<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        private void Awake()
        {
            if(instance == null)
            {
                instance = (T)this;
            }
        }
    }
}

