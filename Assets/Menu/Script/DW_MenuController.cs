using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DW_MenuController : MonoBehaviour
{
    public GameObject _GameOver;
    public GameObject _Settings;
    public GameObject _Pause;
    public AudioSource[] _AudioSources;
    public Slider VolumeSlider;  
    public Slider LuminositySlider;
    public Toggle FullScreenToggle;
    public Image _Luminosity;

    public bool is_in_hospital = false;
    public List<GameObject> destroy_on_load = new List<GameObject>();
    public List<GameObject> enable_on_load = new List<GameObject>();
    public DW_Character Player;
    public DW_SwitchScene exit_forest;

    public bool pauseIsActive = false;

    private void Start()
    {
        _AudioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None );
    }

    private void Update()
    {
        if (_Luminosity != null)
        {
            return;
        }
        _Luminosity = GameObject.Find("luminosity").GetComponent<Image>();
    }
    public void Settings()
    {
        _Settings.SetActive(true);
    }

    public void GoToCharactereSelection()
    {
        SceneManager.LoadScene("ChangeMap");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void Respawn()
    {
        foreach(DW_Class c in DW_ClassController.Instance.classes)
        {
            c.currentHealth = c.maxHealth;
            c.specialSourceAmount = 100;

            c.classSkill.isOnCooldown = false;
            c.classSkill.currentSkillCooldown = c.classSkill.skillCooldown;
            c.shouldBeAggro = false;
        }
        is_in_hospital = exit_forest.isInHospital;
        if(is_in_hospital)
        {
            foreach(GameObject go in destroy_on_load)
            {
                go.SetActive(false);
            }
            foreach(GameObject go in enable_on_load)
            {
                go.SetActive(true);
            }
            Player.transform.position = exit_forest.spawn_pos;
            Player.initial_pos = new Vector2Int((int)Mathf.Abs(exit_forest.spawn_pos.x / 10), (int)Mathf.Abs(exit_forest.spawn_pos.z / 10));

        }
        else
        {
            SceneManager.LoadScene("ChangeMap");
        }
    }

    public void GameOver()
    {
        _GameOver.SetActive(true);
    }

    public void ExitGame()
    {
        UnityEngine.Application.Quit();
    }

    public void ExitSetting()
    {
        _Settings.SetActive(false);
    }

    public void Pause()
    {
        _Pause.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ExitPause()
    {
        _Pause.SetActive(false);
        Time.timeScale = 1.0f;
        pauseIsActive = false;
    }

    public void Volume()
    {
        for(int i = 0; i < _AudioSources.Length; i++)
        {
            _AudioSources[i].volume = VolumeSlider.value;
        }
    }

    public void Luminosity()
    {
        _Luminosity.color = new Color(0,0,0,LuminositySlider.value);
    }

    public void FullScreen()
    {
        UnityEngine.Screen.fullScreenMode = FullScreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

}
