using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawn;
    [SerializeField] private float maxSpawn;
    [SerializeField] private Enemy enemyPrefab;

    private Player player;
    private float distanceTraveled;
    private float nextX;
    // private GameObject[] enemies;
    // private GameObject[] powerups;
    // private GameObject[] obstacles;
    private ArrayList enemies = new ArrayList();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SpawnNext();
        // enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // powerups = GameObject.FindGameObjectsWithTag("Powerups");
        // obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
    }

    void Update()
    {
        if (player.transform.position.x >= nextX)
        {
            SpawnNext();
        }
    }

    private void SpawnNext()
    {
        nextX = Random.Range(minSpawn, maxSpawn) + Camera.main.transform.position.x;
        enemies.Add(Instantiate(enemyPrefab, new Vector2(nextX, -3.425f), Quaternion.identity));
    }
}
