using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class Options : MonoBehaviour
{
    public Slider audioSlider;
    public float audioVolume;
    public int resolutionWidth;
    public int resolutionHeight;

    public AudioMixer mixer;

    public bool fullscreen;

    public void SaveOptions()
    {
        resolutionHeight = Screen.currentResolution.height;
        resolutionWidth = Screen.currentResolution.width;
        audioVolume = audioSlider.value;
        fullscreen = Screen.fullScreen;
        SaveSystem.SaveData(this);
    }

    public void LoadOptions()
    {
        OptionsData data = SaveSystem.LoadData();

        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, data.fullscreen);

            audioVolume = data.audioVolume;
        }
        else
        {
            Debug.LogError("Savefile not found in " + path);
        }
    }
}
