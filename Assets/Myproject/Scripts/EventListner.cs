using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListner : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInpt;
   
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shootInput?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            reloadInpt?.Invoke();
        }
    }
}
