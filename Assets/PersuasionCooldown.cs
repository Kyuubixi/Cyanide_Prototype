using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersuasionCooldown : MonoBehaviour
{
    public TextMeshProUGUI cooldownText;

    public int cooldownTime = 60;
    public int timer = 0;

    public GameManager gameManager;

    public bool CR_Running = false;

    private void Start()
    {
        cooldownText.text = timer.ToString() + "s";
    }

    public void Update()
    {
        while(timer > 0 && !CR_Running)
        {
            StartCoroutine(cooldownTimer());
        }
    }

    public IEnumerator cooldownTimer()
    {
        CR_Running = true;
        yield return new WaitForSecondsRealtime(1);
        timer -= 1;
        cooldownText.text = timer.ToString() + " s";
        CR_Running = false;
    }
}
