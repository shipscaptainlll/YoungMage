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
    [SerializeField] private ShatterAnimationSphere m_shatterAnimationSphere;
    Coroutine upgradPSCoroutine;

    [Header("Roates to inner elements")]
    [SerializeField] public string m_notenoughStringLocalized;
    [SerializeField] public string m_maxStringLocalized;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationAppearSound;
    
    [SerializeField] private int sphereUpgradesMaxCount;
    int sphereUpgradeCurrentCount;
    float offsetUpgrade;

    [SerializeField] private int countUpgradesMaxCount;
    int countUpgradeCurrentCount;

    int sphereUpgradeCost;

    int countUpgradeCost;

    int fillCost;
    int fillAmmount;

    
    List<Transform> availableShards;
    Transform currentShard;
    private bool coroutineIsRunning;
    Coroutine fillingCoroutine;
    [SerializeField] private Transform availableButtons;
    [SerializeField] private RectTransform healthTransform;
    [SerializeField] private Text magicPotencyUpgradeText;
    [SerializeField] private Text magicPotencyLevelText;
    [SerializeField] private Text magicPotencyParameterText;
    [SerializeField] private Text sphereBuyText;
    [SerializeField] private Text sphereCountText;
    [SerializeField] private Color m_upgradeStartColor;
    [SerializeField] private Color m_upgradeFinishColor;
    [SerializeField] private Image m_magicPotencyFillImage;
    [SerializeField] private Text m_sphereCountUpper;


    int countUpgradesQuests;
    int countShardsQuests;
    private int m_regenerationLevel;
    public int CountUpgradesQuests { get { return countUpgradesQuests; } }
    public int CountShardsQuests { get { return countShardsQuests; } }
    public int SphereUpgradeCurrentCount { get { return sphereUpgradeCurrentCount; } }
    public int CountUpgradeCurrentCount { get { return countUpgradeCurrentCount; } }
    public string NotenoughStringLozalized { get {return m_notenoughStringLocalized; } set { m_notenoughStringLocalized = value; } }
    public string MaxStringLozalized { get {return m_maxStringLocalized; } set { m_maxStringLocalized = value; } }
    public int RegenerationLevel { get {return m_regenerationLevel; } }
    public event Action<int> CastleUpgradedQuests = delegate { };
    public event Action<int> ShardsUpgradedQuests = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        sphereUpgradeCurrentCount = 1;
        countUpgradeCurrentCount = 1;
        
        InitiateFilling();

        UpgradePotencyCost();
        UpdatePotencyCostText();
        m_regenerationLevel = CityCastleParametersManager.GetRegenerationAmmount(sphereUpgradeCurrentCount);
        ResetButtons();
        ShowButtons();
        
        UpgradeSphereCost();
        UpdateSphereCostText();
        //upgradesBar.value = ((float)(sphereUpgradeCurrentCount) / (float)(sphereUpgradesMaxCount + 2.5f)) + offsetUpgrade;
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", circleSoundSource);
    }


    public void UpgradeSpheresEnergy()
    {
        if (sphereUpgradeCurrentCount < sphereUpgradesMaxCount)
        {
            if (TakeUpgradeMoney(sphereUpgradeCost))
            {
                ShowUpgradePS();
                suiNotificator.Notify("-" + sphereUpgradeCost);
                sphereUpgradeCurrentCount++;
                m_regenerationLevel = CityCastleParametersManager.GetRegenerationAmmount(sphereUpgradeCurrentCount);
                InitiateFilling();

                UpgradePotencyCost();
                UpdatePotencyCostText();
            } else
            {
                suiNotificator.Notify(m_notenoughStringLocalized);
            }
        } else
        {
            suiNotificator.Notify(m_maxStringLocalized);
        }
    }
    
    void InitiateFilling()
    {
        fillAmmount = sphereUpgradeCurrentCount * (100 / sphereUpgradesMaxCount);
        float leftHealthPercent = fillAmmount;
        
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        int updatedWidth = (int)(leftHealthPercent * 1000 / 100);
        
        if (coroutineIsRunning)
        {
            StopCoroutine(fillingCoroutine);
        }

        if (sphereUpgradeCurrentCount == sphereUpgradesMaxCount)
        {
            updatedWidth = 1000;
        }
        fillingCoroutine = StartCoroutine(SmoothFillIncrease(updatedWidth));
    }
    
    IEnumerator SmoothFillIncrease(float updatedWidth)
    {
        float counter = 0;
        float smoothingDuration = 0.15f;
        float initialWidth = healthTransform.rect.width;
        float currentWidth = initialWidth;
        while (counter < smoothingDuration)
        {
            counter += Time.deltaTime;
            currentWidth = Mathf.Lerp(initialWidth, updatedWidth, counter / smoothingDuration);
            //Debug.Log(currentWidth);
            healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }
        healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
        yield return null;
    }

    public void UploadCastleData(int uploadedSphereLevel, int uploadedCountLevel)
    {
        magicPotencyUpgradeText.gameObject.SetActive(true);
        m_magicPotencyFillImage.color = m_upgradeStartColor;
        sphereUpgradeCurrentCount = uploadedCountLevel;
        InitiateFilling();

        UpgradePotencyCost();
        UpdatePotencyCostText();

        countUpgradeCurrentCount = uploadedCountLevel;

        ResetButtons();
        ShowButtons();
        
        UpgradeSphereCost();
        UpdateSphereCostText();
    }

    public void DestroyOneSpehere()
    {
        countUpgradeCurrentCount--;
        if (countUpgradeCurrentCount < 0)
        {
            countUpgradeCurrentCount = 0;
            
        }
        m_shatterAnimationSphere.ActivateAnimation();
        ResetButtons();
        ShowButtons();
        
        UpgradeSphereCost();
        UpdateSphereCostText();
    }

    public void UpdateSphereEnergy(int uploadedLevel, int uploadedCost)
    {
        sphereUpgradeCurrentCount = uploadedLevel;
        InitiateFilling();
        sphereUpgradeCost = uploadedCost;
        UpgradeSphereCost();
    }

    public void UpgradeSpheresCount()
    {
        if (countUpgradeCurrentCount < countUpgradesMaxCount)
        {
            if (TakeUpgradeMoney(countUpgradeCost))
            {
                ShowUpgradePS();
                suiNotificator.Notify("-" + countUpgradeCost);
                countUpgradeCurrentCount++;
                ShowButtons();
                UpgradeSphereCost();
                UpdateSphereCostText();
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
        if (goldCoinsCounter.Count >= upgradeCost)
        {
            
            goldCoinsCounter.AddResource(-(int)upgradeCost);
            return true;
        }
        return false;
    }

    void UpgradePotencyCost()
    {
        sphereUpgradeCost = CityCastleParametersManager.GetMagicUpgradeCost(sphereUpgradeCurrentCount);
    }
    
    void UpgradeSphereCost()
    {
        countUpgradeCost = CityCastleParametersManager.GetSphereCost(countUpgradeCurrentCount);
    }

    void UpdatePotencyCostText()
    {
        magicPotencyUpgradeText.text = sphereUpgradeCost.ToString();
        magicPotencyLevelText.text = sphereUpgradeCurrentCount.ToString();
        magicPotencyParameterText.text = "%" + m_regenerationLevel.ToString();

        if (sphereUpgradeCurrentCount == sphereUpgradesMaxCount)
        {
            magicPotencyUpgradeText.text = "MAX";
            magicPotencyUpgradeText.gameObject.SetActive(false);
            m_magicPotencyFillImage.color = m_upgradeFinishColor;
        }
    }
    
    void UpdateSphereCostText()
    {
        sphereBuyText.text = countUpgradeCost.ToString();
        sphereCountText.text = countUpgradeCurrentCount.ToString();
        m_sphereCountUpper.text = countUpgradeCurrentCount.ToString();
        
        if (countUpgradeCurrentCount == countUpgradesMaxCount) { sphereBuyText.text = "MAX"; }
    }

    

    void ResetButtons()
    {
        for (int i = 0; i < availableButtons.childCount; i++)
        {
            availableButtons.GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void ShowButtons()
    {
        for (int i = 0; i < countUpgradeCurrentCount; i++)
        {
            availableButtons.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
        }
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
    }
}
