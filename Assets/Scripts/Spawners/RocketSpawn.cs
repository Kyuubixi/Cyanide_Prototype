using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawn : MonoBehaviour
{
    private float spawnTimer, spawnTimerMax = 2f;
    public GameObject rocketPrefab;

    private void Update()
    {
        if (spawnTimer <= 0)
        {
            spawnTimer = 0f;

            Instantiate(rocketPrefab, transform.position + new Vector3(Random.Range(-100f, 100f), 60f, 0f), Quaternion.Euler(90f, 0f, 0f));

            spawnTimer = spawnTimerMax;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}
