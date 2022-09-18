using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpellsCaster : MonoBehaviour
{
    [SerializeField] Transform magicBook;
    [SerializeField] Transform magicbookPaper;
    [SerializeField] AnimationCurve appearAnimationCurve;
    [SerializeField] AnimationCurve intensityAnimationCurve;
    [SerializeField] AnimationCurve enhanceAnimationCurve;
    [SerializeField] Animator bookAnimator;
    [SerializeField] Transform activatedSpellInstance;
    [SerializeField] Transform magicLettersParticles;
    [SerializeField] Texture2D distortionTexture;
    [SerializeField] Transform bookTransform;
    [SerializeField] Transform instantiatedObjectsHolder;

    [Header("ObjectPool")]
    [SerializeField] Transform objectPool;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource bookOpeningSound;
    AudioSource nullSpellCasting;
    AudioSource popUpSound;
    AudioSource spellCasting;
    AudioSource magicLettersAppear;


    MeshRenderer paperMeshRenderer;
    public Spell[] spells;
    Spell castedSpell;
    int count;
    string currentSpell;
    bool bookIsHidden;

    public string CurrentSpell { get { return currentSpell; } }
    Coroutine showLettersCoroutine;
    Coroutine enhanceLettersCoroutine;

    void Start()
    {
        
        paperMeshRenderer = magicbookPaper.GetComponent<MeshRenderer>();
        bookOpeningSound = soundManager.LocateAudioSource("BookOpening", magicbookPaper.parent);
        nullSpellCasting = soundManager.FindSound("NullSpell");
        popUpSound = soundManager.FindSound("PopUp");
        spellCasting = soundManager.LocateAudioSource("CastingSpell", magicbookPaper);
        magicLettersAppear = soundManager.LocateAudioSource("MagicLettersAppear", magicbookPaper);

    }

    void Update()
    {
        
    }

    public void CastSpell(string spellNameSearched)
    {
        if (!AlreadyCasted(spellNameSearched) && !bookIsHidden) {
            currentSpell = spellNameSearched;
            count++;
            if (showLettersCoroutine != null) { StopCoroutine(showLettersCoroutine); }
            paperMeshRenderer.materials[1].SetFloat("_ColorMultiplier", 0);
            paperMeshRenderer.materials[1].SetFloat("_Clip", 2f);
            castedSpell = null;
            castedSpell = FindSpell(spellNameSearched);
            //Debug.Log(castedSpell);
            if (castedSpell != null)
            {
                //Debug.Log("Casting Spell");
                
                MagicbookAttachSprite(castedSpell.spellTexture);
                ActivateAnimation();
                bookOpeningSound.Play();
                
                //StartShowingLetters();
                //Debug.Log("Spell casted");
            }
        }
    }

    public void ActivateSpell()
    {
        if (!bookIsHidden)
        {
            //Debug.Log("here");
            //magicLettersParticles.gameObject.SetActive(true);
            //magicLettersParticles.GetComponent<ParticleSystem>().Play();
            Transform newSpell = Instantiate(activatedSpellInstance, magicbookPaper.position, magicbookPaper.rotation);
            newSpell.gameObject.SetActive(true);
            newSpell.parent = objectPool.transform;
            newSpell.position = magicbookPaper.position;
            newSpell.rotation = magicbookPaper.rotation;
            ActivatedSpell newSpellScript = newSpell.GetComponent<ActivatedSpell>();
            newSpellScript.UpdateSprite(FindSpell(currentSpell).spellTexture);
            newSpellScript.MoveSpell();
            ManageSounds();

            if (castedSpell.spellName == "NullSpell")
            {

                Material paperMaterial = newSpell.GetComponent<MeshRenderer>().material;
                float currentSecondMeshHSV;
                Color overloadingMeshSecondColor = paperMaterial.GetColor("_EmissionColor");
                float hMeshSecond, sMeshSecond, vMeshSecond;
                Color.RGBToHSV(overloadingMeshSecondColor, out hMeshSecond, out sMeshSecond, out vMeshSecond);
                currentSecondMeshHSV = 1;
                overloadingMeshSecondColor = Color.HSVToRGB(currentSecondMeshHSV, sMeshSecond, vMeshSecond);
                paperMaterial.SetColor("_EmissionColor", overloadingMeshSecondColor);
                newSpell.GetComponent<MeshRenderer>().material = paperMaterial;
            }


            if (enhanceLettersCoroutine != null) { StopCoroutine(enhanceLettersCoroutine); }
            enhanceLettersCoroutine = StartCoroutine(EnhanceLetters(0.5f));
            //magicLettersParticles.gameObject.SetActive(false);
            //Debug.Log("there");
        }
    }

    void ManageSounds()
    {
        if (castedSpell.spellName == "NullSpell")
        {
            nullSpellCasting.Play();
        }
        else { spellCasting.Play(); }
        if (castedSpell.spellName == "ThrowObject")
        {
            popUpSound.Play();
        }
        
    }

    public void HideBook()
    {
        
    }

    public void ShowBook()
    {
        
    }

    Spell FindSpell(string spellNameSearched)
    {
        //Debug.Log("Searching Spell");
        if (Array.Find(spells, spell => spell.spellName == spellNameSearched) != null) {
            return Array.Find(spells, spell => spell.spellName == spellNameSearched);
        }
        return null;
    }

    void MagicbookAttachSprite(Texture2D spellTexture)
    {
        Material paperMaterial = paperMeshRenderer.materials[1];
        paperMaterial.SetTexture("_Texture2D", spellTexture);
    }

    void ActivateAnimation()
    {
        //Debug.Log("Animation activated");
        if (count % 2 == 0)
        {
            bookAnimator.Play("BookPrecasting");
            
        } else { bookAnimator.Play("BookPrecasting 1"); }
        
    }

    void EndAnimation()
    {
        
    }

    public void ActivateParticleSystem()
    {

    }

    public void ShowSpellsBook()
    {
        bookIsHidden = false;
        bookTransform.gameObject.SetActive(true);
        if (instantiatedObjectsHolder.GetChild(0) != null && instantiatedObjectsHolder.GetChild(0).GetComponent<MeshRenderer>() != null) { instantiatedObjectsHolder.GetChild(0).GetComponent<MeshRenderer>().enabled = true; }
        if (instantiatedObjectsHolder.GetChild(0).GetChild(0) != null && instantiatedObjectsHolder.GetChild(0).GetChild(0).GetComponent<MeshRenderer>() != null) { instantiatedObjectsHolder.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true; }
        if (instantiatedObjectsHolder.GetChild(0).GetChild(0).GetChild(0) != null && instantiatedObjectsHolder.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>() != null) { instantiatedObjectsHolder.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true; }
    }

    public void HideSpellsBook()
    {
        bookIsHidden = true;
        bookTransform.gameObject.SetActive(false);
        if (instantiatedObjectsHolder.GetChild(0) != null && instantiatedObjectsHolder.GetChild(0).GetComponent<MeshRenderer>() != null) { instantiatedObjectsHolder.GetChild(0).GetComponent<MeshRenderer>().enabled = false; }
        if (instantiatedObjectsHolder.GetChild(0).GetChild(0) != null && instantiatedObjectsHolder.GetChild(0).GetChild(0).GetComponent<MeshRenderer>() != null) { instantiatedObjectsHolder.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false; }
        if (instantiatedObjectsHolder.GetChild(0).GetChild(0).GetChild(0) != null && instantiatedObjectsHolder.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>() != null) { instantiatedObjectsHolder.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false; }
    }

    public void StartShowingLetters()
    {
        if (castedSpell.spellName != "NullSpell")
        {
            if (showLettersCoroutine != null) { StopCoroutine(showLettersCoroutine); }
            showLettersCoroutine = StartCoroutine(ShowingLetters(castedSpell.spellDuration));
        }
        
    }

    IEnumerator EnhanceLetters(float duration)
    {
        
        
        float elapsed = 0;
        float currentEmission;

        Material paperMaterial = paperMeshRenderer.materials[1];


        if (castedSpell.spellName == "NullSpell")
        {
            MagicbookAttachSprite(distortionTexture);
            paperMaterial.SetFloat("_Clip", 0.8f);
            
            float currentSecondMeshHSV;
            Color overloadingMeshSecondColor = paperMaterial.GetColor("_EmissionColor");
            float hMeshSecond, sMeshSecond, vMeshSecond;
            Color.RGBToHSV(overloadingMeshSecondColor, out hMeshSecond, out sMeshSecond, out vMeshSecond);
            currentSecondMeshHSV = 1;
            overloadingMeshSecondColor = Color.HSVToRGB(currentSecondMeshHSV, sMeshSecond, vMeshSecond);
            paperMaterial.SetColor("_EmissionColor", overloadingMeshSecondColor);
            paperMeshRenderer.materials[1] = paperMaterial;
        }
        

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentEmission = Mathf.Lerp(1.5f, 90f, enhanceAnimationCurve.Evaluate(elapsed / duration));
            paperMaterial.SetFloat("_ColorMultiplier", currentEmission);
            paperMeshRenderer.materials[1] = paperMaterial;
            yield return null;
        }
        paperMaterial.SetFloat("_ColorMultiplier", 1.5f);
        paperMeshRenderer.materials[1] = paperMaterial;
        enhanceLettersCoroutine = null;

        if (castedSpell.spellName == "NullSpell")
        {
            paperMaterial.SetFloat("_Clip", 2f);
            float currentSecondMeshHSV;
            Color overloadingMeshSecondColor = paperMaterial.GetColor("_EmissionColor");
            float hMeshSecond, sMeshSecond, vMeshSecond;
            Color.RGBToHSV(overloadingMeshSecondColor, out hMeshSecond, out sMeshSecond, out vMeshSecond);
            currentSecondMeshHSV = 0.69f;
            overloadingMeshSecondColor = Color.HSVToRGB(currentSecondMeshHSV, sMeshSecond, vMeshSecond);
            paperMaterial.SetColor("_EmissionColor", overloadingMeshSecondColor);
            paperMeshRenderer.materials[1] = paperMaterial;
            paperMeshRenderer.materials[1] = paperMaterial;
        }
        yield return null;
    }

    IEnumerator ShowingLetters(float duration)
    {
        //Debug.Log("Showing letters");
        float elapsed = 0;
        float currentFill;
        float currentEmission;
        float currentClip;
        Color currentColor;
        
        Material paperMaterial = paperMeshRenderer.materials[1];
        Color lettersColor = paperMaterial.GetColor("_EmissionColor");
        magicLettersAppear.Play();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            //currentFill = Mathf.Lerp(0f, 0.5f, appearAnimationCurve.Evaluate(elapsed / duration));
            currentEmission = Mathf.Lerp(0f, 1.5f, intensityAnimationCurve.Evaluate(elapsed / duration));
            currentClip = Mathf.Lerp(2f, 0.8f, appearAnimationCurve.Evaluate(elapsed / duration));
            currentColor = new Color(lettersColor.r * currentEmission, lettersColor.g * currentEmission, lettersColor.b * currentEmission);
            //paperMaterial.SetFloat("_Fill", currentFill);
            paperMaterial.SetFloat("_ColorMultiplier", currentEmission);
            paperMaterial.SetFloat("_Clip", currentClip);
            paperMeshRenderer.materials[1] = paperMaterial;
            yield return null;
        }
        //paperMaterial.SetFloat("_Fill", 0.5f);
        paperMaterial.SetFloat("_ColorMultiplier", 1.5f);
        paperMaterial.SetFloat("_Clip", 0.8f);
        paperMeshRenderer.materials[1] = paperMaterial;
        showLettersCoroutine = null;
        yield return null;
    }

    bool AlreadyCasted(string newSpell)
    {
        if (currentSpell != "") { return currentSpell == newSpell; }
        else { return false; }
    }

    bool isCasting()
    {
        return (bookAnimator.GetCurrentAnimatorStateInfo(0).IsName("BookPrecasting")
            || bookAnimator.GetCurrentAnimatorStateInfo(0).IsName("BookPrecasting 1"));
    }
}       
