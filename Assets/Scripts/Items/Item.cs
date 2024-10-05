using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractableObject
{
    [SerializeField] private ItemSO itemData;//Datos obtenidos por el ScriptableObject

    public override void Interact(Player player)
    {
        //Añadimos la llave a nuestro inventario y la destruimos
        base.Interact(player);
        gameManager.PlayerInventory.Add(itemData);
        AudioManager.Instance.PlaySFX("Item");
        Destroy(gameObject);
    }
}
