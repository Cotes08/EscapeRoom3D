using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private GameObject messageFrame;
    [SerializeField] private TextMeshProUGUI infoText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RestartLevel());
        }
    }

    //Cuando el jugador entre lanzamos la corrutina de victoria

    private IEnumerator RestartLevel()
    {
        infoText.text = "Teleporting...";
        messageFrame.SetActive(true);
        AudioManager.Instance.PlaySFX("Teleport");
        yield return new WaitForSeconds(2);
        messageFrame.SetActive(false);
        gameManager.PlayerWin();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }
}
