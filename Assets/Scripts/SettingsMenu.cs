using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject previousMenu;

    [SerializeField]
    public GameObject settingsMenu;

    [SerializeField]
    public Slider masterVolSlider;
    [SerializeField]
    public Slider musicVolSlider;
    [SerializeField]
    public Slider sfxVolSlider;
    [SerializeField]
    public TMP_Dropdown colFilter;

    float currMasterVol;
    float currMusicVol;
    float currSFXVol;
    int currColFilter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currMasterVol = GlobalSettings.MasterVolume;
        currMusicVol = GlobalSettings.MusicVolume;
        currSFXVol = GlobalSettings.SFXVolume;
        currColFilter = GlobalSettings.ColFilter;

        masterVolSlider.value = currMasterVol;
        musicVolSlider.value = currMusicVol;
        sfxVolSlider.value = currSFXVol;
        colFilter.value = currColFilter;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void backToPreviousMenu()
    {
        settingsMenu.SetActive(false);
        previousMenu.SetActive(true);
    }

    public void saveSettings()
    {
         GlobalSettings.MasterVolume = masterVolSlider.value;
         GlobalSettings.MusicVolume = musicVolSlider.value;
         GlobalSettings.SFXVolume = sfxVolSlider.value;
         GlobalSettings.ColFilter = colFilter.value;
    }
}
