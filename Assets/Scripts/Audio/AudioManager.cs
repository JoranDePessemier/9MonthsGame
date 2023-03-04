using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] _sounds;

    [SerializeField]
    private float _fastFadeOutSpeed;

    [SerializeField]
    private float _fastFadeInSpeed;

    [SerializeField]
    private float _slowFadeOutSpeed;

    [SerializeField]
    private float _clockFadeVolume = 0.337f;

    [SerializeField]
    private float _clockFadeSpeed = 0.1f;

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
        Play("Chord1");
        Play("Chord2");
        Play("Chord3");
        Play("Chord4");
        Play("Chord5");
        Play("Rain");


        FadeOut("Rain", 1000f);
        FadeOut("Chord1", 1000f);
        FadeOut("Chord2", 1000f);
        FadeOut("Chord3", 1000f);
        FadeOut("Chord4", 1000f);
        FadeOut("Chord5", 1000f);
        FadeOut("Bass",1000f);
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

    public void Play(string name) => Play(name, 0);

    public void Play(string name, float pitchVariation)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);

        float pitch = UnityEngine.Random.Range(sound.Pitch - pitchVariation, sound.Pitch + pitchVariation);
        sound.source.pitch = pitch;

        sound.source.Play();
    }

    public void Play(string[] names, float PitchVariation)
    {
        string name = names[UnityEngine.Random.Range(0, names.Length)];
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);


        float pitch = UnityEngine.Random.Range(sound.Pitch - PitchVariation, sound.Pitch + PitchVariation);
        sound.source.pitch = pitch;

        sound.source.Play();
    }

    public void Play(string[] names) => Play(names, 0);

    public void Stop(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        sound.source.Stop();
    }

    public void FastFadeOut(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        StartCoroutine(FadeOutMusic(sound, _fastFadeOutSpeed));
    }

    public void FastFadeIn(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        StartCoroutine(FadeInMusic(sound, _fastFadeInSpeed));
    }

    public void SlowFadeOut(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        StartCoroutine(FadeOutMusic(sound, _slowFadeOutSpeed));
    }

    public void SetMoreSilentForClock(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.Name == name);
        StartCoroutine(FadeToVolume(sound, _clockFadeSpeed,_clockFadeVolume));
    }

    private IEnumerator FadeToVolume(Sound song, float fadeSpeed,float volume)
    {
        while (song.source.volume != volume)
        {
            song.source.volume = Mathf.MoveTowards(song.source.volume, volume, fadeSpeed * Time.deltaTime);
            yield return 0;
        }
        song.source.volume = volume;
    }
}
