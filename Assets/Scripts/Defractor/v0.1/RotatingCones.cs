using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCones : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] CuttingProcess cuttingProcess;
    [SerializeField] int direction;
    [SerializeField] float requiredSpeed;
    [SerializeField] float updateSpeed;
    float currentSpeed = 0;
    bool rotating = false;
    bool slowingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        cuttingProcess.RotationStarted += StartRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartRotation()
    {
        rotating = true;
        slowingDown = false;
        StopAllCoroutines();
        StartCoroutine(RotateCoroutine());
        StartCoroutine(Delay());
    }

    void StopRotation()
    {
        
        if (!slowingDown)
        {
            slowingDown = true;
            rotating = false;
            StopAllCoroutines();
            StartCoroutine(StopRotationCoroutine());
        }
        
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        StopRotation();
    }

    IEnumerator RotateCoroutine()
    {
        
        float elapsed = 0;
        var done = false;
        while (true)
        {
            yield return null;
            if (!done && currentSpeed < requiredSpeed)
            {
                elapsed += Time.deltaTime;
                currentSpeed += Time.deltaTime * 100;
            } else if (!done && currentSpeed >= requiredSpeed) { done = true; currentSpeed = requiredSpeed; }
            transform.Rotate(0, 0, currentSpeed * direction * Time.deltaTime);
        }
    }

    IEnumerator StopRotationCoroutine()
    {
        slowingDown = true;
        float elapsed = 0;
        
        var done = false;
        while (true)
        {
            yield return null;
            if (!done && currentSpeed > 0)
            {
                elapsed += Time.deltaTime;
                currentSpeed -= Time.deltaTime * 100;
                //Debug.Log(currentSpeed);
            }
            else if (!done && currentSpeed <= requiredSpeed) { done = true; currentSpeed = 0; slowingDown = false; StopAllCoroutines(); }
            transform.Rotate(0, 0, currentSpeed * direction * Time.deltaTime);
        }
    }
}
