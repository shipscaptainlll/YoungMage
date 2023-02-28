using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] AnimationCurve openingAnimationCurve;
    [SerializeField] AnimationCurve closingAnimationCurve;

    [Header("Audio Connection")]
    [SerializeField] SoundManager soundManager;
    AudioSource doorOpeningSound;
    AudioSource doorClosingSound;
    AudioSource doorBumpingSound;

    Coroutine openingCoroutine;
    Vector3 startRotation;

    bool doorOpened;
    bool bumpSoundPlayed;
    public bool DoorOpened { get { return doorOpened; } }

    void Start()
    {
        
        doorOpeningSound = soundManager.LocateAudioSource("DoorOpening", transform);
        doorClosingSound = soundManager.LocateAudioSource("DoorClosing", transform);
        doorBumpingSound = soundManager.LocateAudioSource("DoorBump", transform);
        startRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OpenTheDoor();
        }
    }

    public void OpenTheDoor()
    {
        if (!doorOpened)
        {
            doorOpened = true;
            doorOpeningSound.Play();
            openingCoroutine = StartCoroutine(OpeningCoroutine(2));
        }
    }

    IEnumerator OpeningCoroutine(float delay)
    {
        float elapsed = 0;
        float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentRotation = Mathf.Lerp(startRotation.z, startRotation.z - 120, openingAnimationCurve.Evaluate(elapsed / delay));
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, currentRotation));
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z - 120));
        openingCoroutine = null;
        yield return null;
    }

    public void UploadDoorState(bool doorUploadedOpened)
    {
        doorOpened = doorUploadedOpened;
        if (doorUploadedOpened)
        {
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z - 120));
        } else
        {
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z));
        }
    }
}
