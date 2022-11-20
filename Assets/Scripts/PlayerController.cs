using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour, ICollisionEntity
{
    public PlayerStats stats;
    public List<CollisionEvent> collisionEvents;   

    private Rigidbody _rb;
    private Controls _controls;
    private float _attack;
    private float _defense;

    private Vector2 ReadPlayerMovementInput() => stats.playerIndex switch
    {
        Players.ONE => _controls.Player1.Move.ReadValue<Vector2>(),
        Players.TWO => _controls.Player2.Move.ReadValue<Vector2>(),
        _ => throw new System.NotImplementedException(),
    };

    #region Callbacks
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = new Controls();
        _controls.Player1.Enable();
        _controls.Player2.Enable();
        _attack = stats.attack;
        _defense = stats.defense;
    }

    private void Update()
    {
        Vector2 inputVector = ReadPlayerMovementInput();
        _rb.AddForce(inputVector.x * stats.speed * Time.deltaTime, 0, inputVector.y * stats.speed * Time.deltaTime, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (CollisionEvent collisionEvent in collisionEvents)
            if (collision.gameObject.CompareTag(collisionEvent.tag))
                collisionEvent.events.Invoke(collision);
    }
    #endregion

    #region ICollisionEntity
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    public bool IsImmutable()
    {
        return Defense >= 999;
    }

    public void PushOther(Collision collision)
    {
        if (collision.gameObject.GetComponent<ICollisionEntity>().IsImmutable()) return;
        var mySpeedToOther =  _rb.velocity.magnitude / collision.rigidbody.velocity.magnitude;
        //if (stats.playerIndex == Players.ONE)
            print(mySpeedToOther);
        if(mySpeedToOther > 1)
        {
            //Push with Attack
            collision.gameObject.GetComponent<Rigidbody>().AddForce(mySpeedToOther * (-collision.impulse * 0.1f) * Attack / collision.gameObject.GetComponent<ICollisionEntity>().Defense, ForceMode.VelocityChange);

        }
        else
        {
            //Push reverse with Defense
            collision.gameObject.GetComponent<Rigidbody>().AddForce(mySpeedToOther * (collision.impulse * 0.1f) * Defense / collision.gameObject.GetComponent<ICollisionEntity>().Defense, ForceMode.VelocityChange);

        }

    }

    #endregion

    #region Types
    [System.Serializable]
    public struct CollisionEvent
    {
        public string tag;
        public UnityEvent<Collision> events;
    }
    public enum Players
    {
        ONE,
        TWO,
    }

    #endregion
}
