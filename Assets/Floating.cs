using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
   public float amplitude = 0.5f; // Controls the maximum height of the float
    public float frequency = 1f;   // Controls how quickly the float moves up and down
    public float rotationSpeed = 50f; // Controls the speed of rotation

    private float time = 0f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        time += Time.deltaTime;
        float newY = startPosition.y + Mathf.Sin(time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        float newRotation = Time.time * rotationSpeed;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }
}
