using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpellsCaster : MonoBehaviour
{
    [SerializeField] Transform magicbookPaper;
    [SerializeField] AnimationCurve appearAnimationCurve;
    [SerializeField] AnimationCurve intensityAnimationCurve;
    MeshRenderer paperMeshRenderer;
    public Spell[] spells;

    Coroutine showLettersCoroutine;

    void Start()
    {
        
        paperMeshRenderer = magicbookPaper.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            CastSpell("NewSpell");
        }
    }

    public void CastSpell(string spellNameSearched)
    {
        
        Spell castedSpell = FindSpell(spellNameSearched);
        if (castedSpell != null)
        {
            Debug.Log("Casting Spell");
            MagicbookAttachSprite(castedSpell.spellTexture);
            ActivateAnimation();
            if (showLettersCoroutine != null) { StopCoroutine(showLettersCoroutine); }
            showLettersCoroutine = StartCoroutine(ShowLetters(castedSpell.spellDuration));
            Debug.Log("Spell casted");
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
        Debug.Log("Attaching sprite");
        Debug.Log(paperMeshRenderer.materials[0]);
        Debug.Log(paperMeshRenderer.materials[1]);
        Material paperMaterial = paperMeshRenderer.materials[1];
        paperMaterial.SetTexture("_Texture2D", spellTexture);
    }

    void ActivateAnimation()
    {
        Debug.Log("Animation activated");
    }

    public void ActivateParticleSystem()
    {

    }

    IEnumerator ShowLetters(float duration)
    {
        Debug.Log("Showing letters");
        float elapsed = 0;
        float currentFill;
        float currentEmission;
        Color currentColor;
        
        Material paperMaterial = paperMeshRenderer.materials[1];
        Color lettersColor = paperMaterial.GetColor("_EmissionColor");


        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentFill = Mathf.Lerp(0.088f, 0.154f, appearAnimationCurve.Evaluate(elapsed / duration));
            currentEmission = Mathf.Lerp(0f, 1f, intensityAnimationCurve.Evaluate(elapsed / duration));
            currentColor = new Color(lettersColor.r * currentEmission, lettersColor.g * currentEmission, lettersColor.b * currentEmission);
            paperMaterial.SetFloat("_Fill", currentFill);
            paperMaterial.SetFloat("_ColorMultiplier", currentEmission);
            paperMeshRenderer.materials[1] = paperMaterial;
            yield return null;
        }
        paperMaterial.SetFloat("_Fill", 0.154f);
        paperMaterial.SetFloat("_ColorMultiplier", 1);
        paperMeshRenderer.materials[1] = paperMaterial;
        showLettersCoroutine = null;
        yield return null;
    }
}       
