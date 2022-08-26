using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoor : MonoBehaviour
{
    [SerializeField] AnimationCurve openingAnimationCurve;
    [SerializeField] AnimationCurve closingAnimationCurve;
    Coroutine openingCoroutine;
    Coroutine closingCoroutine;
    Vector3 startRotation;

    bool playerInInitiator;
    bool doorOpened;

    public bool PlayerInInitiator { get { return playerInInitiator; } set { playerInInitiator = value; } }

    void Start()
    {
        startRotation = transform.rotation.eulerAngles;
        StartCoroutine(RandomlyOpenDoor());
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
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, currentRotation));
            yield return null;
        }
        transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z));
        closingCoroutine = null;
        doorOpened = false;
        yield return null;
    }
}
