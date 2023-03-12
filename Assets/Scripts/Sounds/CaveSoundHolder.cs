using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSoundHolder : MonoBehaviour
{
    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource caveBulpFirstSound;
    AudioSource caveBulpSecondSound;

    System.Random rand;

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        caveBulpFirstSound = soundManager.LocateAudioSource("CaveEnvironmentBulpFirst", transform);
        caveBulpSecondSound = soundManager.LocateAudioSource("CaveEnvironmentBulpSecond", transform);
    }

    public void PlaySound()
    {
        //Debug.Log("Here: ");
        int randomNumber = rand.Next(1, 10);
        if (randomNumber > 5)
        {
            caveBulpFirstSound.Play();
        } else
        {
            caveBulpSecondSound.Play();
        }
    }
}
