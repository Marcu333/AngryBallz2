using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    public Players playerIndex;

    private Rigidbody _rb;
    private Controls _controls;

    public enum Players
    {
        ONE,
        TWO,
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = new Controls();
        _controls.Player1.Enable();
        _controls.Player2.Enable(); 
    }

    private void Update()
    {
        Vector2 inputVector = ReadPlayerMovementInput();
        _rb.AddForce(inputVector.x * speed * Time.deltaTime, 0, inputVector.y * speed * Time.deltaTime, ForceMode.Acceleration);
    }

    private Vector2 ReadPlayerMovementInput()
    {
        return playerIndex switch
        {
            Players.ONE => _controls.Player1.Move.ReadValue<Vector2>(),
            Players.TWO => _controls.Player2.Move.ReadValue<Vector2>(),
            _ => throw new System.NotImplementedException(),
        };
    }

    public void MoveCommand(CallbackContext input)
    {

       //print("AM VENIT!");
       //Vector2 inputVector = input.ReadValue<Vector2>();
       //_rb.AddForce(inputVector.x * speed * Time.deltaTime, 0, inputVector.y * speed * Time.deltaTime, ForceMode.Acceleration);
    }


}
