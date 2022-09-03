using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpellsCaster : MonoBehaviour
{
    [SerializeField] Transform magicbookPaper;
    [SerializeField] AnimationCurve appearAnimationCurve;
    [SerializeField] AnimationCurve intensityAnimationCurve;
    [SerializeField] Animator bookAnimator;
    MeshRenderer paperMeshRenderer;
    public Spell[] spells;
    Spell castedSpell;
    int count;
    string currentSpell;

    Coroutine showLettersCoroutine;

    void Start()
    {
        
        paperMeshRenderer = magicbookPaper.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    public void CastSpell(string spellNameSearched)
    {
        if (!AlreadyCasted(spellNameSearched) && !isCasting()) {
            currentSpell = spellNameSearched;
            count++;
            if (showLettersCoroutine != null) { StopCoroutine(showLettersCoroutine); }
            paperMeshRenderer.materials[1].SetFloat("_ColorMultiplier", 0);
            paperMeshRenderer.materials[1].SetFloat("_Clip", 2f);
            castedSpell = null;
            castedSpell = FindSpell(spellNameSearched);
            if (castedSpell != null)
            {
                Debug.Log("Casting Spell");
                MagicbookAttachSprite(castedSpell.spellTexture);
                ActivateAnimation();
                //StartShowingLetters();
                Debug.Log("Spell casted");
            }
        }
    }

    Spell FindSpell(string spellNameSearched)
    {
        Debug.Log("Searching Spell");
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
        Debug.Log("Animation activated");
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

    IEnumerator ShowingLetters(float duration)
    {
        Debug.Log("Showing letters");
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
