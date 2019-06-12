using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public GameObject kulka;

    private int score;

    void FixedUpdate()
    {
        if (kulka != null)
        {
            score = FindObjectOfType<PlanetDestruction>().GetComponent<PlanetDestruction>().score;
            scoreText.text = score.ToString();
        }
    }
}
