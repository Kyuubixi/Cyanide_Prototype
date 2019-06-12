using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool isPaused = false;

    [Header("Gameobjects")]
    public GameObject kulka;
    public GameObject deathScreen;
    public GameObject pauseScreen;
    public GameObject shopScreen;
    public GameObject mainEventSystem;

    [Header("Boost")]
    public Slider boostSlider;
    public TrailRenderer trailRenderer;

    [Header("Screenshake")]
    public ScreenShake screenShake;
    public float screenShakeDuration;
    public float screenShakeMagnitude;

    [Header("Death")]
    public bool screnshakeWasExecuted = false;

    private void OnApplicationPause(bool pauseCheck)
    {
        if (pauseCheck && kulka != null)
        {
            enablePauseScreen();
        }
    }

    GameObject sel; // selected gameobject in menu
    EventSystem evt; // event system

    private void Start()
    {
        evt = EventSystem.current;
        disablePauseScreen();
    }

    private void Update()
    {
        if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != sel)
            sel = evt.currentSelectedGameObject;
        else if (sel != null && evt.currentSelectedGameObject == null)
            evt.SetSelectedGameObject(sel);

        if(kulka == null)
        {
            deathScreen.SetActive(true);
            evt.SetSelectedGameObject(evt.firstSelectedGameObject);
            mainEventSystem.SetActive(false);
        }

        if ((Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7)) && isPaused == false && kulka != null && shopScreen.activeSelf == false)
        {
            enablePauseScreen();
        }
        else if((Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7)) && isPaused == true && kulka != null && shopScreen.activeSelf == false)
        {
            disablePauseScreen();
        }

        if (kulka != null)
        {
            boostSlider.value = trailRenderer.time;
        }

        if (kulka == null)
        {
            screenshakeOnce();
        }

        if(Input.GetKeyUp(KeyCode.P) && shopScreen.activeSelf == false && kulka != null)
        {
            boostSlider.gameObject.SetActive(false);
            shopScreen.SetActive(true);
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            kulka.GetComponent<Movement>().movementSpeed = 0.01f;
            
        }
        else if(Input.GetKeyUp(KeyCode.P) && shopScreen.activeSelf == true && kulka != null)
        {
            boostSlider.gameObject.SetActive(true);
            shopScreen.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            kulka.GetComponent<Movement>().movementSpeed = 0.5f;
        }
    }

    private void enablePauseScreen()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        evt.SetSelectedGameObject(evt.firstSelectedGameObject);
        mainEventSystem.SetActive(false);
    }

    private void disablePauseScreen()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        mainEventSystem.SetActive(true);
    }


    public void onContinueClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onMenuClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void onContinuePause()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    private void screenshakeOnce()
    {
        if(screnshakeWasExecuted == false)
        {
            StartCoroutine(screenShake.Shake(screenShakeDuration, screenShakeMagnitude));
            screnshakeWasExecuted = true;
        }
    }


}
