using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawn;
    [SerializeField] private float maxSpawn;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Obstacle obstaclePrefab;

    private Player player;
    private float distanceTraveled;
    private float enemyNextX;
    private float obstacleNextX;
    private float powerupNextX;
    // private GameObject[] enemies;
    // private GameObject[] powerups;
    // private GameObject[] obstacles;
    private ArrayList randomObjects = new ArrayList();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SpawnNext(enemyPrefab, ref enemyNextX);
        SpawnNext(obstaclePrefab, ref obstacleNextX);
        // enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // powerups = GameObject.FindGameObjectsWithTag("Powerups");
        // obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
    }

    void Update()
    {
        if (player.transform.position.x >= enemyNextX)
        {
            SpawnNext(enemyPrefab, ref enemyNextX);
        }
        if (player.transform.position.x >= obstacleNextX)
        {
            SpawnNext(obstaclePrefab, ref obstacleNextX);
        }
    }

    private void SpawnNext(MonoBehaviour prefab, ref float nextX)
    {
        nextX = Camera.main.ViewportToWorldPoint(Vector2.right).x + Random.Range(minSpawn, maxSpawn);
        randomObjects.Add(Instantiate(prefab, new Vector2(nextX, -3.425f), Quaternion.identity));
    }
}
