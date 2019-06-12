using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessChange : MonoBehaviour
{
    public PostProcessVolume ppProfile;

    Bloom bloomLayer;
    public float bloomAmount = 10f;

    ColorGrading colorgradingLayer;
    public float colorgradingAmount = 0f;

    private void Start()
    {
        ppProfile = GetComponent<PostProcessVolume>();

        ppProfile.profile.TryGetSettings(out bloomLayer);
        bloomLayer.enabled.value = true;

        ppProfile.profile.TryGetSettings(out colorgradingLayer);
        colorgradingLayer.enabled.value = true;
    }

    private void FixedUpdate()
    {
        float bloomValue = Mathf.PingPong((Time.time / 3), 1);
        bloomAmount = Mathf.Lerp(9f, 15f, bloomValue);

        float colorgradingValue = Mathf.PingPong((Time.time / 10), 1);
        colorgradingAmount = Mathf.Lerp(-100f, 100f, colorgradingValue);

        bloomLayer.intensity.value = bloomAmount;
        colorgradingLayer.tint.value = colorgradingAmount;
    }
}
