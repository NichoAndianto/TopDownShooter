using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemyPrefab;   
    public GameObject enemyPrefab2;  
    public GameObject enemyPrefab3;  

    [Header("Spawn Settings")]
    public float spawnRadius = 6f;

    [Header("Wave Settings")]
    public int startEnemyCount = 5;
    public int waveIncrement = 3;
    public float delayBetweenWaves = 2f;

    [Header("UI")]
    public TMP_Text enemyCountText;
    public TMP_Text waveText;

    private int currentWave = 1;
    private List<GameObject> enemies = new List<GameObject>();
    private bool spawning = false;
    private bool waitingNextWave = false;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        enemies.RemoveAll(e => e == null);
        UpdateUI();

        if (enemies.Count == 0 && !spawning && !waitingNextWave)
        {
            waitingNextWave = true;
            StartCoroutine(NextWave());
        }
    }

   
    System.Collections.IEnumerator SpawnWave()
    {
        spawning = true;
        waitingNextWave = false;

        int enemyCount = startEnemyCount + (waveIncrement * (currentWave - 1));

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            GameObject prefabToSpawn = GetEnemyPrefabForWave();

            GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            enemies.Add(newEnemy);

            yield return new WaitForSeconds(0.2f);
        }

        spawning = false;
        UpdateUI();
    }

    
    GameObject GetEnemyPrefabForWave()
    {
        if (currentWave == 1)
            return enemyPrefab;

        if (currentWave == 2)
        {
            
            return Random.value < 0.5f ? enemyPrefab : enemyPrefab2;
        }

        
        float rand = Random.value;
        if (rand < 0.33f) return enemyPrefab;
        if (rand < 0.66f) return enemyPrefab2;
        return enemyPrefab3;
    }

    
    System.Collections.IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBetweenWaves);
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    
    void UpdateUI()
    {
        if (enemyCountText != null)
            enemyCountText.text = "Enemies Left: " + enemies.Count;

        if (waveText != null)
            waveText.text = "Wave: " + currentWave;
    }
}
