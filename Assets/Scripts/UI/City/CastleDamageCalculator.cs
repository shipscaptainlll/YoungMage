using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDamageCalculator : MonoBehaviour
{
    [SerializeField] private RectTransform m_firstRectTransform;
    [SerializeField] private RectTransform m_secondRectTransform;
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;
    Coroutine decreaseCoroutine;
    int skeletonsNumber;
    float dps;

    // Start is called before the first frame update
    void Awake()
    {
        castleHealthDecreaser.CastleRegenerationStarted += StartCityRegeneration;

        skeletonsNumber = 3;
        CalculateDPS();
        //decreaseCoroutine = StartCoroutine(DecreaseHealth());
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
        float elapsed = 0;
        float regenerationTimeOffset = 1.15f;

        while (elapsed < regenerationTimeOffset)
        {
            elapsed += Time.deltaTime;
            float lerpedHealthWidth = Mathf.Lerp(currentHealthWidth, finalHealthWidth, elapsed / regenerationTimeOffset);
            m_firstRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lerpedHealthWidth);
            m_secondRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, finalHealthWidth);
            yield return null;
        }
        m_secondRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, finalHealthWidth);
        m_firstRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, finalHealthWidth);
        //RestartHealthDecrease();
        yield return null;
    }

    public void StartCityRegeneration(float finalRectWidth)
    {
        StopAllCoroutines();
        StartCoroutine(IncreaseHealth(finalRectWidth));
    }

    public void RestartHealthDecrease()
    {
        //StopAllCoroutines();
        //decreaseCoroutine = StartCoroutine(DecreaseHealth());
    }
}
