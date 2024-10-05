using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "InputManager")]
public class InputManagerSO : ScriptableObject
{

    Controls myControls;
    public event Action OnJump;
    public event Action<Vector2> OnMove;
    public event Action OnInteract;

    private void OnEnable()
    {
        //Mapeamos los controles
        myControls = new Controls();
        myControls.Gameplay.Enable();
        myControls.Gameplay.Jump.started += Jump;
        myControls.Gameplay.Move.performed += Move;
        myControls.Gameplay.Move.canceled += Move;
        myControls.Gameplay.Interact.started += Interact;
    }

    //Creamos un evento para que se invoque cierta accino al pulsar dicho boton mapeado
    private void Move(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    private void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }
}
