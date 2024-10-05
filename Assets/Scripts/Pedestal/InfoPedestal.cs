using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPedestal : MonoBehaviour
{
    [SerializeField] private GameObject messageFrame;
    [SerializeField] private TextMeshProUGUI infoText;

    private void OnTriggerEnter(Collider other)
    {
        //Cuando el payer entre mostramos un mensaje de ayuda
        if (other.CompareTag("Player"))
        {
            infoText.text = "Necesitas dejar encima un objeto pesado para abrir la puerta";
            messageFrame.SetActive(true);
        }
    }

    //Cuando el player salga se lo desactivamos
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageFrame.SetActive(false);
        }
    }
}

