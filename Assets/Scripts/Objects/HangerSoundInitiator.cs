using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerSoundInitiator : MonoBehaviour
{
    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] float soundVolume;
    AudioSource clothingSound;

    // Start is called before the first frame update
    void Start()
    {
        clothingSound = soundManager.LocateAudioSource("BedJump", transform);
        clothingSound.volume = soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        clothingSound.Play();
    }
}
