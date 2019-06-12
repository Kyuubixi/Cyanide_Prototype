using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class OptionsData
{
    public float audioVolume;
    public bool fullscreen;
    public int resolutionWidth;
    public int resolutionHeight;

    public OptionsData (Options options)
    {
        audioVolume = options.audioVolume;
        fullscreen = options.fullscreen;
        resolutionHeight = Screen.currentResolution.height;
        resolutionWidth = Screen.currentResolution.width;
    }
}
