using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Text volumeNumber;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject menuHolder;
    [SerializeField] private GameObject settingsHolder;

    private void Awake()
    {
        menuHolder.SetActive(true);
        settingsHolder.SetActive(false);
        
        var volume = audioSource.volume;
        volumeNumber.text = (int)(volume * 100) + " %";
        slider.value = volume;
    }

    public void SettingMenu()
    {
        menuHolder.SetActive(false);
        settingsHolder.SetActive(true);
    }

    public void GoBack()
    {
        menuHolder.SetActive(true);
        settingsHolder.SetActive(false);
    }

    public void SliderVolume()
    {
        var volume = slider.value;
        audioSource.volume = volume;
        volumeNumber.text = (int)(volume * 100) + " %";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
