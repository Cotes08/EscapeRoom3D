using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected GameObject interactableCanvas;
    [SerializeField] protected GameManagerSO gameManager;

    protected Player player;
    protected Collider interactableCollider;


    protected virtual void OnTriggerEnter(Collider other)
    {
        //Comprobamos si es el player el que esta en el trigger
        //Si es asi le permitimos interactuar y la flecha
        if (other.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.GetComponent<Player>();
            }

            if (player != null)
            {
                interactableCanvas.SetActive(true);
                player.InteractuableObject = this;
            }
        }
        

    }

    protected virtual void OnTriggerExit(Collider other)
    {
        //Comprobamos si el player se ha ido del trigger
        //Si es asi le quitamos la interaccion y la flecha
        if (other.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.GetComponent<Player>();
            }

            if (player != null)
            {
                interactableCanvas.SetActive(false);
                player.InteractuableObject = null;
            }
        }
    }

    public virtual void Interact(Player player) 
    {}
}
