using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 200f;

    [SerializeField] private Transform player;


    private float mouseX;
    private float mouseY;
    private float rotation;
    private float minAngle = -90f;
    private float maxAngle = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(rotation, 0, 0);
        player.Rotate(Vector3.up * mouseX);
    }

}
