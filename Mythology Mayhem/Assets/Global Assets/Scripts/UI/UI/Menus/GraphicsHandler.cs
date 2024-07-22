using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicsHandler : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    FullScreenMode fsm = FullScreenMode.FullScreenWindow;

    // Start is called before the first frame update
    void Start() {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        //resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;
        for(int i = 0; i < resolutions.Length; i++) {
            if(resolutions[i].refreshRate == currentRefreshRate) {
                filteredResolutions.Add(resolutions[i]);
                print(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for(int i = 0; i < filteredResolutions.Count; i++) {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;// + " " + filteredResolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);

            if(filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();
    }

    public void GraphicsDropdown() {
        /* Graphics Settings and Indices:
            Very Low    0
            Low         1
            Medium      2
            High        3
            Very High   4
            Ultra       5
        */
        switch(graphicsDropdown.value)
        {
            case 0: QualitySettings.SetQualityLevel(1); break; //Low
            case 1: QualitySettings.SetQualityLevel(2); break; //Medium
            case 2: QualitySettings.SetQualityLevel(3); break; //High
            default: break;
        }
    }

    public void FullScreenToggle() {
        if(Screen.fullScreenMode == FullScreenMode.Windowed) {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            fsm = FullScreenMode.FullScreenWindow;
            SetResolution(filteredResolutions.Count - 1);
        } else {
            Screen.fullScreenMode = FullScreenMode.Windowed;            
            fsm = FullScreenMode.Windowed;
        }
    }

    public void ResolutionDropdown() {
        SetResolution(resolutionDropdown.value);
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fsm);
    }
}
