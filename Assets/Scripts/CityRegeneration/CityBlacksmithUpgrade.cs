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
    [SerializeField] private RectTransform healthTransform;
    [SerializeField] private int maxLevels;
    [SerializeField] private Text levelCounter;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private Text m_wallDefence;
    [SerializeField] float settingFinalScale;
    [SerializeField] float settingStartScale;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] private Button m_defenceUpgradeButton;
    [SerializeField] private Color m_upgradeStartColor;
    [SerializeField] private Color m_upgradeFinishColor;
    [SerializeField] private Image m_defenceUpgradeImage;
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
    private float fillAmmount;
    private int m_upgradeCost;
    private int m_currentDefence;

    bool coroutineIsRunning;
    Coroutine fillingCoroutine;

    private Coroutine m_popUpCoroutine;

    int countUpgradesQuests;
    public int CurrentDefence { get { return m_currentDefence; } }
    public int UpgradeCurrentCount { get { return upgradeCurrentCount; } }
    public int CountUpgradesQuests { get { return countUpgradesQuests; } }
    public string NotenoughStringLozalized { get {return m_notenoughStringLocalized; } set { m_notenoughStringLocalized = value; } }
    public string MaxStringLozalized { get {return m_maxStringLocalized; } set { m_maxStringLocalized = value; } }
    public event Action<int> BlacksmithUpgradedQuests = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        upgradeCurrentCount = 1;
        
        upgradesMaxCount = maxLevels;
        //upgradesBar.value = ((float)(upgradeCurrentCount) / (float)(upgradesMaxCount+2.5f)) + offsetUpgrade;
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", circleSoundSource);
        UploadBlacksmithLevel(upgradeCurrentCount);
        //Debug.Log(upgradesBar.value);
    }


    public void UpgradeBlacksmith()
    {
        if (upgradeCurrentCount < upgradesMaxCount)
        {
            if (TakeUpgradeMoney())
            {
                ShowUpgradePS();
                suiNotificator.Notify("-" + m_upgradeCost);
                upgradeCurrentCount++;
                InitiateFilling();
                UpdatelevelCounter();
                UpdateUpgradeImage();
                UpgradeDefenceShower();
                PopUpShield();
                
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
        m_defenceUpgradeButton.gameObject.SetActive(true);
        m_defenceUpgradeImage.color = m_upgradeStartColor;
        upgradeCurrentCount = uploadedLevel;
        InitiateFilling();
        UpdatelevelCounter();
        UpdateUpgradeImage();
        UpgradeDefenceShower();
        
        UpgradeCost();
        UpdateCostText();
    }

    void UpdateUpgradeImage()
    {
        if (upgradeCurrentCount < 3)
        {
            upgradeImage.sprite = BlacksmithSpritesManager.GetSprite(1);
        } else if (upgradeCurrentCount < 5)
        {
            upgradeImage.sprite = BlacksmithSpritesManager.GetSprite(2);
        }
        else
        {
            upgradeImage.sprite = BlacksmithSpritesManager.GetSprite(3);
        }
    }

    void UpdatelevelCounter()
    {
        levelCounter.text = upgradeCurrentCount.ToString();
    }

    void UpgradeDefenceShower()
    {
        m_currentDefence = BlacksmithParametersManager.GetWallDefence(upgradeCurrentCount);
        m_wallDefence.text = "+" + m_currentDefence.ToString();
    }

    bool TakeUpgradeMoney()
    { 
        if (goldCoinsCounter.Count >= m_upgradeCost)
        {
            goldCoinsCounter.AddResource(-(int)m_upgradeCost);
            return true;
        }
        return false;
    }

    void UpgradeCost()
    {
        if (upgradeCurrentCount == maxLevels)
        {
            return;
        }
        m_upgradeCost = BlacksmithParametersManager.GetUpgradeCost(upgradeCurrentCount);
    }

    void UpdateCostText()
    {
        goldText.text = m_upgradeCost.ToString();
        if (upgradeCurrentCount == maxLevels)
        {
            goldText.text = m_maxStringLocalized;
            m_defenceUpgradeButton.gameObject.SetActive(false);
            m_defenceUpgradeImage.color = m_upgradeFinishColor;
        }

        
    }

    void InitiateFilling()
    {
        fillAmmount = upgradeCurrentCount * (100 / upgradesMaxCount);
        float leftHealthPercent = ((fillAmmount) / 100) * 100;
        //Debug.Log("hello there");
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        int updatedWidth = (int)(leftHealthPercent * 1000 / 100);
        
        if (coroutineIsRunning)
        {
            StopCoroutine(fillingCoroutine);
        }

        if (upgradeCurrentCount == maxLevels)
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
    
    void PopUpShield()
    {
        if (m_popUpCoroutine != null) { StopCoroutine(m_popUpCoroutine); }
        m_popUpCoroutine = StartCoroutine(ShieldPopingUp());
    }

    IEnumerator ShieldPopingUp()
    {
        
        float elapsed = 0;
        float maxDuration = 0.5f;
        float currentScale = settingStartScale;
        while (elapsed < maxDuration)
        {
            
            elapsed += Time.deltaTime;
            currentScale = Mathf.Lerp(settingStartScale, settingFinalScale, animationCurve.Evaluate(elapsed/maxDuration));
            upgradeImage.transform.localScale = new Vector3(currentScale, currentScale, 1);
            yield return null;
        }
        currentScale = settingStartScale;
        upgradeImage.transform.localScale = new Vector3(currentScale, currentScale, 1);
        m_popUpCoroutine = null;
        yield return null;
    }
}
