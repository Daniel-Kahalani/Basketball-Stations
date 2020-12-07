using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer audioMixer;

    public void Start()
    {
        if (gameObject.scene.name == "MainMenu")
        {
            Play("MainMenuMusic");
        }
        else
        {
            Play("GameMusic");
        }
        EventManager._instance.OnStartGame += () => Play("GameStart");
        EventManager._instance.OnHitBoardAction += () => Play("BasketBounce");
        EventManager._instance.OnHitFloorAction += () => Play("FloorBounce");
        EventManager._instance.OnScore += () => Play("Score");
    }

    public void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.outputAudioMixerGroup = sound.output;
        }
    }

    public void Play(string name)
    {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);
        if (soundToPlay == null)
        {
            return;
        }
        soundToPlay.source.Play();
    }

    public void Pause(string name)
    {
        Sound soundToPause = Array.Find(sounds, sound => sound.name == name);
        if (soundToPause == null)
        {
            return;
        }
        soundToPause.source.Pause();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat("soundEffectsVolume", volume);
    }
}