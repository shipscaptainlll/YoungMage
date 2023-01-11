using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonNecklessBehavior : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] Transform necklessParticleSystem;
    [SerializeField] Transform letersParticleSystem;
    Coroutine showLettersCoroutine;
    Coroutine enteringDestructionCoroutine;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationSound;
    AudioSource deconjurationSound;
    AudioSource skeletonWaves;

    public Transform NecklessParticleSystem { get { return necklessParticleSystem; } }
    public Transform LettersParticleSystem { get { return letersParticleSystem; } }
    public void ActivateConjurationNeckless()
    {
        if (conjurationSound != null)
        {
            conjurationSound.Play();
        }
        if (skeletonWaves != null)
        {
            skeletonWaves.Play();
        }
        
        necklessParticleSystem.gameObject.SetActive(true);
        necklessParticleSystem.GetComponent<ParticleSystem>().Play();
        letersParticleSystem.gameObject.SetActive(true);
        letersParticleSystem.GetComponent<ParticleSystem>().Play();
        showLettersCoroutine = StartCoroutine(ShowingLetters(1));
    }

    public void ActivateDestructionMode()
    {
        deconjurationSound.Play();
        if (enteringDestructionCoroutine == null) { enteringDestructionCoroutine = StartCoroutine(EnteringDestructionMode(1, 0.999f)); } 
        else { StopCoroutine(enteringDestructionCoroutine); enteringDestructionCoroutine = StartCoroutine(EnteringDestructionMode(1, 0.999f)); }
    }

    public void ActivateNormalMode()
    {
        conjurationSound.Play();
        if (enteringDestructionCoroutine == null) { enteringDestructionCoroutine = StartCoroutine(EnteringDestructionMode(0.1f, 0.69f)); }
        else { StopCoroutine(enteringDestructionCoroutine); enteringDestructionCoroutine = StartCoroutine(EnteringDestructionMode(0.1f, 0.69f)); }
    }

    void Start()
    {
        conjurationSound = soundManager.LocateAudioSource("SkeletonConjuration", transform);
        deconjurationSound = soundManager.LocateAudioSource("SkeletonDeconjuration", transform);
        skeletonWaves = soundManager.LocateAudioSource("SkeletonWaves", transform);
    }

    void Update()
    {
        
    }

    IEnumerator ShowingLetters(float duration)
    {
        float elapsed = 0;
        float currentClip;
        ParticleSystemRenderer necklessMeshRenderer = necklessParticleSystem.GetComponent<ParticleSystemRenderer>();
        Material necklessMaterial = necklessMeshRenderer.material;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentClip = Mathf.Lerp(2f, 0f, elapsed / duration);
            necklessMaterial.SetFloat("_Clip", currentClip);
            necklessMeshRenderer.material = necklessMaterial;
            yield return null;
        }
        necklessMaterial.SetFloat("_Clip", 0);
        necklessMeshRenderer.material = necklessMaterial;
        showLettersCoroutine = null;
        yield return null;
    }

    IEnumerator EnteringDestructionMode(float duration, float endValue)
    {
        float elapsed = 0;
        float currentHSV;
        ParticleSystemRenderer necklessMeshRenderer = necklessParticleSystem.GetComponent<ParticleSystemRenderer>();
        ParticleSystemRenderer lettersMeshRenderer = letersParticleSystem.GetComponent<ParticleSystemRenderer>();
        Material necklessMaterial = necklessMeshRenderer.material;
        Material lettersMaterial = lettersMeshRenderer.material;
        Color necklessColor = necklessMaterial.GetColor("_EmissionColor");
        Color lettersColor = lettersMaterial.GetColor("_EmissionColor");

        Color currentNecklessColor = necklessMeshRenderer.material.GetColor("_EmissionColor");
        Color currentLettersColor = lettersMeshRenderer.material.GetColor("_EmissionColor");
        float hNeckless, sNeckless, vNeckless;
        float hLetters, sLetters, vLetters;
        Color.RGBToHSV(currentNecklessColor, out hNeckless, out sNeckless, out vNeckless);
        Color.RGBToHSV(currentLettersColor, out hLetters, out sLetters, out vLetters);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentHSV = Mathf.Lerp(hNeckless, endValue, elapsed / duration);
            //Debug.Log(currentHSV);
            necklessColor = Color.HSVToRGB(currentHSV, sNeckless, vNeckless);
            lettersColor = Color.HSVToRGB(currentHSV, sLetters, vLetters);
            necklessMaterial.SetColor("_EmissionColor", necklessColor);
            lettersMaterial.SetColor("_EmissionColor", lettersColor);
            necklessMeshRenderer.material = necklessMaterial;
            lettersMeshRenderer.material = lettersMaterial;

            yield return null;
        }
        necklessColor = Color.HSVToRGB(endValue, sNeckless, vNeckless);
        lettersColor = Color.HSVToRGB(endValue, sLetters, vLetters);

        necklessMaterial.SetColor("_EmissionColor", necklessColor);
        lettersMaterial.SetColor("_EmissionColor", lettersColor);
        necklessMeshRenderer.material = necklessMaterial;
        lettersMeshRenderer.material = lettersMaterial;

        enteringDestructionCoroutine = null;
        yield return null;
    }
}
