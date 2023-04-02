using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CityWallUpgrade : MonoBehaviour
{
    [Header("Attached main scripts")]
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    [SerializeField] SUINotificator suiNotificator;
    [SerializeField] ParticleSystem upgradeParticleSystem;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    [SerializeField] Transform circleSoundSource;
    [SerializeField] Transform regenerationSoundSource;
    [SerializeField] public string m_notenoughStringLocalized;
    
    [SerializeField] private LearningModeFlow m_learningModeFlow;
    [SerializeField] private LearningCityRegeneration m_learningCityRegeneration;
    

    [Header("Roates to inner elements")]
    [SerializeField] Text healthText;
    [SerializeField] Text goldText;
    [SerializeField] WallRegenerationButton wallRegenerationButton;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationAppearSound;
    AudioSource regenerationSound;

    int wallMaximumHealth;
    float healthCost;

    int hpRegeneratedQuests;

    public string NotenoughStringLozalized { get {return m_notenoughStringLocalized; } set { m_notenoughStringLocalized = value; } }
    public int CountReneratedQuests { get { return hpRegeneratedQuests; } }
    public event Action<int> HealthRegeneratedQuests = delegate { };
    // Start is called before the first frame update
    private void Awake()
    {
        healthCost = 1f;
        wallMaximumHealth = castleHealthDecreaser.MaximumHealth;
        castleHealthDecreaser.CastleHealthChanged += AnalyzeCurrentHealth;
        
    }

    void Start()
    {
        
        
        Debug.Log("maximum is " + wallMaximumHealth);
        
        //goldCoinsCounter.AmmountEnded += AnalyzeGold;
        wallRegenerationButton.ButtonDown += TransformMoneyHealth;
        wallRegenerationButton.ButtonUp += HideUpgradePS;
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", circleSoundSource);
        regenerationSound = soundManager.LocateAudioSource("RegenerationwallRegeneration", regenerationSoundSource);

        healthText.text = castleHealthDecreaser.CurrentHealth.ToString();
        //UpdateGoldShower(0);
    }

    public void TransformMoneyHealth()
    {
        if (castleHealthDecreaser.CurrentHealth == castleHealthDecreaser.MaximumHealth)
        {
            return;
        }
        
        ShowUpgradePS();
        float healthRegenerate = wallRegenerationButton.ButtonDownTime;
        float goldNeeded = Mathf.RoundToInt(wallRegenerationButton.ButtonDownTime * healthCost);
        Debug.Log(goldNeeded);
        if (goldCoinsCounter.Count >= goldNeeded)
        {
            suiNotificator.Notify("-" + goldNeeded);
            goldCoinsCounter.AddResource(-(int)goldNeeded);
            castleHealthDecreaser.RegenerateHealth(goldNeeded / healthCost);
        } else
        {
            suiNotificator.Notify(m_notenoughStringLocalized);
        }
        
    }

    public void AnalyzeCurrentHealth(int currentHealth)
    {
        Debug.Log("current he " + currentHealth + " " + CalculateRequiredGold(currentHealth));
        UpdateHealthShower(currentHealth);
        UpdateGoldShower(CalculateRequiredGold(currentHealth));
    }

    void UpdateHealthShower(int currentHealth)
    {
        healthText.text = currentHealth.ToString();
    }

    void UpdateGoldShower(int currentGold)
    {
        goldText.text = currentGold.ToString();
    }

    int CalculateRequiredGold(int currentHealth)
    {
        return Mathf.RoundToInt((wallMaximumHealth - currentHealth) * healthCost);
    }

    void ShowUpgradePS()
    {
        if (!upgradeParticleSystem.isPlaying)
        {
            appearanceTransmutationCircle.CircleAppearance();
            //upgradeParticleSystem.gameObject.SetActive(true);
            //upgradeParticleSystem.Play();
            conjurationAppearSound.Play();
            regenerationSound.Play();
        }
    }

    void HideUpgradePS()
    {
        
        if (castleHealthDecreaser.CurrentHealth / castleHealthDecreaser.MaximumHealth >= 0.9f)
        {
            if (m_learningCityRegeneration.NextStep == 3)
            {
                m_learningCityRegeneration.ShowNextStep();
            }
        }
        appearanceTransmutationCircle.CircleDisappearance();
        //upgradeParticleSystem.Stop();
        regenerationSound.Stop();
        //upgradeParticleSystem.gameObject.SetActive(false);
    }
}
