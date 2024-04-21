using System;

namespace FPSZombie.Event
{
    public class EventService 
    {
        private static EventService instance = null;

        public static EventService Instance
        {
            get
            {
                if (instance == null)
                    instance = new EventService();

                return instance;
            }
        }

        public Action<int> OnZombieDestroy;
        public Action<int> OnSetMaxHealthBar;
        public Action<int> OnSetPlayerHealthBar;
        public Action OnGameOver;

        public void InvokeZombieDestroyer(int zombiesDestroyercount)
        {
            OnZombieDestroy?.Invoke(zombiesDestroyercount);
        }
        public void InvokeSetMaxHealthBar(int maxHealth)
        {
            OnSetMaxHealthBar?.Invoke(maxHealth);
        }
        public void InvokeSetPlayerHealthBar(int health)
        {
            OnSetPlayerHealthBar?.Invoke(health);
        }
        public void InvokeGameOver()
        {
            OnGameOver?.Invoke();
        }
    }
}