using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsControlsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new();

        int currentResolutionIndex = 0;
        for(var i = 0;i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width} x {resolutions[i].height}");
            if (resolutions[i].width==Screen.currentResolution.width &&
                resolutions[i].height==Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
    public void SetResolution(int resolutionIndex)
    {
        var resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
