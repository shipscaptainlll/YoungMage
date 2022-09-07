using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedSpell : MonoBehaviour
{
    [SerializeField] AnimationCurve intensityAnimationCurve;
    [SerializeField] AnimationCurve appearAnimationCurve;
    MeshRenderer spellMeshRenderer;
    Material emissionMaterial;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite(Texture2D spellTexture)
    {
        spellMeshRenderer = transform.GetComponent<MeshRenderer>();
        emissionMaterial = spellMeshRenderer.material;
        emissionMaterial.SetTexture("_Texture2D", spellTexture);
    }

    public void MoveSpell()
    {
        transform.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -40);
        StartCoroutine(ShowingLetters(0.5f));
    }

    IEnumerator ShowingLetters(float duration)
    {
        //Debug.Log("Showing letters");
        float elapsed = 0;
        float currentFill;
        float currentEmission;
        float currentClip;
        Color currentColor;

        Material paperMaterial = spellMeshRenderer.material;
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
            spellMeshRenderer.material = paperMaterial;
            yield return null;
        }
        //paperMaterial.SetFloat("_Fill", 0.5f);
        paperMaterial.SetFloat("_ColorMultiplier", 0f);
        paperMaterial.SetFloat("_Clip", 2f);
        spellMeshRenderer.material = paperMaterial;
        yield return null;
    }
}
