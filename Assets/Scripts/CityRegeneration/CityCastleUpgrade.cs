using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CityCastleUpgrade : MonoBehaviour
{
    [Header("Attached main scripts")]
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    [SerializeField] SUINotificator suiNotificator;
    [SerializeField] ParticleSystem upgradePS;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    [SerializeField] Transform circleSoundSource;
    Coroutine upgradPSCoroutine;

    [Header("Roates to inner elements")]
    [SerializeField] Scrollbar upgradesBar;
    [SerializeField] Transform contentHolder;
    [SerializeField] Transform countButtonsHolder;
    [SerializeField] Transform fillShardsHolder;
    [SerializeField] Text sphereGoldText;
    [SerializeField] Text countGoldText;
    [SerializeField] Text fillGoldText;
    [SerializeField] Text fillText;
    [SerializeField] Transform SUINotificator;
    [SerializeField] public string m_notenoughStringLocalized;
    [SerializeField] public string m_maxStringLocalized;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationAppearSound;

    [Header("Loading")]
    [SerializeField] Color firstButtonColor;
    [SerializeField] Color defaultButtonColor;

    int sphereUpgradesMaxCount;
    int sphereUpgradeCurrentCount;
    float offsetUpgrade;

    int countUpgradesMaxCount;
    int countUpgradeCurrentCount;

    int sphereUpgradeCost;
    int sphereUpgradeDefaultCost;
    float sphereCostModifier;

    int countUpgradeCost;
    int countUpgradeDefaultCost;
    float countcostModifier;

    int fillCost;
    int fillAmmount;

    List<Transform> availableButtons = new List<Transform>();
    List<Transform> availableShards;
    Transform currentShard;
    bool coroutineIsRunning;
    Coroutine scrollingCoroutine;

    int countUpgradesQuests;
    int countShardsQuests;
    public int CountUpgradesQuests { get { return countUpgradesQuests; } }
    public int CountShardsQuests { get { return countShardsQuests; } }
    public int SphereUpgradeCurrentCount { get { return sphereUpgradeCurrentCount; } }
    public int CountUpgradeCurrentCount { get { return countUpgradeCurrentCount; } }
    public string NotenoughStringLozalized { get {return m_notenoughStringLocalized; } set { m_notenoughStringLocalized = value; } }
    public string MaxStringLozalized { get {return m_maxStringLocalized; } set { m_maxStringLocalized = value; } }
    public event Action<int> CastleUpgradedQuests = delegate { };
    public event Action<int> ShardsUpgradedQuests = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        sphereUpgradeCurrentCount = 1;
        //Debug.Log(sphereUpgradeCurrentCount);
        //Debug.Log(sphereUpgradesMaxCount);
        countUpgradeCurrentCount = 1;
        offsetUpgrade = 0.075f;
        sphereUpgradeCost = 100;
        sphereUpgradeDefaultCost = sphereUpgradeCost;
        sphereCostModifier = 2.5f;
        countUpgradeCost = 1000;
        countUpgradeDefaultCost = countUpgradeCost;
        countcostModifier = 2f;
        sphereUpgradesMaxCount = contentHolder.childCount - 4;
        countUpgradesMaxCount = 5;
        foreach (Transform button in countButtonsHolder)
        {
            availableButtons.Add(button);
        }
        upgradesBar.value = ((float)(sphereUpgradeCurrentCount) / (float)(sphereUpgradesMaxCount + 2.5f)) + offsetUpgrade;
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", circleSoundSource);
        //Debug.Log(sphereUpgradeCurrentCount);
        //Debug.Log(sphereUpgradesMaxCount);
    }


    public void UpgradeSpheresEnergy()
    {
        //Debug.Log(sphereUpgradeCurrentCount);
        //Debug.Log(sphereUpgradesMaxCount);
        if (sphereUpgradeCurrentCount < sphereUpgradesMaxCount)
        {
            
            //Debug.Log("there hello if");
            if (TakeUpgradeMoney(sphereUpgradeCost))
            {
                ShowUpgradePS();
                suiNotificator.Notify("-" + sphereUpgradeCost);
                Debug.Log("there hello if1");
                sphereUpgradeCurrentCount++;
                InitiateScrolling();

                UpgradeCost(ref sphereUpgradeCost, sphereCostModifier);
                UpdateCostText(sphereGoldText, sphereUpgradeCost, sphereUpgradeCurrentCount, sphereUpgradesMaxCount);
            } else
            {
                suiNotificator.Notify(m_notenoughStringLocalized);
            }
        } else
        {
            suiNotificator.Notify(m_maxStringLocalized);
        }
    }

    public void UploadCastleData(int uploadedSphereLevel, int uploadedCountLevel)
    {
        sphereUpgradeCurrentCount = uploadedCountLevel;
        InitiateScrolling();

        UpgradeSphereCost(ref sphereUpgradeCost, sphereCostModifier);
        UpdateCostText(sphereGoldText, sphereUpgradeCost, sphereUpgradeCurrentCount, sphereUpgradesMaxCount);

        countUpgradeCurrentCount = uploadedCountLevel;

        ResetButtons();
        if (countUpgradeCurrentCount > 1)
        {
            ShowButtons();
        }
        
        UpgradeCountCost(ref countUpgradeCost, countcostModifier);
        UpdateCostText(countGoldText, countUpgradeCost, countUpgradeCurrentCount, countUpgradesMaxCount);
    }

    public void UpdateSphereEnergy(int uploadedLevel, int uploadedCost)
    {
        sphereUpgradeCurrentCount = uploadedLevel;
        InitiateScrolling();
        sphereUpgradeCost = uploadedCost;
        UpdateCostText(sphereGoldText, sphereUpgradeCost, sphereUpgradeCurrentCount, sphereUpgradesMaxCount);
    }

    public void UpgradeSpheresCount()
    {
        if (countUpgradeCurrentCount < countUpgradesMaxCount)
        {
            Debug.Log("there hello if22");
            if (TakeUpgradeMoney(countUpgradeCost))
            {
                ShowUpgradePS();
                suiNotificator.Notify("-" + countUpgradeCost);
                countUpgradeCurrentCount++;
                //availableShards[countUpgradeCurrentCount - 1].GetComponent<CanvasGroup>().alpha = 1;
                ShowNextButton();
                Debug.Log("there hello if2");
                //ShowNextShard();
                UpgradeCost(ref countUpgradeCost, countcostModifier);
                UpdateCostText(countGoldText, countUpgradeCost, countUpgradeCurrentCount, countUpgradesMaxCount);
            } else
            {
                suiNotificator.Notify(m_notenoughStringLocalized);
            }
        } else
        {
            suiNotificator.Notify(m_maxStringLocalized);
        }
    }

    bool TakeUpgradeMoney(int upgradeCost)
    {
        Debug.Log(goldCoinsCounter.Count);
        if (goldCoinsCounter.Count <= upgradeCost)
        {
            
            goldCoinsCounter.AddResource(-(int)upgradeCost);
            return true;
        }
        return false;
    }

    void UpgradeSphereCost(ref int upgradeCost, float costModifier)
    {
        float cacheUpgradeCost = sphereUpgradeDefaultCost;
        for (int i = 1; i < sphereUpgradeCurrentCount; i++)
        {
            cacheUpgradeCost *= costModifier;
        }
        sphereUpgradeCost = (int)(cacheUpgradeCost);
    }

    void UpgradeCountCost(ref int upgradeCost, float costModifier)
    {
        float cacheUpgradeCost = countUpgradeDefaultCost;
        for (int i = 1; i < countUpgradeCurrentCount; i++)
        {
            cacheUpgradeCost *= costModifier;
        }
        countUpgradeCost = (int)(cacheUpgradeCost);
    }

    void UpgradeCost(ref int upgradeCost, float costModifier)
    {

        upgradeCost = (int)(upgradeCost * costModifier);
    }

    void UpdateCostText(Text costTextHolder, float upgradeCost, float currentLevel, float maxLevel)
    {
        costTextHolder.text = upgradeCost.ToString();
        if (currentLevel == maxLevel) { costTextHolder.text = "MAX"; }
    }

    void InitiateScrolling()
    {
        if (coroutineIsRunning)
        {
            StopCoroutine(scrollingCoroutine);
        }
        float destinationValue = ((float)(sphereUpgradeCurrentCount) / (float)(sphereUpgradesMaxCount + 2.5f)) + offsetUpgrade;
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

    void ResetButtons()
    {
        availableButtons[0].GetComponent<Image>().color = firstButtonColor;
        availableButtons[0].GetComponent<Button>().enabled = enabled;
        for (int i = 1; i < availableButtons.Count; i++)
        {
            availableButtons[i].GetComponent<Image>().color = defaultButtonColor;
            availableButtons[i].GetComponent<Button>().enabled = false;
        }
    }

    void ShowButtons()
    {
        
        for (int i = 0; i < countUpgradeCurrentCount - 1; i++)
        {
            var color = defaultButtonColor;
            color.a = 1f;
            availableButtons[i].GetComponent<Image>().color = color;
            availableButtons[i].GetComponent<Button>().enabled = false;
        }
        availableButtons[countUpgradeCurrentCount - 1].GetComponent<Image>().color = firstButtonColor;
        availableButtons[countUpgradeCurrentCount - 1].GetComponent<Button>().enabled = true;
    }


    void ShowNextButton()
    {
        var color = availableButtons[countUpgradeCurrentCount-2].GetComponent<Image>().color;
        color.a = 1f;
        availableButtons[countUpgradeCurrentCount-2].GetComponent<Image>().color = color;
        availableButtons[countUpgradeCurrentCount - 2].GetComponent<Button>().enabled = false;
        if (countUpgradeCurrentCount != countUpgradesMaxCount)
        {
            color = availableButtons[countUpgradeCurrentCount-1].GetComponent<Image>().color;
            color.a = 0.25f;
            availableButtons[countUpgradeCurrentCount-1].GetComponent<Image>().color = color;
            
            availableButtons[countUpgradeCurrentCount-1].GetComponent<Button>().enabled = true;
            availableButtons[countUpgradeCurrentCount - 1].GetComponent<Regeneration2DSUI>().enabled = true;
        }
    }

    void ShowNextShard()
    {
        availableShards[countUpgradeCurrentCount - 1].GetComponent<CanvasGroup>().alpha = 1;
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
