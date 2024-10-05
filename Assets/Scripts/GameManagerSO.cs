using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManager")]
public class GameManagerSO : ScriptableObject
{
    private Player player;
    private CanvasManager canvasManager;
    private bool stopGame;
    [NonSerialized] private List<ItemSO> playerInventory = new List<ItemSO>();

    public List<ItemSO> PlayerInventory { get => playerInventory; }
    public bool StopGame { get => stopGame; }

    private void OnEnable()
    {
        //El game manager se suscribe al evento de la carga de una nueva escena
        SceneManager.sceneLoaded += SceneLoaded;
    }

    //Es en este metodo cuando buscamos al player u objetos que queramos tener traqueados
    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindAnyObjectByType<Player>();
        canvasManager = FindAnyObjectByType<CanvasManager>();
        stopGame = false;
    }

    //Eliminamos al jugador y mostramos la interfaz de derrota
    public void PlayerDead()
    {
        stopGame = true;
        player.GetComponent<Player>().enabled = false;
        player.gameObject.SetActive(false);
        canvasManager.EndGameInterface("Has Perdido!!");

    }

    //Eliminamos al jugador y mostramos la interfaz de derrota
    public void TiemOut()
    {
        stopGame = true;
        player.gameObject.SetActive(false);
        canvasManager.EndGameInterface("Has Perdido!!");
    }

    //Eliminamos al jugador y mostramos la interfaz de derrota
    public void PlayerWin()
    {
        stopGame = true;
        player.gameObject.SetActive(false);
        canvasManager.EndGameInterface("Has Ganado!!");
        
    }
}

