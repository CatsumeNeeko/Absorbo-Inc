using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 3f;
    public float spawnRadius = 5f;
    public int initialWaveSize = 5;
    public int enemiesPerWaveIncrement = 2;

    private int waveCount = 1;
    private int enemiesToSpawn;

    void Start()
    {

        SpawnWave();
    }

    void SpawnWave()
    {

        enemiesToSpawn = initialWaveSize + (waveCount - 1) * enemiesPerWaveIncrement;


        for (int i = 0; i < enemiesToSpawn; i++)
        {

            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];


            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;


            randomPosition.y = 0f;
            


            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }


        waveCount++;


        Invoke("SpawnWave", spawnInterval);
    }

}
