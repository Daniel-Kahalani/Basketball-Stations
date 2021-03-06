﻿using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;
    public bool loop;
    [Range(0f, 1f)]
    public float spatialBlend;
    public AudioSource source;
    public AudioMixerGroup output;
}
