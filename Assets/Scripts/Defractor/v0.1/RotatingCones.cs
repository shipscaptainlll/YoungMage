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
    bool rotating = false; 
    bool slowingDown = false;

    public bool Rotating { get { return rotating; } }

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
        
    }

    void StartRotation()
    {
        if ( soundManager != null && !knivesRotationSounds.isPlaying) { knivesRotationSounds.Play(); }
        ActivateRotationPS();
        Debug.Log("started cones rotation2");
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
            StopAllCoroutines();
            StartCoroutine(StopRotationCoroutine());
        }
        
        
    }

    public void ImmediateStopRotation()
    {
        if (soundManager != null && knivesRotationSounds.isPlaying) { knivesRotationSounds.Stop(); }
        StopAllCoroutines();
        transform.Rotate(0, 0, 0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        StopRotation();
    }

    IEnumerator RotateCoroutine()
    {
        Debug.Log("started cones rotation3");
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
            //Debug.Log("started cones rotation4");
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

    void ActivateRotationPS()
    {
        if (soundManager != null && !rotationParticleSystem.isPlaying)
        {
            appearanceTransmutationCircle.CircleAppearance();
            conjurationAppearSound.Play();
        }
    }

    void DeactivateRotationPS()
    {
        if (soundManager != null && rotationParticleSystem.isPlaying)
        {
            appearanceTransmutationCircle.CircleDisappearance();
        }
    }
}
