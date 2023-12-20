using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DW_MenuController : MonoBehaviour
{
    public GameObject _GameOver;
    public GameObject _Settings;
    public GameObject _Pause;
    public List<AudioSource> _AudioSources;
    public Slider VolumeSlider;  
    public Slider LuminositySlider;
    public Toggle FullScreenToggle;
    public Image _Luminosity;

    public void Settings()
    {
        _Settings.SetActive(true);
    }

    public void GoToCharactereSelection()
    {
        SceneManager.LoadScene("MeCScene");
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
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
    }

    public void Volume()
    {
        for(int i = 0; i < _AudioSources.Count; i++)
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
