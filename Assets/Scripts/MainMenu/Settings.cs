using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider masterSlider, musicSlider, sfxSlider;

    void Start()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        masterSlider.value = masterVolume;
        SetMasterVolume(masterVolume);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        musicSlider.value = musicVolume;
        SetMusicVolume(musicVolume);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        sfxSlider.value = sfxVolume;
        SetSFXVolume(sfxVolume);
    }

    public void SetMasterVolume(float vol)
    {
        masterMixer.SetFloat("MasterVol", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("MasterVolume", vol);
    }

    public void SetMusicVolume(float vol)
    {
        masterMixer.SetFloat("MusicVol", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }

    public void SetSFXVolume(float vol)
    {
        masterMixer.SetFloat("SFXVol", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("SFXVolume", vol);
    }

    public void BackButton()
    {
        gameObject.SetActive(false);
    }
}
