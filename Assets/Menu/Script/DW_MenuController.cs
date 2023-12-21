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

    public void Restart()
    {
        SceneManager.LoadScene("ChangeMap");
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
