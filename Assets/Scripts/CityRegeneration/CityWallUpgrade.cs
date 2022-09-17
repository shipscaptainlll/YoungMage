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
    [SerializeField] Transform circleSoundSource;

    [Header("Roates to inner elements")]
    [SerializeField] Text healthText;
    [SerializeField] Text goldText;
    [SerializeField] WallRegenerationButton wallRegenerationButton;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationAppearSound;

    int wallMaximumHealth;
    float healthCost;

    int hpRegeneratedQuests;

    public int CountReneratedQuests { get { return hpRegeneratedQuests; } }
    public event Action<int> HealthRegeneratedQuests = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        healthCost = 2.5f;
        wallMaximumHealth = castleHealthDecreaser.MaximumHealth;
        castleHealthDecreaser.CastleHealthChanged += AnalyzeCurrentHealth;
        //goldCoinsCounter.AmmountEnded += AnalyzeGold;
        wallRegenerationButton.ButtonDown += TransformMoneyHealth;
        wallRegenerationButton.ButtonUp += HideUpgradePS;
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", circleSoundSource);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransformMoneyHealth()
    {
        ShowUpgradePS();
        float healthRegenerate = wallRegenerationButton.ButtonDownTime;
        int goldNeeded = Mathf.RoundToInt(wallRegenerationButton.ButtonDownTime * healthCost);
        if (goldCoinsCounter.Count <= goldNeeded)
        {
            suiNotificator.Notify("-" + goldNeeded);
            goldCoinsCounter.AddResource(-(int)goldNeeded);
            castleHealthDecreaser.RegenerateHealth(healthRegenerate);
        } else
        {
            suiNotificator.Notify("not enough gold");
        }
        
    }

    void AnalyzeCurrentHealth(int currentHealth)
    {
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
            upgradeParticleSystem.gameObject.SetActive(true);
            upgradeParticleSystem.Play();
            conjurationAppearSound.Play();
        }
    }

    void HideUpgradePS()
    {
        upgradeParticleSystem.Stop();
        upgradeParticleSystem.gameObject.SetActive(false);
    }
}
