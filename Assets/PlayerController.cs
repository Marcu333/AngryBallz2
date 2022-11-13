using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 100;
    private Controls _controls;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = new Controls();
        _controls.Player.Enable();
    }

    private void Update()
    { 
        Vector2 inputVector = _controls.Player.MoveP1.ReadValue<Vector2>();
        _rb.AddForce(inputVector.x * speed * Time.deltaTime, 0, inputVector.y * speed * Time.deltaTime, ForceMode.Acceleration);

    }

    public void MoveCommand(CallbackContext input)
    {

       //print("AM VENIT!");
       //Vector2 inputVector = input.ReadValue<Vector2>();
       //_rb.AddForce(inputVector.x * speed * Time.deltaTime, 0, inputVector.y * speed * Time.deltaTime, ForceMode.Acceleration);
    }


}
