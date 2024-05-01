using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.player;

public class MiniMap : MonoBehaviour
{
    private Transform player;
    private void LateUpdate()
    {
        player = PlayerService.instance.GetPlayerTransform();
        Vector3 newPosition = player.transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.transform.rotation.y, 0f);
    }

}
