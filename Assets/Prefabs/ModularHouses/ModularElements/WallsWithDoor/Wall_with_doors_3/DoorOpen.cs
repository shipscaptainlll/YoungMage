using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] AnimationCurve openingAnimationCurve;
    Coroutine openingCoroutine;
    Vector3 startRotation;

    void Start()
    {
        startRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TryOpenDoor();
        }
    }

    public void TryOpenDoor()
    {
        if (openingCoroutine != null)
        {
            StopCoroutine(openingCoroutine);
        }
        openingCoroutine = StartCoroutine(OpeningCoroutine(1));
    }

    IEnumerator OpeningCoroutine(float delay)
    {
        float elapsed = 0;
        float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentRotation = Mathf.Lerp(startRotation.z, startRotation.z - 3, openingAnimationCurve.Evaluate(elapsed / delay));
            transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, currentRotation));
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, startRotation.z));
        openingCoroutine = null;
        yield return null;
    }
}
