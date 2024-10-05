using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private InputManagerSO inputManager;
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    [Header("Floor detection")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask isFloor;

    private CharacterController controller;
    private Animator anim;
    private PlayerPickUp playerPickUp;
    private Vector3 movementDirection;
    private Vector3 inputDirection;
    private Vector3 verticalSpeed;
    private InteractableObject interactuableObject;

    public InteractableObject InteractuableObject { get => interactuableObject; set => interactuableObject = value; }

    private void OnEnable()
    {
        inputManager.OnJump += Jump;
        inputManager.OnMove += Move;
        inputManager.OnInteract += Interact;
    }

    //Para evitar problemas desactivamos la escucha de eventos al deshabilitar el script
    private void OnDisable()
    {
        inputManager.OnJump -= Jump;
        inputManager.OnMove -= Move;
        inputManager.OnInteract -= Interact;
    }


    //Lamada del evento mover
    private void Move(Vector2 context)
    {
        inputDirection = new Vector3(context.x, 0, context.y);
        
    }

    //Lamada del evento saltar
    private void Jump()
    {
        if (playerInFloor())
        {
            anim.SetTrigger("Jump");
            verticalSpeed.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }   
    }

    //Lamada del evento interacruar
    private void Interact()
    {
        anim.ResetTrigger("PickUp");
        if (interactuableObject != null)
        {
            anim.SetTrigger("PickUp");
            interactuableObject.Interact(this);
        }
        if (playerPickUp)
        {
            playerPickUp.PickUp(anim);
        }
    }

   
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerPickUp = GetComponent<PlayerPickUp>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.StopGame) return;

        movementDirection = playerCamera.forward * inputDirection.z + playerCamera.right * inputDirection.x;
        movementDirection.y = 0;
        controller.Move(movementDirection * playerSpeed * Time.deltaTime);
        anim.SetFloat("speed", controller.velocity.magnitude);

        if (movementDirection.sqrMagnitude > 0)
        {
            RotateToTarget();
        }
        if (playerInFloor() && verticalSpeed.y<0)
        {
            verticalSpeed.y = 0;
            anim.ResetTrigger("Jump");
        }
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        verticalSpeed.y += gravity * Time.deltaTime;
        controller.Move(verticalSpeed * Time.deltaTime);
    }

    private bool playerInFloor()
    {
        return Physics.CheckSphere(transform.position, detectionRadius, isFloor);
    }

    private void RotateToTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = targetRotation;
    }

    //Sonidos ejecutados en el animator
    private void playStep()
    {
        string[] stepSounds = { "Step0", "rStep1", "Step2" };
        int randomIndex = Random.Range(0, stepSounds.Length); // Elegimos un índice aleatorio

        AudioManager.Instance.PlaySFX(stepSounds[randomIndex]); // Reproducimos el sonido aleatorio
    }
}
