using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnMeteorit : MonoBehaviour
{
    [SerializeField] GameObject[] meteorPrefabs = new GameObject[9];
    public float spawnInterval = 2f;  // Interval waktu antar spawn meteorit
    public float spawnHeight = 5f;  // Tinggi maksimum dan minimum spawn meteorit

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMeteor();
            timer = 0f;
        }
    }

    void SpawnMeteor()
    {
        float randomY = Random.Range(-spawnHeight, spawnHeight);  // Random posisi Y
        int randomIndex = Random.Range(0, meteorPrefabs.Length);  // Random memilih prefab meteorit
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0);  // Posisi spawn meteorit

        Instantiate(meteorPrefabs[randomIndex], spawnPosition, Quaternion.identity);  // Spawn meteorit
    }
}
