using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    /* Once game starts, obtain list of possible resolutions based on computer model.
       Then create this new list and add it as the dropdown options. Additionally, 
       updates resolution choice to current computer's best suitable resolution.
    */
    void Start() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if (!options.Contains(option)) {
                options.Add(option);
            }

            // comparison to update the resolution at the start
            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        audioMixer.SetFloat("volume", -5f);

    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume) {
        float aftLogConversion = Mathf.Log10(volume) * 20;

        audioMixer.SetFloat("volume", aftLogConversion);
        GameMgrSingleton.GM.currVol = aftLogConversion;
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }
}
