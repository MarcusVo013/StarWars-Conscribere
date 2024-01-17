using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    [Header("____Audio___")]
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider voiceSlider;

    [Header("____Resolution___")]
    public Resolution[] resolution;
    public TMPro.TMP_Dropdown resolutionDropdown;
    int currentResolutionIndex = 0;


    private void Awake()
    {
        LoadSettings();
        MasterVolume();
        MusicVolume();
        SoundVolume();
        VoiceVolume();

        resolution = Screen.resolutions;
    }

    public void SetResolution(int resolutionindex)
    {
        Resolution resolutions = resolution[resolutionindex];
        Screen.SetResolution(resolutions.width, resolutions.height, Screen.fullScreen);
    }
    public void MasterVolume()
    {      
        float volume = masterSlider.value;
        audioMixer.SetFloat("master",Mathf.Log10(volume)*20);
    }
    public void MusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
    public void SoundVolume()
    {
        float volume = soundSlider.value;
        audioMixer.SetFloat("sound", Mathf.Log10(volume) * 20);
    }
    public void VoiceVolume()
    {
        float volume = voiceSlider.value;
        audioMixer.SetFloat("voice", Mathf.Log10(volume) * 20);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool Isfullscreen)
    {
        Screen.fullScreen = Isfullscreen;
    }
    public void SetScreenResolutions()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + "X" + resolution[i].height;
            options.Add(option);
            if (resolution[i].width == Screen.currentResolution.width && resolution[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("voiceVolume", voiceSlider.value);
        PlayerPrefs.SetInt("resolutionIndex", currentResolutionIndex);
        PlayerPrefs.SetInt("qualityIndex", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("fullScreen", Screen.fullScreen ? 1 : 0);
    }
    
    public void LoadSettings()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", 1);
        voiceSlider.value = PlayerPrefs.GetFloat("voiceVolume", 1);
        currentResolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityIndex", 0));
        Screen.fullScreen = PlayerPrefs.GetInt("fullScreen", 0) == 1;
    }
}
