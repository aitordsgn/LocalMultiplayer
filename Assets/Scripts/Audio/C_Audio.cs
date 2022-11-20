using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class C_Audio
{

    public string Nombre;
    public AudioClip AudioClip;
    public AudioMixerGroup AudioMixer;
    [Range(0f,1f)]
    public float Volumen;
    [Range(1f, 3f)]
    public float Pitch;
    [HideInInspector]
    public AudioSource source;

    public bool loop;
    public bool BGM;
}
