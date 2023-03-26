using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CityBlacksmithUpgrade : MonoBehaviour
{
    [Header("Attached main scripts")]
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    [SerializeField] SUINotificator suiNotificator;
    [SerializeField] ParticleSystem upgradePS;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    [SerializeField] Transform circleSoundSource;
    [SerializeField] public string m_notenoughStringLocalized;
    [SerializeField] public string m_maxStringLocalized;
    Coroutine upgradPSCoroutine;

    [Header("Roates to inner elements")]
    [SerializeField] Scrollbar upgradesBar;
    [SerializeField] Transform contentHolder;
    [SerializeField] Text goldText;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationAppearSound;

    int upgradesMaxCount;
    int upgradeCurrentCount;
    float offsetUpgrade;

    int upgradeCost;
    int defaultUpgradeCost;
    float costModifier;

    bool coroutineIsRunning;
    Coroutine scrollingCoroutine;

    int countUpgradesQuests;
    public int UpgradeCurrentCount { get { return upgradeCurrentCount; } }
    public int CountUpgradesQuests { get { return countUpgradesQuests; } }
    public string NotenoughStringLozalized { get {return m_notenoughStringLocalized; } set { m_notenoughStringLocalized = value; } }
    public string MaxStringLozalized { get {return m_maxStringLocalized; } set { m_maxStringLocalized = value; } }
    public event Action<int> BlacksmithUpgradedQuests = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        upgradeCurrentCount = 1;
        offsetUpgrade = 0.075f;
        upgradeCost = 100;
        defaultUpgradeCost = upgradeCost;
        costModifier = 2.5f;
        upgradesMaxCount = contentHolder.childCount - 4;
        upgradesBar.value = ((float)(upgradeCurrentCount) / (float)(upgradesMaxCount+2.5f)) + offsetUpgrade;
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", circleSoundSource);
        //Debug.Log(upgradesBar.value);
    }


    public void UpgradeBlacksmith()
    {
        if (upgradeCurrentCount < upgradesMaxCount)
        {
            if (TakeUpgradeMoney())
            {
                ShowUpgradePS();
                suiNotificator.Notify("-" + upgradeCost);
                upgradeCurrentCount++;
                InitiateScrolling();
                
                UpgradeCost();
                UpdateCostText();
            } else
            {
                suiNotificator.Notify(m_notenoughStringLocalized);
            }
        } else
        {
            suiNotificator.Notify(m_maxStringLocalized);
        }
    }

    public void UploadBlacksmithLevel(int uploadedLevel)
    {
        upgradeCurrentCount = uploadedLevel;
        InitiateScrolling();

        UpgradeCost();
        UpdateCostText();
    }

    bool TakeUpgradeMoney()
    { 
        if (goldCoinsCounter.Count <= upgradeCost)
        {
            goldCoinsCounter.AddResource(-(int)upgradeCost);
            return true;
        }
        return false;
    }

    void UpgradeCost()
    {
        float cacheUpgradeCost = defaultUpgradeCost;
        for (int i = 1; i < upgradeCurrentCount; i++)
        {
            cacheUpgradeCost *= costModifier;
        }
        upgradeCost = (int) (cacheUpgradeCost);
    }

    void UpdateCostText()
    {
        goldText.text = upgradeCost.ToString();
        if (upgradeCurrentCount == upgradesMaxCount) { goldText.text = m_maxStringLocalized; }
    }

    void InitiateScrolling()
    {
        if (coroutineIsRunning)
        {
            StopCoroutine(scrollingCoroutine);
        }
        float destinationValue = ((float)(upgradeCurrentCount) / (float)(upgradesMaxCount + 2.5f)) + offsetUpgrade;
        scrollingCoroutine = StartCoroutine(Scroll(upgradesBar.value, destinationValue, 0.6f));
    }

    IEnumerator Scroll(float startScroll, float endScroll, float delay)
    {
        coroutineIsRunning = true;
        float elapsed = 0;
        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            upgradesBar.value = Mathf.Lerp(startScroll, endScroll, elapsed / delay);
            yield return null;
        }
        upgradesBar.value = endScroll;
        coroutineIsRunning = false;
        yield return null;
    }

    void ShowUpgradePS()
    {
        if (upgradPSCoroutine != null)
        {
            StopCoroutine(upgradPSCoroutine);
        }
        upgradPSCoroutine = StartCoroutine(DelayUpgradePS());
        if (!upgradePS.isPlaying)
        {
            appearanceTransmutationCircle.CircleAppearance();
            //upgradePS.gameObject.SetActive(true);
            //upgradePS.Play();
            conjurationAppearSound.Play();
        }
    }

    IEnumerator DelayUpgradePS()
    {
        yield return new WaitForSeconds(0.75f);
        HideUpgradePS();
        upgradPSCoroutine = null;
    }

    void HideUpgradePS()
    {
        appearanceTransmutationCircle.CircleDisappearance();
        //upgradePS.Stop();
        //upgradePS.gameObject.SetActive(false);
    }
}
