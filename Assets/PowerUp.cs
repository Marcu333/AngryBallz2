using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 5;
    public float lifeSpan = 3;

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
}
