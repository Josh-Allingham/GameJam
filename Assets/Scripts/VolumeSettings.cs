using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public float masterVolume; 
    public float musicVolume;
    public float sfxVolume;

    [SerializeField]
    private AudioMixer audioMixer;

    public void SetVolume(float master, float music, float sfx)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(master)*20);
        audioMixer.SetFloat("MusicVol", Mathf.Log10(music) * 20);
        audioMixer.SetFloat("SFXVol", Mathf.Log10(sfx) * 20);
    }

}
