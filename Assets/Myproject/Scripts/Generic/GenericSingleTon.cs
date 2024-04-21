using UnityEngine;
namespace FPSZombie.Generic
{
    public class GenericSingleTon<T> : MonoBehaviour where T : GenericSingleTon<T>
    {
        static private T Instance;
        static public T instance { get { return Instance; } }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
