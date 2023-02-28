using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] _sounds;

    private void Awake()
    {
        foreach(Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.Clip;
            sound.source.volume = sound.Volume;
            sound.source.pitch = sound.Pitch;
            sound.source.loop = sound.Loop;
        }
    }

    private void Start()
    {
        Play("Clock");
        Play("Bass");
        Play("GuitarMelody");
        Play("PianoChords");
        Play("PianoMelody");
        Play("Violin");

        FadeOut("Violin", 1000f);
        FadeOut("Clock", 1000f);
        FadeOut("PianoChords", 1000f);
        FadeOut("PianoMelody", 1000f);
        FadeOut("GuitarMelody", 1000f);
    }

    public void FadeOut(string name,float fadeSpeed)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        StartCoroutine(FadeOutMusic(sound, fadeSpeed));
    }

    IEnumerator FadeOutMusic(Sound song, float fadeSpeed)
    {
        while(song.source.volume > 0f)
        {
            song.source.volume = Mathf.MoveTowards(song.source.volume, 0, fadeSpeed * Time.deltaTime);
            yield return 0;
        }
        
    }

    public void FadeIn(string name, float fadeSpeed)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        StartCoroutine(FadeInMusic(sound, fadeSpeed));
    }

    private IEnumerator FadeInMusic(Sound song, float fadeSpeed)
    {
        while (song.source.volume < song.Volume)
        {
            song.source.volume = Mathf.MoveTowards(song.source.volume, song.Volume, fadeSpeed * Time.deltaTime);
            yield return 0;
        }
        song.source.volume = song.Volume;
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        sound.source.Play();
    }
}
