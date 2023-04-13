using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer effectsMixer;

    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;
    [SerializeField] Toggle fullScreenCheck;
    [SerializeField] GameObject pauseMenu;

    private Resolution[] resolutions;

    private void Start()
    {
        fullScreenCheck.isOn = Screen.fullScreen;
        resolutions = Screen.resolutions;

        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        resolutionDropdown.ClearOptions();
        List<string> resolutionStrings = new List<string>();
        int resIndex = 0;
        for (int i = 0; i < resolutions.Length; ++i)
        {
            if (resolutions[i].height == Screen.height && resolutions[i].width == Screen.width)
            {
                resIndex = i;
            }
            resolutionStrings.Add(resolutions[i].width + " x " + resolutions[i].height);
        }
        resolutionDropdown.AddOptions(resolutionStrings);
        resolutionDropdown.value = resIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        effectsMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }
    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }

    public void BackToPauseMenu()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
