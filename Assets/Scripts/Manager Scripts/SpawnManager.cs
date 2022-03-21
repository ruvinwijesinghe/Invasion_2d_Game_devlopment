using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject[] powerupPrefabs; // 0 = tripleShot 1 = speedBoost 2 = Shields

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());

    }// Start

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }// StartSpawnRoutines

    private IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.isGameOver == false)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-7f, 7f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.5f);
        }// EnemySpawnRoutine
    }

    private IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.isGameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerupPrefabs[randomPowerup], new Vector3(Random.Range(-7f, 7f), 7f, 0f), 
                Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }// PowerupSpawnRoutine

}// class
