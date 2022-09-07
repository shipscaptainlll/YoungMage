using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpellsCaster : MonoBehaviour
{
    [SerializeField] Transform magicbookPaper;
    [SerializeField] AnimationCurve appearAnimationCurve;
    [SerializeField] AnimationCurve intensityAnimationCurve;
    [SerializeField] AnimationCurve enhanceAnimationCurve;
    [SerializeField] Animator bookAnimator;
    [SerializeField] Transform activatedSpellInstance;
    [SerializeField] Transform magicLettersParticles;

    [Header("ObjectPool")]
    [SerializeField] Transform objectPool;
    MeshRenderer paperMeshRenderer;
    public Spell[] spells;
    Spell castedSpell;
    int count;
    string currentSpell;

    public string CurrentSpell { get { return currentSpell; } }
    Coroutine showLettersCoroutine;
    Coroutine enhanceLettersCoroutine;

    void Start()
    {
        
        paperMeshRenderer = magicbookPaper.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    public void CastSpell(string spellNameSearched)
    {
        if (!AlreadyCasted(spellNameSearched)) {
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
                //StartShowingLetters();
                //Debug.Log("Spell casted");
            }
        }
    }

    public void ActivateSpell()
    {
        Debug.Log("here");
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
        if (enhanceLettersCoroutine != null) { StopCoroutine(enhanceLettersCoroutine); }
        enhanceLettersCoroutine = StartCoroutine(EnhanceLetters(0.5f));
        //magicLettersParticles.gameObject.SetActive(false);
        Debug.Log("there");
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

    public void StartShowingLetters()
    {
        if (showLettersCoroutine != null) { StopCoroutine(showLettersCoroutine); }
        showLettersCoroutine = StartCoroutine(ShowingLetters(castedSpell.spellDuration));
    }

    IEnumerator EnhanceLetters(float duration)
    {
        float elapsed = 0;
        float currentEmission;

        Material paperMaterial = paperMeshRenderer.materials[1];
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
