using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Valve.VR.InteractionSystem;

public class Wave : MonoBehaviour
{
    public Transform[] waveSpawnPoints;
    public GameObject enemyPrefab;

    private GameObject[] enemies;

    public void SpawnEnemies()
    {
        enemies = new GameObject[waveSpawnPoints.Length];
        for (int i = 0; i < waveSpawnPoints.Length; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, waveSpawnPoints[i]);

            Debug.Log("MARC LOG: enemy: " + enemies[i]);
        }

    }

    public int EnemiesLeft()
    {
        int left = 0;
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i])
                left++;

            Debug.Log("MARC LOG: enemy in left: " + enemies[i]);
        }
        return left;
    }
}
