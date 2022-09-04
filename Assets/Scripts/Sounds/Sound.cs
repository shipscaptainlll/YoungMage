using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float spatialBlend;

    public AudioRolloffMode audioRolloffMode;

    public AudioSourceCurveType audioSourceCurveType;

    public AnimationCurve audioSourceAnimationCurve;

    public float minDistance;

    public float maxDistance;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
