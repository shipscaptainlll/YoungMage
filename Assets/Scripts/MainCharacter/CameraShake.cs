using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeMagnitude;
    [SerializeField] float aftershakeDuration;
    Vector3 originalPos;
    Coroutine cameraShakingCoroutine;
    Coroutine cameraAftershakingCoroutine;
    bool activated;

    public bool Activated { 
        get
        {
            return activated;
        } 
        set { 
            activated = value;
            if (activated) { InitiateCameraShaking(); }
            else { StopCameraShaking(); }
        } 
    }

    public Coroutine CameraShakingCoroutine { get { return cameraShakingCoroutine; } }
    public Coroutine CameraAftershakingCoroutine { get { return cameraAftershakingCoroutine; } }

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    IEnumerator Shake (float magnitude)
    {
        //Debug.Log(transform.localPosition);
        while (true)
        {
            
            float x = Random.Range(-1, 1) * magnitude / 100;
            float y = Random.Range(-1, 1) * magnitude / 100;

            transform.localPosition = new Vector3(x, originalPos.y + y, originalPos.z);
            //Debug.Log(transform.localPosition);
            yield return null;
        }
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        //Debug.Log("started");
        float elapsed = 0;
        float currentMagnitude;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMagnitude = Mathf.Lerp(magnitude, 0, elapsed / duration);
            float x = Random.Range(-1, 1) * currentMagnitude / 100;
            float y = Random.Range(-1, 1) * currentMagnitude / 100;
            transform.localPosition = new Vector3(x, originalPos.y + y, originalPos.z);
            yield return null;
        }
        if (cameraShakingCoroutine != null) { cameraShakingCoroutine = null; }
        if (cameraAftershakingCoroutine != null) { cameraAftershakingCoroutine = null; }
        transform.localPosition = originalPos;
        yield return null;
    }

    void PlaceCameraOriginal()
    {
        transform.localPosition = originalPos;
    }

    void InitiateCameraShaking()
    {
        StopCameraShaking();
        StopCameraAftershaking();
        cameraShakingCoroutine = StartCoroutine(Shake(shakeMagnitude));
    }

    void StopCameraShaking()
    {
        if (cameraShakingCoroutine != null) { StopCoroutine(cameraShakingCoroutine); cameraShakingCoroutine = null; InitiateCameraAftershaking(); }
    }

    void InitiateCameraAftershaking()
    {
        cameraAftershakingCoroutine = StartCoroutine(Shake(aftershakeDuration, shakeMagnitude));
    }

    void StopCameraAftershaking()
    {
        if (cameraAftershakingCoroutine != null) { StopCoroutine(cameraAftershakingCoroutine); cameraAftershakingCoroutine = null; }
    }

    public void ShakeCamera(float duration)
    {
        Debug.Log("Hello there1");
        StopCameraShaking();
        Debug.Log("Hello there2");
        StopCameraAftershaking();
        Debug.Log("Hello there3");
        cameraShakingCoroutine = StartCoroutine(Shake(duration, shakeMagnitude));
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        StopCameraShaking();
        StopCameraAftershaking();
        cameraShakingCoroutine = StartCoroutine(Shake(duration, magnitude));
    }
}
