using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioSettings : MonoBehaviour {
    FMOD.Studio.Bus _music;
    FMOD.Studio.Bus _sfx;
    FMOD.Studio.Bus _master;
    float _musicVolume = 1f;
    float _sfxVolume = 1f;
    float _masterVolume = 1f;

    void Awake ()
    {
        _music = FMODUnity.RuntimeManager.GetBus ("bus:/MasterGrp/MusicGrp");
        _sfx = FMODUnity.RuntimeManager.GetBus ("bus:/MasterGrp/SFXGrp");
        _master = FMODUnity.RuntimeManager.GetBus ("bus:/MasterGrp");
    }

    void Update () 
    {

        _music.setVolume (_musicVolume);
        _sfx.setVolume (_sfxVolume);
        _master.setVolume (_masterVolume);
    }

    public void MasterVolumeLevel (Slider slider)
    {
        _masterVolume = slider.value;
    }

    public void MusicVolumeLevel (Slider slider)
    {
        _musicVolume = slider.value;
    }

    public void SFXVolumeLevel (Slider slider)
    {
        _sfxVolume = slider.value;
    }
}
