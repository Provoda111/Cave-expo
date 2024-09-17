using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public TMP_Text SetgUpperMainText;
    public GameObject VideoSetgButton;
    public GameObject VideoPanel;
    public GameObject AudioSetgButton;
    public GameObject AudioPanel;
    public GameObject GameSetgButton;
    public GameObject GamePanel;

    /// <summary>
    /// Below this summary will be all variables, that are responsible for Video Settings Quality of this game.
    /// </summary>
    Resolution[] resolutions; // List of available resolutions
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Dropdown textureDropdown;
    public Dropdown aaDropdown;

    /// <summary>
    /// Below this summary will be all variables, that are responsible for Audio Settings of this game.
    /// </summary>
    //public AudioMixer audioMixer;
    //public Slider volumeSlider;
    //float currentVolume;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TurnOnMenu();
        }
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,
            resolution.height, Screen.fullScreen);
    }
    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        qualityDropdown.value = 6;
    }
    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        qualityDropdown.value = 6;
    }
    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            switch (qualityIndex)
            {
                case 0:
                    textureDropdown.value = 3;
                    aaDropdown.value = 0;
                    break;
                case 1:
                    textureDropdown.value = 2;
                    aaDropdown.value = 0;
                    break;
                case 2:
                    textureDropdown.value = 1;
                    aaDropdown.value = 0;
                    break;
                case 3:
                    textureDropdown.value = 0;
                    aaDropdown.value = 0;
                    break;
                case 4:
                    textureDropdown.value = 0;
                    aaDropdown.value = 1;
                    break;
                case 5:
                    textureDropdown.value = 0;
                    aaDropdown.value = 2;
                    break;
            }
        }
        qualityDropdown.value = qualityIndex;
    }
    void OpenVideoSettings()
    {
        VideoSetgButton.SetActive(false);
        GameSetgButton.SetActive(false);
    }
    void OpenGameSettings()
    {
        VideoSetgButton.SetActive(false);
        GameSetgButton.SetActive(false);
    }
    void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("TextureQualityPreference", textureDropdown.value);
        PlayerPrefs.SetInt("AntiAliasingPreference", aaDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference", Convert.ToInt32(Screen.fullScreen));
    }
    void TurnOnMenu()
    {

    }
    void TurnOffMenu()
    {

    }
}
