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
        SpawnNextEnemy();
        SpawnNextObstacle();
        // enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // powerups = GameObject.FindGameObjectsWithTag("Powerups");
        // obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
    }

    void Update()
    {
        // Debug.Log(Camera.main.ViewportToWorldPoint(Vector3.right).x);
        // Debug.Log("Mouse " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (player.transform.position.x >= enemyNextX)
        {
            SpawnNextEnemy();
        }
        if (player.transform.position.x >= obstacleNextX)
        {
            SpawnNextObstacle();
        }
    }

    private void SpawnNextEnemy()
    {
        enemyNextX = Camera.main.ViewportToWorldPoint(Vector2.right).x + Random.Range(minSpawn, maxSpawn);
        randomObjects.Add(Instantiate(enemyPrefab, new Vector2(enemyNextX, -3.425f), Quaternion.identity));
    }

    private void SpawnNextObstacle()
    {
        obstacleNextX = Camera.main.ViewportToWorldPoint(Vector2.right).x + Random.Range(minSpawn, maxSpawn);
        randomObjects.Add(Instantiate(obstaclePrefab, new Vector2(obstacleNextX, -3.425f), Quaternion.identity));
    }
}
