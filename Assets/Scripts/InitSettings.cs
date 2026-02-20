using TMPro;
using UnityEngine;

public class InitSettings : VolumeSettings
{
    [SerializeField]
    public TMP_Text highScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalSettings.MasterVolume = 0.7f;
        GlobalSettings.MusicVolume = 0.7f;
        GlobalSettings.SFXVolume = 0.7f;
        GlobalSettings.ColFilter = 0;
        GlobalSettings.score = 0;
        SetVolume(0.7f, 0.7f, 0.7f);
    }

    void Update()
    {
        SetVolume(GlobalSettings.MasterVolume, GlobalSettings.MusicVolume, GlobalSettings.SFXVolume);
    }

    public void gameOver()
    {
        float score = GlobalSettings.score;
        highScore.text = "$ " + score.ToString();
    }

}
