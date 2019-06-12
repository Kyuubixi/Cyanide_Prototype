using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour, ISelectHandler, ISubmitHandler
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

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip selectSFX;
    public AudioClip pressSFX;
    public AudioSource explosionSource;
    public AudioClip explosionSFX;

    [Header("Shop")]
    public int boostCost;
    public TextMeshProUGUI boostCostText;
    public int speedCost;
    public TextMeshProUGUI speedCostText;
    public Button boostBuyButton;
    public Button speedBuyButton;

    private int money;

    public int persuasionSkill = 1;
    public PersuasionCooldown persuasionCooldown;

    private void OnApplicationPause(bool pauseCheck)
    {
        if (pauseCheck && kulka != null && shopScreen.activeSelf == false)
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
        boostCostText.text = boostCost + "$";
        speedCostText.text = speedCost + "$";
    }

    private void Update()
    {
        if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != sel)
            sel = evt.currentSelectedGameObject;
        else if (sel != null && evt.currentSelectedGameObject == null)
            evt.SetSelectedGameObject(sel);

        if (kulka != null)
        {
            money = FindObjectOfType<PlanetDestruction>().money;
        }

        if (kulka == null)
        {
            deathScreen.SetActive(true);
            evt = EventSystem.current;
            mainEventSystem.SetActive(false);
            screenshakeOnce();
        }

        if ((Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7)) && isPaused == false && kulka != null && shopScreen.activeSelf == false)
        {
            enablePauseScreen();
        }
        else if ((Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7)) && isPaused == true && kulka != null && shopScreen.activeSelf == false)
        {
            disablePauseScreen();
        }

        if (kulka != null)
        {
            boostSlider.value = trailRenderer.time;
        }

        if (Input.GetKeyUp(KeyCode.P) && shopScreen.activeSelf == false && kulka != null)
        {
            mainEventSystem.SetActive(false);
            shopScreen.SetActive(true);
            isPaused = true;
            evt = EventSystem.current;
            evt.SetSelectedGameObject(evt.firstSelectedGameObject);
            Time.timeScale = 0;
            if(money < boostCost)
            {
                boostBuyButton.interactable = false;
            }
            else
            {
                boostBuyButton.interactable = true;
            }
            if (money < speedCost)
            {
                speedBuyButton.interactable = false;
            }
            else
            {
                speedBuyButton.interactable = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.P) && shopScreen.activeSelf == true && kulka != null)
        {
            mainEventSystem.SetActive(true);
            shopScreen.SetActive(false);
            isPaused = false;
            evt = EventSystem.current;
            evt.SetSelectedGameObject(evt.firstSelectedGameObject);
            Time.timeScale = 1;
        }
    }

    private void enablePauseScreen()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        mainEventSystem.SetActive(false);
        evt = EventSystem.current;
        evt.SetSelectedGameObject(evt.firstSelectedGameObject);

    }

    private void disablePauseScreen()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        mainEventSystem.SetActive(true);
        evt = EventSystem.current;
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
        if (screnshakeWasExecuted == false)
        {
            StartCoroutine(screenShake.Shake(screenShakeDuration, screenShakeMagnitude));
            screnshakeWasExecuted = true;
            explosionSource.PlayOneShot(explosionSFX);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        audioSource.PlayOneShot(selectSFX);
        Debug.Log("Selected");
    }

    public void OnSubmit(BaseEventData eventData)
    {
        audioSource.PlayOneShot(pressSFX);
        Debug.Log("Selected");
    }

    public void BoostBuy()
    {
        if (money >= boostCost)
        {
            trailRenderer.time = 3f;
            FindObjectOfType<PlanetDestruction>().money -= boostCost;
            boostCost += Random.Range(3, 10);
            boostCostText.text = boostCost + "$";
        }
    }

    public void SpeedBuy()
    {
        if (money >= speedCost)
        {
            kulka.GetComponent<Movement>().normalMovementSpeed += 0.1f;
            FindObjectOfType<PlanetDestruction>().money -= speedCost;
            speedCost += 50;
            speedCostText.text = speedCost + "$";
        }
    }

    public void tryPersuade()
    {
        if (persuasionCooldown.timer <= 0)
        {
            shopPersuadeTest();
        }
        if (money < boostCost)
        {
            boostBuyButton.interactable = false;
        }
        else
        {
            boostBuyButton.interactable = true;
        }
    }

    public void shopPersuadeTest()
    {
        if (Random.Range(0, 2) == 1)
        {
            shopReduceCost();
        }
        persuasionCooldown.timer = persuasionCooldown.cooldownTime;
    }
    
    public void shopReduceCost()
    {
        if(persuasionSkill == 0 && boostCost > 15)
        {
            boostCost -= Random.Range(1, 5);
            Debug.Log("Reduced");
        }
        else if(persuasionSkill == 1 && boostCost > 20)
        {
            boostCost -= Random.Range(5, 10);
            Debug.Log("Reduced");
        }
        else if (persuasionSkill == 2 && boostCost > 25)
        {
            boostCost -= Random.Range(10, 15);
            Debug.Log("Reduced");
        }
        else if (persuasionSkill == 3 && boostCost > 30)
        {
            boostCost -= Random.Range(15, 20);
            Debug.Log("Reduced");
        }
        else if (persuasionSkill == 4 && boostCost > 35)
        {
            boostCost -= Random.Range(20, 25);
            Debug.Log("Reduced");
        }
        else if (persuasionSkill == 5 && boostCost > 40)
        {
            boostCost -= Random.Range(25, 30);
            Debug.Log("Reduced");
        }
        else if (boostCost > 50)
        {
            boostCost -= Random.Range(30, 40);
            Debug.Log("Reduced");
        }
        boostCostText.text = boostCost.ToString() + "$";
    }

    public void shopExit()
    {
        shopScreen.SetActive(false);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        evt = mainEventSystem.GetComponent<EventSystem>();
    }
}
