using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerMenu : MonoBehaviour
{
    EventSystem evt;

    [Header("Gameobjects")]
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject ExitConfirm;

    [Header("Options")]
    public Options gameOptions;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        gameOptions = GetComponent<Options>();
        gameOptions.LoadOptions();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        evt = EventSystem.current;
    }

    GameObject sel;

    private void Update()
    {
        if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != sel)
            sel = evt.currentSelectedGameObject;
        else if (sel != null && evt.currentSelectedGameObject == null)
            evt.SetSelectedGameObject(sel);
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void onStartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onOptionsClick()
    {
        MainMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(true);
        evt = EventSystem.current;
        evt.SetSelectedGameObject(evt.firstSelectedGameObject);
    }

    public void onFullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void onExitButton()
    {
        MainMenu.gameObject.SetActive(false);
        ExitConfirm.gameObject.SetActive(true);
        evt = EventSystem.current;
        evt.SetSelectedGameObject(evt.firstSelectedGameObject);
    }

    public void onBackButton()
    {
        OptionsMenu.gameObject.SetActive(false);
        ExitConfirm.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        evt = EventSystem.current;
        evt.SetSelectedGameObject(evt.firstSelectedGameObject);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Saved");
        SaveSystem.SaveData(gameOptions);
    }

    public void QuitConfirm()
    {
        Application.Quit();
    }
}
