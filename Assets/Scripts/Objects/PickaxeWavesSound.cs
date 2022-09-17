using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeWavesSound : MonoBehaviour
{
    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource pickaxeSound;

    // Start is called before the first frame update
    void Start()
    {
        pickaxeSound = soundManager.LocateAudioSource("PickaxeWaves", transform);
        pickaxeSound.Play();
    }
}
