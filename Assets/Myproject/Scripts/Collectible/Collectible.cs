
using UnityEngine;
using TMPro;
using FPSZombie.player;

public class Collectible : MonoBehaviour
{
    
  
    private void Start()
    {
        gameObject.SetActive(true);
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerView>())
        {
            SoundsManager.instance.Play(Sounds.PickUps);
            EventListner.OnCollectible?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
