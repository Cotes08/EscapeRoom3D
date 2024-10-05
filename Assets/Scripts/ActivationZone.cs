using System;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public event Action OnPlayerPassed;
    
    //Invocamos una accion cuando el jugador pase el area indicada
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerPassed?.Invoke();
        }
    }
}
