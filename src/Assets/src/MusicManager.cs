using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public float volumeReal;
    public float volume;
    private string lastSong;

    public void Awake()
    {
        this.volume = volumeReal;
       // volume = PlayerPrefs.GetFloat("SFXVol", 0.2f);

        OnVolumeChanged(volume);
        Events.OnMusic += OnMusic;
    }
    void OnDestroy()
    {
        Events.OnMusic -= OnMusic;
    }
    public void OnVolumeChanged(float value)
    {
        audioSource.volume = value;
        volume = value;

       // PlayerPrefs.SetFloat("musicVolume", value);
    }
    void OnMusic(string soundName)
    {
        if (lastSong == soundName) return;
        lastSong = soundName;
        if (soundName == "")
            audioSource.Stop();
        else
        {
            AudioClip clip = (Resources.Load("music/" + soundName) as AudioClip);
            audioSource.clip = clip;
            audioSource.Play();
        }

        if (audioSource.volume!= 0)
            audioSource.volume = 0.1f;
    }
    public void SwitchMusic()
    {
        if (volume > 0)
            OnVolumeChanged(0);
        else
            OnVolumeChanged(volumeReal);
    }
    
}
