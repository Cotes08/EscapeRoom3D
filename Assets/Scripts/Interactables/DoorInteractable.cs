using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//Heredamos el objeto interactuable y usamos su mismo metodo
//En este caso cambiamos el interactuar para que ejecute la animacion de la puerta
public class DoorInteractable : InteractableObject
{

    [SerializeField] Animator doorAnim;
    [SerializeField] bool doorLocked;
    [SerializeField] bool needKey;
    [SerializeField] string keyID;

    [SerializeField] private GameObject messageFrame;
    [SerializeField] private TextMeshProUGUI infoText;

    public override void Interact(Player player)
    {
        base.Interact(player);

        //Miramos si la puerta esta bloqueada y necesita una llave, en el caso de que si buscamos en el iventario del player
        //Si tiene la llave se desbloquea la puerta, si no la tiene....
        if (doorLocked && needKey)
        {
            foreach (ItemSO item in gameManager.PlayerInventory)
            {
                if (item.itemID == keyID)
                {
                    doorLocked = false;
                }
            }
            if (doorLocked)
            {
                StartCoroutine(showInfoMessage());
            }
        }

        if (!doorLocked)
        {
            interactableCanvas.SetActive(false);
            doorAnim.Play("OpenDoor");
            AudioManager.Instance.PlaySFX("OpenDoor");
        }
    }

    //Mostramos un mensaje para ayudar al jugador
    IEnumerator showInfoMessage() {
        infoText.text = "Necesitas una llave para abrir esta puerta";
        messageFrame.SetActive(true);
        yield return new WaitForSeconds(3);
        messageFrame.SetActive(false);
    }
}
