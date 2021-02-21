using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawningAlgorithm : MonoBehaviour
{
    private Player player;
    private float distanceTraveled;
    private float nextDistance;
    [SerializeField]private float minSpawn;
    [SerializeField]private float maxSpawn;
    private GameObject[] enemies;
    private GameObject[] powerups;
    private GameObject[] obstacles;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        nextDistance = Random.Range(minSpawn, maxSpawn);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        powerups = GameObject.FindGameObjectsWithTag("Powerups");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
    }

    void Update()
    {
        distanceTraveled = player.transform.position.x;
        if(distanceTraveled == nextDistance) {
            
            nextDistance = Random.Range(minSpawn + distanceTraveled, maxSpawn + distanceTraveled);
        }
    }
}
