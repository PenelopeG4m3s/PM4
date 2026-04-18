using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Menu_Options : MonoBehaviour
{
    public AudioMixer mainAudioMixer;
    public Toggle mapOfTheDay;
    public Toggle multiplayer;
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public TMP_Text numberOfPlayersButtonText;

    void Start()
    {
        OnMainVolumeSliderChange();
        OnMusicVolumeSliderChange();
        OnSFXVolumeSliderChange();
    }

    public void OptionsMenu_Back()
    {
        GameManager.instance.ActivateMainMenuScreen();
    }

    public void OnMainVolumeSliderChange ()
    {
        // Start with the slider value 
        float newVolume = mainVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = (newVolume + 0.0f) * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("MainVolume", newVolume);

        // Set the value in the game manager
        GameManager.instance.masterVolume = newVolume;
    }

    public void OnMusicVolumeSliderChange ()
    {
        // Start with the slider value 
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = (newVolume + 0.0f) * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("MusicVolume", newVolume);

        // Set the value in the game manager
        GameManager.instance.musicVolume = newVolume;
    }

    public void OnSFXVolumeSliderChange ()
    {
        // Start with the slider value 
        float newVolume = sfxVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = (newVolume + 0.0f) * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("SFXVolume", newVolume);

        // Set the value in the game manager
        GameManager.instance.sfxVolume = newVolume;
    }
}