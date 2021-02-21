using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] prefabs;
    [SerializeField] private float minDist;
    [SerializeField] private float maxDist;
    [SerializeField] private float probability;

    private float nextX;
    private ArrayList spawnables = new ArrayList();

    void Start()
    {
        nextX = Camera.main.ViewportToWorldPoint(Vector2.right).x + Random.Range(minDist, maxDist);
    }

    public void SpawnNext(float playerX)
    {
        if (playerX >= nextX)
        {
            // Update nextX
            nextX = Camera.main.ViewportToWorldPoint(Vector2.right).x + Random.Range(minDist, maxDist);

            if (Random.Range(0f, 1f) <= probability)
            {
                // Pick a random prefab to spawn
                int index = Random.Range(0, prefabs.Length);
                spawnables.Add(Instantiate(prefabs[index], new Vector2(nextX, -3.425f), Quaternion.identity));
            }
        }
    }
}
