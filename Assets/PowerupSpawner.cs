using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerups;
    public float minSpawn;
    public float maxSpawn;

    

    private List<Transform> spawnPositions;

    private void Start()
    {
        spawnPositions = new List<Transform>();

        foreach (Transform child in transform)
        {
            spawnPositions.Add(child);
        }

        StartSpawning();
    }

    public void SpawnPowerup()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        Transform spawnPosition = spawnPositions[randomIndex];

        int randomPowerup = Random.Range(0, powerups.Length);
        GameObject powerup = powerups[randomPowerup];

        Instantiate(powerup, spawnPosition.position, Quaternion.identity);
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnPowerups()
    {
        while (true)
        {
            SpawnPowerup();
            yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
        }
    }


}
