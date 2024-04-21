using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.player;

public enum PickupsItem
{
    ammo,
    healthKit
};

public class Pickups : MonoBehaviour
{
    [SerializeField] private  PickupsItem pickupsItem;
    private int ammoAmount = 50;
    private int healAmount = 20;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>()) 
        {
            PlayerView playerView = other.GetComponent<PlayerView>();
            switch(pickupsItem)
            {
                case PickupsItem.ammo:
                    playerView.AddAmmo(ammoAmount);
                    break;
                case PickupsItem.healthKit:
                    playerView.Heal(healAmount);
                    break;
            }
            Destroy(gameObject);
        }
    }

}
