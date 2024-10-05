using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{

    [SerializeField] private Transform playerCamera;
    [SerializeField] private float pickUpDistance;
    [SerializeField] private Transform objectGrabPoint;
    [SerializeField] private LayerMask pickUpLayer;


    private GrabbableObject grabbableObject;

    public void PickUp(Animator anim)
    {
        //Si no tenemos ningun objeto cogido, lo cogemos
        if (grabbableObject == null) {
            
            //Lanzamos un raycast y todo objeto que este en la layer de pick up y tenga el script de grab podra ser cogido
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayer))
            {
                if (raycastHit.transform.TryGetComponent(out grabbableObject))
                {
                    anim.SetTrigger("PickUp");
                    grabbableObject.Grab(objectGrabPoint);
                }
            }
        }
        else
        {
            //Si ya tenemos un objeto en la mano lo soltamos y limpiamos la variable
            grabbableObject.Drop();
            grabbableObject = null;
        }


    }
}
