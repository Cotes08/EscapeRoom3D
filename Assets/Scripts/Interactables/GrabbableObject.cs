using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody objectRb;
    private Transform objectGrabPoint;
    private float lerpSpeed = 10f;

    private void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    //Al agarrar el objeto nos guardamos una copia del punto de agarre, desactivamos la fisicas y congelamos la rotacion
    public void Grab(Transform objectGrabPoint)
    {
        this.objectGrabPoint = objectGrabPoint;
        objectRb.useGravity = false;
        objectRb.freezeRotation = true;
    }

    //Al soltar el objeto ponemos el grab point a nullo, activamos la gravedad y volvemos a activar la rotacion
    public void Drop()
    {
        this.objectGrabPoint = null;
        objectRb.useGravity = true;
        objectRb.freezeRotation = false;
    }

    private void FixedUpdate()
    {
        //Si cogemos un objeto lo movemos de forma suave al punto de agarre
        if (objectGrabPoint != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPoint.position, Time.deltaTime * lerpSpeed);
            objectRb.MovePosition(newPosition);
        }
    }
}
