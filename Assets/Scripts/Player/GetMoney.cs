using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetMoney : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject kulka;

    private int money;

    void Update()
    {
        if (kulka != null)
        {
            money = FindObjectOfType<PlanetDestruction>().GetComponent<PlanetDestruction>().money;
            text.text = money.ToString() + "$";
        }
    }
}
