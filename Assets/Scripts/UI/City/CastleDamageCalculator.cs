using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDamageCalculator : MonoBehaviour
{
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;
    Coroutine decreaseCoroutine;
    int skeletonsNumber;
    float dps;

    // Start is called before the first frame update
    void Start()
    {
        castleHealthDecreaser.CastleRegenerationStarted += StartCityRegeneration;

        skeletonsNumber = 3;
        CalculateDPS();
        //decreaseCoroutine = StartCoroutine(DecreaseHealth());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateDPS()
    {
        dps = skeletonsNumber * 10.1f;
    }

    void CalculateDamage()
    {
        dps = 10f;
    }

    IEnumerator DecreaseHealth()
    {
        while (true)
        {
            castleHealthDecreaser.DealDamage(dps);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator IncreaseHealth(float finalRectWidth)
    {
        float maximumWidth = castleHealthDecreaser.MaximumWidth;
        float currentRectWidth = castleHealthDecreaser.CurrentWidth;
        int currentHealthWidth = (int)(currentRectWidth * maximumWidth / 100);
        int finalHealthWidth = (int)(finalRectWidth * maximumWidth / 100);
        RectTransform healthBorders = transform.Find("Borders").Find("Foreground").GetComponent<RectTransform>();
        float elapsed = 0;
        float regenerationTimeOffset = 1.15f;

        while (elapsed < regenerationTimeOffset)
        {
            elapsed += Time.deltaTime;
            float lerpedHealthWidth = Mathf.Lerp(currentHealthWidth, finalHealthWidth, elapsed / regenerationTimeOffset);
            healthBorders.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lerpedHealthWidth);
            yield return null;
        }
        healthBorders.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, finalHealthWidth);
        RestartHealthDecrease();
        yield return null;
    }

    public void StartCityRegeneration(float finalRectWidth)
    {
        StopAllCoroutines();
        StartCoroutine(IncreaseHealth(finalRectWidth));
    }

    public void RestartHealthDecrease()
    {
        StopAllCoroutines();
        decreaseCoroutine = StartCoroutine(DecreaseHealth());
    }
}
