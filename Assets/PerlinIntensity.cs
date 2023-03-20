using UnityEngine;

public class PerlinIntensity : MonoBehaviour
{
    public float speed = 1f;        // Controls how quickly the noise changes
    public float scale = 1f;        // Controls the size of the noise pattern
    public float amplitude = 1f;    // Controls the height of the noise values
    public float frequency = 1f;    // Controls how often the noise values change

    private float time = 0f;

    private void Start()
    {
        time = Random.Range(0f, 10f);
    }


    private void Update()
    {
        time += Time.deltaTime * speed;

        float noiseValue = Mathf.PerlinNoise(time * frequency, 0f) * amplitude;
        float intensity = noiseValue * scale;

        foreach (Light light in GetComponentsInChildren<Light>())
        {
            light.intensity = intensity;
        }

    }
}
