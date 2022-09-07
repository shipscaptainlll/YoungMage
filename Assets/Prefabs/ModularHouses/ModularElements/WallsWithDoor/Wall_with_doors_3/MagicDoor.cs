using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoor : MonoBehaviour
{
    [SerializeField] AnimationCurve openingAnimationCurve;
    [SerializeField] AnimationCurve closingAnimationCurve;

    [Header("Audio Connection")]
    [SerializeField] SoundManager soundManager;
    AudioSource doorOpeningSound;
    AudioSource doorClosingSound;
    AudioSource doorBumpingSound;

    Coroutine openingCoroutine;
    Coroutine closingCoroutine;
    Vector3 startRotation;

    bool playerInInitiator;
    bool doorOpened;
    bool bumpSoundPlayed;
    public bool PlayerInInitiator { get { return playerInInitiator; } set { playerInInitiator = value; } }

    void Start()
    {
        
        doorOpeningSound = soundManager.LocateAudioSource("DoorOpening", transform);
        doorClosingSound = soundManager.LocateAudioSource("DoorClosing", transform);
        doorBumpingSound = soundManager.LocateAudioSource("DoorBump", transform);
        startRotation = transform.rotation.eulerAngles;
        StartCoroutine(RandomlyOpenDoor());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            doorOpeningSound.Play();
        }
    }

    IEnumerator RandomlyOpenDoor()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            OpenTheDoor();
        }
    }

    void OpenTheDoor()
    {
        if (!playerInInitiator && !doorOpened)
        {
            doorOpened = true;
            if (openingCoroutine != null)
            {
                StopCoroutine(openingCoroutine);
            }
            doorOpeningSound.Play();
            openingCoroutine = StartCoroutine(OpeningCoroutine(5));
        }
    }

    public void CloseTheDoor()
    {
        if (doorOpened)
        {
            if (closingCoroutine != null)
            {
                StopCoroutine(closingCoroutine);
            }
            if (openingCoroutine != null)
            {
                StopCoroutine(openingCoroutine);
            }
            doorClosingSound.Play();
            closingCoroutine = StartCoroutine(ClosingCoroutine());
        }
    }

    IEnumerator OpeningCoroutine(float delay)
    {
        float elapsed = 0;
        float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentRotation = Mathf.Lerp(startRotation.z, startRotation.z - 60, openingAnimationCurve.Evaluate(elapsed / delay));
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, currentRotation));
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z - 60));
        openingCoroutine = null;
        yield return null;
    }

    IEnumerator ClosingCoroutine()
    {
        float elapsed = 0;
        float delay = 1;
        float zStartRotation = transform.rotation.eulerAngles.z;
        float currentRotation;
        //float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentRotation = Mathf.Lerp(zStartRotation, startRotation.z, closingAnimationCurve.Evaluate(elapsed / delay));
            if (!bumpSoundPlayed && elapsed > 0.4f)
            {
                doorBumpingSound.Play();
                bumpSoundPlayed = true;
            }
            
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, currentRotation));
            yield return null;
        }
        transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z));

        bumpSoundPlayed = false;
        closingCoroutine = null;
        doorOpened = false;
        yield return null;
    }
}
