using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlanetSpawn : MonoBehaviour
{
    public GameObject planetPrefab;

    public DestroyPlanetOnTriggerExit numOfPlanets;

    public float spawnTimerMax;
    private float spawnTimer;

    public float radius;

    public static Vector3 RandomPointOnUnitCircle(float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;

        return new Vector3(x, y, 0f);
    }


    private void Update()
    {
        if (spawnTimer <= 0 && numOfPlanets.amountOfObjects <= 15)
        {
            spawnTimer = 0f;

            GameObject planet = Instantiate(planetPrefab, transform.position + RandomPointOnUnitCircle(radius), Quaternion.identity) as GameObject;

            float randomSize = Random.Range(5f, 20f);

            planet.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

            spawnTimer = spawnTimerMax;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

}
