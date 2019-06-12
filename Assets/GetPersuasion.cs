using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetPersuasion : MonoBehaviour
{
    public TextMeshProUGUI skillText;

    public int skillLvl;

    public GameManager gameManager;

    public void Start()
    {
        skillText.text = skillLvl.ToString() + " lvl";
    }

    public void FixedUpdate()
    {
        if (gameManager != null)
        {
            skillLvl = gameManager.persuasionSkill;
            skillText.text = skillLvl.ToString() + " lvl";
        }
    }
}
