using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCones : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] ClickManager clickManager;
    [SerializeField] CuttingProcess cuttingProcess;
    [SerializeField] int direction;
    [SerializeField] float requiredSpeed;
    [SerializeField] float updateSpeed;
    [SerializeField] ParticleSystem rotationParticleSystem;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    float currentSpeed = 0;
    float elapsed;
    bool rotating = false; 
    bool slowingDown = false;
    bool circleShown;

    public bool Rotating { get { return rotating; } }
    public bool SlowingDown { get { return slowingDown; } }
    public float Elapsed { get { return elapsed; } }
    public bool CircleShown { get { return circleShown; } }

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationAppearSound;
    AudioSource knivesRotationSounds;
    AudioSource knivesStopSounds;

    // Start is called before the first frame update
    void Start()
    {
        cuttingProcess.RotationStarted += StartRotation;
        
        if (soundManager != null) { conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", rotationParticleSystem.transform); }
        if (soundManager != null) { knivesRotationSounds = soundManager.LocateAudioSource("DefractorRotation", transform);
            knivesStopSounds = soundManager.LocateAudioSource("DefractorRotationStop", transform); }
        
        DeactivateRotationPS();
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current state: is rotating " + rotating + " is slowing down " + slowingDown + " with elapsed " + elapsed);
    }

    void StartRotation()
    {
        if ( soundManager != null && !knivesRotationSounds.isPlaying) { knivesRotationSounds.Play(); }
        ActivateRotationPS();
        //Debug.Log("started cones rotation2");
        rotating = true;
        slowingDown = false;
        StopAllCoroutines();
        StartCoroutine(RotateCoroutine());
        StartCoroutine(Delay());
    }

    void StopRotation()
    {
        if (soundManager != null && knivesRotationSounds.isPlaying) { knivesRotationSounds.Stop(); knivesStopSounds.Play(); }
        DeactivateRotationPS();
        if (!slowingDown)
        {
            slowingDown = true;
            rotating = false;
            elapsed = 0;
            StopAllCoroutines();
            StartCoroutine(StopRotationCoroutine());
        }
    }

    public void StopRotation(float delay)
    {
        if (soundManager != null && knivesRotationSounds.isPlaying) { knivesRotationSounds.Stop(); knivesStopSounds.Play(); }
        //DeactivateRotationPS();
        slowingDown = true;
        rotating = false;
        elapsed = delay;
        StopAllCoroutines();
        StartCoroutine(StopRotationCoroutine(elapsed));
    }

    public void StartRotation(float delay)
    {
        if (soundManager != null && !knivesRotationSounds.isPlaying) { knivesRotationSounds.Play(); }
        //ActivateRotationPS();
        Debug.Log("started cones rotation2");
        rotating = true;
        slowingDown = false;
        StopAllCoroutines();
        StartCoroutine(RotateCoroutine(delay));
        StartCoroutine(Delay(delay));
    }

    public void StartSlowingDown()
    {
        slowingDown = true;
        rotating = false;
        elapsed = 0;
        StopAllCoroutines();
        StartCoroutine(StopRotationCoroutine());
    }

    public void ImmediateStopRotation()
    {
        if (soundManager != null && knivesRotationSounds.isPlaying) { knivesRotationSounds.Stop(); }
        StopAllCoroutines();
        slowingDown = false;
        rotating = false;
        elapsed = 0;
        transform.Rotate(0, 0, 0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        StopRotation();
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(5 - delay);
        StopRotation();
    }

    IEnumerator RotateCoroutine()
    {
        //Debug.Log("started cones rotation3");
        elapsed = 0;
        var done = false;
        while (true)
        {
            yield return null;
            if (!done && currentSpeed < requiredSpeed)
            {
                elapsed += Time.deltaTime;
                currentSpeed += Time.deltaTime * 100;
            } else if (!done && currentSpeed >= requiredSpeed) { done = true; currentSpeed = requiredSpeed; }
            //.Log(currentSpeed);
            //Debug.Log("started cones rotation4");
            transform.Rotate(0, 0, currentSpeed * direction * Time.deltaTime);
        }
    }

    IEnumerator RotateCoroutine(float delay)
    {
        Debug.Log("started cones rotation3");
        elapsed = delay;
        var done = false;
        currentSpeed = Time.deltaTime * 100 * 60 * delay;
        while (true)
        {
            yield return null;
            if (!done && currentSpeed < requiredSpeed)
            {
                elapsed += Time.deltaTime;
                //Debug.Log(currentSpeed);
                currentSpeed += Time.deltaTime * 100;
            }
            else if (!done && currentSpeed >= requiredSpeed) { done = true; currentSpeed = requiredSpeed; }
            //Debug.Log("started cones rotation4");
            transform.Rotate(0, 0, currentSpeed * direction * Time.deltaTime);
        }
    }

    IEnumerator StopRotationCoroutine()
    {
        slowingDown = true;
        elapsed = 0;
        
        var done = false;
        while (true)
        {
            yield return null;
            if (!done && currentSpeed > 0)
            {
                elapsed += Time.deltaTime;
                currentSpeed -= Time.deltaTime * 100;
                //Debug.Log(currentSpeed);
                //Debug.Log(currentSpeed);
            }
            else if (!done && currentSpeed <= requiredSpeed) { done = true; currentSpeed = 0; slowingDown = false; elapsed = 0; StopAllCoroutines(); }
            transform.Rotate(0, 0, currentSpeed * direction * Time.deltaTime);
        }
    }

    IEnumerator StopRotationCoroutine(float delay)
    {
        slowingDown = true;
        elapsed = delay;
        currentSpeed = 250 - Time.deltaTime * 100 * 60 * delay;
        var done = false;
        while (true)
        {
            yield return null;
            if (!done && currentSpeed > 0)
            {
                elapsed += Time.deltaTime;
                //Debug.Log(currentSpeed);
                currentSpeed -= Time.deltaTime * 100;
                //Debug.Log(currentSpeed);
            }
            else if (!done && currentSpeed <= requiredSpeed) { done = true; currentSpeed = 0; slowingDown = false; elapsed = 0; StopAllCoroutines(); }
            transform.Rotate(0, 0, currentSpeed * direction * Time.deltaTime);
        }
    }

    void ActivateRotationPS()
    {
        if (soundManager != null && !rotationParticleSystem.isPlaying)
        {
            appearanceTransmutationCircle.CircleAppearance();
            conjurationAppearSound.Play();
            circleShown = true;
        }
    }

    public void ImmediateActivateRotationPS()
    {
        ImmediateDeactivateRotationPS();
        appearanceTransmutationCircle.CircleAppearance();
        conjurationAppearSound.Play();
        circleShown = true;
    }

    void DeactivateRotationPS()
    {
        if (soundManager != null && rotationParticleSystem.isPlaying)
        {
            appearanceTransmutationCircle.CircleDisappearance();
            circleShown = false;
        }
    }

    public void ImmediateDeactivateRotationPS()
    {
        appearanceTransmutationCircle.ImmediateCircleDisappearance();
        circleShown = false;
    }
}
