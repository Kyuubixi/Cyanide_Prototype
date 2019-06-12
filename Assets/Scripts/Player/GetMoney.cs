using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetMoney : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public GameObject kulka;

    private int money;

    void FixedUpdate()
    {
        if (kulka != null)
        {
            money = FindObjectOfType<PlanetDestruction>().GetComponent<PlanetDestruction>().money;
            moneyText.text = money.ToString() + "$";
        }
    }
}
