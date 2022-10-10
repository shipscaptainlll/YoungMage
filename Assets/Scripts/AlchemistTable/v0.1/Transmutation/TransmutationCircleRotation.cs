using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationCircleRotation : MonoBehaviour
{
    [SerializeField] ParticleSystem transmutationCirclePS;

    Coroutine rotationCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CircleLookRotation()
    {
        if (rotationCoroutine != null) { StopCoroutine(rotationCoroutine); }
        rotationCoroutine = StartCoroutine(RotateCircleLook());
    }

    public void CircleChooseRotation()
    {
        if (rotationCoroutine != null) { StopCoroutine(rotationCoroutine); }
        rotationCoroutine = StartCoroutine(RotateCircleChoose());
    }

    public void CircleChoosenRotation()
    {
        if (rotationCoroutine != null) { StopCoroutine(rotationCoroutine); }
        rotationCoroutine = StartCoroutine(RotateCircleChoosen());
    }

    public void CircleDefaultRotation()
    {
        if (rotationCoroutine != null) { StopCoroutine(rotationCoroutine); }
        rotationCoroutine = StartCoroutine(RotateCircleDefault());
    }


    IEnumerator RotateCircleLook()
    {
        //Debug.Log("There1");
        var rotation = transmutationCirclePS.rotationOverLifetime;
        rotation.z = 18;
        yield return new WaitForSeconds(0.19f);
        rotation.z = 7;
        yield return new WaitForSeconds(0.115f);
        rotation.z = -1.2f;
    }

    IEnumerator RotateCircleChoose()
    {
        var rotation = transmutationCirclePS.rotationOverLifetime;
        rotation.z = -15;
        yield return null;
    }

    IEnumerator RotateCircleChoosen()
    {
        var rotation = transmutationCirclePS.rotationOverLifetime;
        rotation.z = 20;
        yield return new WaitForSeconds(0.1f);
        rotation.z = 0;
        yield return new WaitForSeconds(0.3f);
        rotation.z = -20;
        yield return new WaitForSeconds(0.1f);
        rotation.z = 0;
        yield return new WaitForSeconds(0.3f);
        rotation.z = 20;
        yield return new WaitForSeconds(0.1f);
        rotation.z = 0.21f;
    }

    IEnumerator RotateCircleDefault()
    {
        //Debug.Log("There");
        var rotation = transmutationCirclePS.rotationOverLifetime;
        rotation.z = 0.21f;
        yield return null;
    }
}
