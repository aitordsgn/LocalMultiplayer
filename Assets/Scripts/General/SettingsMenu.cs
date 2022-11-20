using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer Volumen, Musica;
    public void SetVolume(float volume)
    {
        Volumen.SetFloat("Volumen", volume * volume);
    }
    public void SetMusic(float volume)
    {
        Musica.SetFloat("Music", volume * volume);
    }

    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }
}
