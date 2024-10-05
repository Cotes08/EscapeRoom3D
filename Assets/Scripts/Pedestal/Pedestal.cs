using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    private Rigidbody barrelRb;

    //Controlamos que el juagdor haya posado el barril encima del pedestal
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            barrelRb = other.GetComponent<Rigidbody>();
            if (barrelRb.useGravity)
            {
                doorAnim.Play("OpenDoor");
                AudioManager.Instance.PlaySFX("Pedestal");
            }
        }
    }
}
