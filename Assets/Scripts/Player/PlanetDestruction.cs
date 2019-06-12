using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDestruction : MonoBehaviour
{
    public GameObject explosionPrefab;

    public GameManager gameManager;
    public ScreenShake screenShake;

    public TrailRenderer getTrailRenderer;

    public int score = 0;
    public int money = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Planeta"))
        {
            StartCoroutine(PlanetDestroy(other));
            StartCoroutine(screenShake.Shake(gameManager.screenShakeDuration, gameManager.screenShakeMagnitude));
            getTrailRenderer.time += 0.1f;
            score++;
            money += 10;
        }
    }

    private IEnumerator PlanetDestroy(Collider other)
    {
        other.gameObject.transform.localScale = Vector3.Lerp(other.gameObject.transform.localScale, new Vector3(0f, 0f, 0f), 20 * Time.deltaTime);

        Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);

        yield return new WaitForEndOfFrame();

        Destroy(other.gameObject);
    }
}
