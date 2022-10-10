using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreMiningHighlighing : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] OreHealthDecreaser oreHealthDecreaser;
    [SerializeField] Transform changedCrystal;

    [Header("Settings")]
    [SerializeField] float highlightingIntensification;
    [SerializeField] float highlightingDuration;
    [SerializeField] AnimationCurve animationCurve;

    Coroutine hightOreCoroutine;
    Material startMaterial;
    Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        startMaterial = changedCrystal.GetComponent<MeshRenderer>().material;
        startColor = startMaterial.color;
        oreHealthDecreaser.HealthReachedZero += StartHighlightOre;
    }

    void StartHighlightOre()
    {
        if (hightOreCoroutine != null) { StopCoroutine(hightOreCoroutine); }
        hightOreCoroutine = StartCoroutine(HighlightOre());
    }

    IEnumerator HighlightOre()
    {
        float elapsed = 0;
        float startEmission = 2.5f;
        float currentEmission = 0f;

        while (elapsed < highlightingDuration)
        {
            elapsed += Time.deltaTime;
            currentEmission = Mathf.Lerp(startEmission, highlightingIntensification, animationCurve.Evaluate(elapsed / highlightingDuration));
            //Debug.Log(currentEmission);
            //Debug.Log(startMaterial.GetColor("_EmissionColor"));
            startMaterial.SetColor("_EmissionColor", startColor * currentEmission);
            
            yield return null;
        }
        //currentEmission = highlightingIntensification;
        startMaterial.SetColor("_EmissionColor", startColor * 2.5f);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartHighlightOre();
        }
    }
}
