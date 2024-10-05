using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private bool canMove;
    [SerializeField] private Transform targetPosition; 
    [SerializeField] private float speed; 
    [SerializeField] private ActivationZone activationZone;
    [SerializeField] private float waitTime;

    private Vector3 startPosition;

    //Al empezar cada trampa tendara su posicion inicial y se suscribria al evento de OnPlayerPassed
    private void Start()
    {
        startPosition = transform.position;
        activationZone.OnPlayerPassed += HandlePlayerPassed;
    }

    private void OnDestroy()
    {
        activationZone.OnPlayerPassed -= HandlePlayerPassed;
    }

    //Cuando se llame a este evento y la trampa se pueda mover comenzara la corrutina
    private void HandlePlayerPassed()
    {
        if (canMove)
        {
            StartCoroutine(MoveTrap());
        }
    }

    //Si la trampa toca al jugador morira y comenzara la corrutina de reinicio
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("Slash");
            gameManager.PlayerDead();
            StartCoroutine(RestartLevel());
        }
    }

    private IEnumerator RestartLevel()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySFX("DieMusic");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    //En estas corrtuinas lo que haremos es mover la trampa desde el punto de inicio al punto final indefinidamente

    private IEnumerator MoveTrap()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(targetPosition.position));
            yield return new WaitForSeconds(waitTime);


            yield return StartCoroutine(MoveToPosition(startPosition));
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveToPosition(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
}
