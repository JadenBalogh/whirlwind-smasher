using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Spawner[] spawners;

    private Player player;

    void Start()
    {
        // Get reference to player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.SpawnNext(player.transform.position.x);
        }
    }
}
