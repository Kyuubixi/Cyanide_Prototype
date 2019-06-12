using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetScore : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject kulka;

    private int score;

    void FixedUpdate()
    {
        if (kulka != null)
        {
            score = FindObjectOfType<PlanetDestruction>().GetComponent<PlanetDestruction>().score;
            text.text = score.ToString();
        }
    }
}
