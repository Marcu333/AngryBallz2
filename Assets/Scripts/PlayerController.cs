using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour, ICollisionEntity
{
    public PlayerStats stats;
    public List<CollisionEvent> collisionEvents;
    public List<TriggerEvent> triggerEvents;


    private Rigidbody _rb;
    private Controls _controls;
    private float _attack;
    private float _defense;
    private int _lives;
    private UIManager _UIManager;

    Coroutine coroutine;
    
    public int Lives
    {
        get { return _lives; }
        set { 
            if(value <= 0)
            {
                RestartScene();
            }
            _lives = value;  _UIManager.UpdatePlayerLives(this); }
    }

    private Vector3 spawnPos;

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
        _UIManager = FindObjectOfType<UIManager>();
        Lives = (int)stats.startLives;
        _controls = new Controls();
        _controls.Player1.Enable();
        _controls.Player2.Enable();
        _attack = stats.attack;
        _defense = stats.defense;
        spawnPos = transform.position;
        _UIManager.UpdatePlayerLives(this);
    }

    private void Update()
    {
        Vector2 inputVector = ReadPlayerMovementInput();
        _rb.AddForce(inputVector.x * stats.speed * Time.deltaTime, 0, inputVector.y * stats.speed * Time.deltaTime, ForceMode.Acceleration);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (CollisionEvent collisionEvent in collisionEvents)
            if (collision.gameObject.CompareTag(collisionEvent.tag))
                collisionEvent.events.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (TriggerEvent collisionEvent in triggerEvents)
            if (other.gameObject.CompareTag(collisionEvent.tag))
                collisionEvent.events.Invoke(other);
    }
    #endregion

    #region ICollisionEntity
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    public bool IsImmutable()
    {
        return Defense >= 999;
    }

    public void Restart(Collision collision)
    {
        transform.position = spawnPos;
        _rb.velocity = Vector3.zero;
        Lives--;
    }

    public void PushOther(Collision collision)
    {
        /*
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
        */

        // Calculate Angle Between the collision point and the player
        Vector3 dir = collision.contacts[0].point - transform.position;
        // We then get the opposite (-Vector3) and normalize it
        dir = -dir.normalized;
        // And finally we add force in the direction of dir and multiply it by force. 
        // This will push back the player
        GetComponent<Rigidbody>().AddForce(dir * Attack);

    }

    #endregion

    #region Types
    [System.Serializable]
    public struct CollisionEvent
    {
        public string tag;
        public UnityEvent<Collision> events;
    }

    [System.Serializable]
    public struct TriggerEvent
    {
        public string tag;
        public UnityEvent<Collider> events;
    }

    public enum Players
    {
        ONE,
        TWO,
    }
    


    public void ActivatePowerUp(Collider powerUp)
    {
        PowerUp pw = powerUp.GetComponent<PowerUp>();
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(PowerUpRoutine(pw.duration));
        Destroy(pw.gameObject);  
    }

    IEnumerator PowerUpRoutine(float time)
    {
        Attack = 1;
        yield return new WaitForSeconds(time);
        Attack = stats.attack;
    }



    

    #endregion
}
