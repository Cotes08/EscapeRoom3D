using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardWorldUI : MonoBehaviour
{
    //Este codgigo sirve para que el indicador de ineraccion con un objeto siempre gire en la direccion de la camara

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform.position + mainCamera.transform.forward);
        }
    }
}
