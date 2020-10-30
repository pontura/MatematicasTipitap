using UnityEngine;
using System.Collections;

public class VoicesManager : MonoBehaviour
{
    public AudioClip[] timeNoPlaying;
    public AudioClip[] felicitaciones;
    public AudioSource audioSource;
    public float volume;

    public void Awake()
    {
        volume = PlayerPrefs.GetFloat("SFXVol", 1);
        OnSoundsVolumeChanged(volume);
        Events.OnVoiceSay += OnVoiceSay;
        Events.OnVoiceSayFromList += OnVoiceSayFromList;
    }
    void OnDestroy()
    {
        Events.OnVoiceSay -= OnVoiceSay;
        Events.OnVoiceSayFromList -= OnVoiceSayFromList;
    }
    void OnSoundsVolumeChanged(float value)
    {
        audioSource.volume = value;
        volume = value;

        if (value == 0 || value == 1)
            PlayerPrefs.SetFloat("SFXVol", value);
    }
    AudioClip sayInDelay = null;
    void OnVoiceSayFromList(string listName, float delay)
    {
       // print("____________ OnVoiceSayFromList " + listName);
        if (listName == "")
            audioSource.Stop();
        else
        {
            AudioClip[] arr = null;
            switch (listName)
            {
                case "felicitaciones": arr = felicitaciones; break;
                case "timeNoPlaying": arr = timeNoPlaying; break;
            }
            if (arr != null)
            {
                sayInDelay = arr[Random.Range(0, arr.Length)];
                Invoke("SayInDelay", delay);
            }
        }
    }
    void SayInDelay()
    {
        audioSource.PlayOneShot(sayInDelay);
    }
    void OnVoiceSay(string soundName)
    {
        string audioName = soundName;
        switch (audioName.ToLower())
        {
            case "palabras/montaña":
                audioName = "palabras/montana"; break;
            case "palabras/niño":
                audioName = "palabras/nino"; break;
            case "palabras/piña":
                audioName = "palabras/pina"; break;

            case "palabras/laniñaalegre":
                audioName = "palabras/laninaalegre"; break;
            case "palabras/laniñaalegrebeep":
                audioName = "palabras/laninaalegreBEEP"; break;

            case "palabras/lamochilapequeña":
                audioName = "palabras/lamochilapequena"; break;
            case "palabras/lamochilapequeñabeep":
                audioName = "palabras/lamochilapequenaBEEP"; break;

        }

        if (audioName == "")
            audioSource.Stop();
        else
            audioSource.PlayOneShot(Resources.Load("SFX/" + audioName) as AudioClip);
    }
}
