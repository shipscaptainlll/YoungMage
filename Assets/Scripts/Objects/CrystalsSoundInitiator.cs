using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsSoundInitiator : MonoBehaviour
{
    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] float soundVolume;
    AudioSource crystalsSound;

    // Start is called before the first frame update
    void Start()
    {
        crystalsSound = soundManager.LocateAudioSource("CrystalWorking", transform);
        crystalsSound.volume = soundVolume;
        crystalsSound.Play();
    }
}
