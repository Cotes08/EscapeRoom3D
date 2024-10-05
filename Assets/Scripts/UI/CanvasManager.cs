using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private TextMeshProUGUI timerNumber;
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private GameObject endGameMessage;
    [SerializeField] private TextMeshProUGUI endGameText;


    // Update is called once per frame
    void Update()
    {

        if (gameManager.StopGame) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerNumber.text = Mathf.CeilToInt(timer).ToString();
        }
        else
        {
            gameManager.TiemOut();
        }
    }

    public void EndGameInterface(string text)
    {
        endGameMessage.SetActive(true);
        endGameText.text = text;
        StartCoroutine(RestartLevel());
    }


    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
