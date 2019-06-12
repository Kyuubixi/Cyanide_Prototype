using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDestruction : MonoBehaviour
{
    public GameObject explosionPrefab;

    public GameManager gameManager;
    public ScreenShake screenShake;

    public TrailRenderer getTrailRenderer;

    public int score = 0, money = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Planeta"))
        {
            StartCoroutine(PlanetDestroy(other));
            StartCoroutine(screenShake.Shake(gameManager.screenShakeDuration, gameManager.screenShakeMagnitude));
            getTrailRenderer.time += 0.1f;
            score++;
            if (score < 20)
            {
                money += Random.Range(2, 5);
            }
            else if (score >= 20 && score < 50)
            {
                money += Random.Range(5, 10);
            }
            else if (score >= 50 && score <= 100)
            {
                money += Random.Range(10, 15);
            }
            else
            {
                money += Random.Range(15, 25);
            }

            if(score % 10 == 0 && score > 0)
            {
                gameManager.persuasionSkill += 1;
            }

            gameManager.explosionSource.PlayOneShot(gameManager.explosionSFX);
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
