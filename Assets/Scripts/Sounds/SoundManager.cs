using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    
    public Sound[] sounds;

    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else { 
            Destroy(gameObject);
            return;             
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play (string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);
            
            sound.source.Play();
        }
        
    }

    public AudioSource FindSound (string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            return Array.Find(sounds, sound => sound.name == name).source;
        }

        return null;
    }
}
