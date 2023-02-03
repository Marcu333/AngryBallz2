using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class spawn_zombies : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 origin = Vector3.zero;
    public float radius = 10;
    private int oldSecond = 0;
    // Start is called before the first frame update
    void Start()
    {
        oldSecond = System.DateTime.Now.Second;
    }

    // Update is called once per frame
    void Update()
    {
        int second = System.DateTime.Now.Second;
        print(second);
        print(oldSecond);
        if (second % 2 == 0 && second != oldSecond) {
            spawn_zombie();
            oldSecond = second;
        }
    }

    void spawn_zombie() {
        Vector3 randomPosition = origin + UnityEngine.Random.insideUnitSphere * radius;
        randomPosition.x = -Mathf.Abs(randomPosition.x);
        randomPosition.y = 1;
        randomPosition.z = -Mathf.Abs(randomPosition.z);
        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }
}
