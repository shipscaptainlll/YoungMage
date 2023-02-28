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

    float coroutineStage;

    bool playerInInitiator;
    bool doorOpened;
    bool bumpSoundPlayed;

    public float CoroutineStage { get { return coroutineStage; } }
    public bool PlayerInInitiator { get { return playerInInitiator; } set { playerInInitiator = value; } }
    public bool DoorOpened { get { return doorOpened; } }
    public Coroutine OpeningCoroutineGetter { get { return openingCoroutine; } }
    public Coroutine ClosingCoroutineGetter { get { return closingCoroutine; } }

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

    public void UploadDoorOpening(float uploadedElapsed)
    {
        StopAllCoroutines();
        openingCoroutine = null;
        closingCoroutine = null;
        coroutineStage = 0;
        doorOpened = true;
        doorOpeningSound.Play();
        openingCoroutine = StartCoroutine(OpeningCoroutine(5, uploadedElapsed));
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

    public void UploadDoorClosing(float uploadedElapsed)
    {
        StopAllCoroutines();
        openingCoroutine = null;
        closingCoroutine = null;
        coroutineStage = 0;
        doorClosingSound.Play();
        closingCoroutine = StartCoroutine(ClosingCoroutine(uploadedElapsed));
    }

    public void UploadDoorState(bool doorUploadedOpened)
    {
        StopAllCoroutines();
        openingCoroutine = null;
        closingCoroutine = null;
        coroutineStage = 0;
        doorOpened = doorUploadedOpened;
        if (doorUploadedOpened)
        {
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z - 60));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z));
        }
    }

    IEnumerator OpeningCoroutine(float delay)
    {
        float elapsed = 0;
        float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            coroutineStage = elapsed;
            currentRotation = Mathf.Lerp(startRotation.z, startRotation.z - 60, openingAnimationCurve.Evaluate(elapsed / delay));
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, currentRotation));
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z - 60));
        openingCoroutine = null;
        yield return null;
    }

    IEnumerator OpeningCoroutine(float delay, float uploadedElapsed)
    {
        float elapsed = uploadedElapsed;
        float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            coroutineStage = elapsed;
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
            coroutineStage = elapsed;
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

    IEnumerator ClosingCoroutine(float uploadedElapsed)
    {
        float elapsed = uploadedElapsed;
        float delay = 1;
        float zStartRotation = transform.rotation.eulerAngles.z;
        float currentRotation;
        //float currentRotation = 0;

        if (elapsed > 0.4f)
        {
            bumpSoundPlayed = true;
        }

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            coroutineStage = elapsed;
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
