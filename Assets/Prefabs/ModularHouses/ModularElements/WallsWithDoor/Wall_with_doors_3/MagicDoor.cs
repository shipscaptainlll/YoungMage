using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoor : MonoBehaviour
{
    Coroutine openingCoroutine;
    Coroutine closingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.localRotation.x + " " + transform.localRotation.y + " " + transform.localRotation.z);
        transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z));
        Debug.Log(transform.localRotation.x + " " + transform.localRotation.y + " " + transform.localRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.localRotation.eulerAngles.x);
        Debug.Log(transform.localRotation.eulerAngles.y);
        Debug.Log(transform.localRotation.eulerAngles.z);
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (openingCoroutine != null)
            {
                StopCoroutine(openingCoroutine);
            }
            openingCoroutine = StartCoroutine(OpeningCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (closingCoroutine != null)
            {
                StopCoroutine(closingCoroutine);
            }
            closingCoroutine = StartCoroutine(ClosingCoroutine());
        }
    }

    IEnumerator OpeningCoroutine()
    {
        float elapsed = 0;
        float delay = 5;
        
        float xRotation = transform.rotation.x;
        float yRotation = transform.rotation.z;
        float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentRotation = Mathf.Lerp(298, 238, elapsed / delay);
            transform.rotation = Quaternion.Euler(new Vector3(xRotation, yRotation, currentRotation));
            yield return null;
        }
        transform.rotation = Quaternion.Euler(new Vector3(xRotation, yRotation, 238));
        openingCoroutine = null;
        yield return null;
    }

    IEnumerator ClosingCoroutine()
    {
        float elapsed = 0;
        float delay = 1;
        float startRotation = transform.localRotation.z;
        float currentRotation = 0;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentRotation = Mathf.Lerp(startRotation, -69, elapsed / delay);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -69));
        closingCoroutine = null;
        yield return null;
    }
}
