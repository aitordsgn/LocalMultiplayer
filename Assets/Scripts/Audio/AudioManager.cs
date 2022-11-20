using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public C_Audio[] AudioArray;
    public static AudioManager instance;
    public AudioSource Musica;
    bool keepfadingIn, keepfadingOut;
    // Start is called before the first frame update
    void Awake()
    {

        if(instance == null )
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        foreach (C_Audio s in AudioArray)
        {
            if(!s.BGM)
            {
                gameObject.AddComponent<AudioSource>();
                s.source = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                s.source = Musica;
            }
            
            s.source.clip = s.AudioClip;
            s.source.volume = s.Volumen;
            s.source.pitch = s.Pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup =s.AudioMixer ;
        }
        
    }

    public void Play( string name)
    {
        C_Audio s = Array.Find(AudioArray, sound => sound.Nombre == name);
        if (s == null)
        {
            Debug.LogWarning("Sonido " + name + " no encontrado!");
            return;
        }
        s.source.Play();
    }
    public void PlayMusic(string name)
    {
        //CallFadeOut(1f);
        Musica.Stop();
        C_Audio s = Array.Find(AudioArray, sound => sound.Nombre == name);
        if (s == null)
        {
            Debug.LogWarning("Sonido " + name + " no encontrado!");
            return;
        }
        Musica.clip = s.AudioClip;
        s.source.Play();
        //CallFadeIn(1f);
    }
    /*public void CallFadeIn(float speed)
    {
        StartCoroutine(FadeIn(speed));
    }
    public void CallFadeOut(float speed)
    {
        StartCoroutine(FadeOut(speed));
    }


    IEnumerator FadeIn (float speed)
    {
        keepfadingIn = true;
        keepfadingOut = false;
        float maxVolume = Musica.volume;
        Musica.volume = 0;
        while (Musica.volume <= maxVolume && keepfadingIn)
        {
            Musica.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOut(float speed)
    {
        keepfadingIn = true;
        keepfadingOut = false;

        while (Musica.volume >= speed && keepfadingIn)
        {
            Musica.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}
