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

    [Header("Roates to inner elements")]
    [SerializeField] Text healthText;
    [SerializeField] Text goldText;
    [SerializeField] WallRegenerationButton wallRegenerationButton;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransformMoneyHealth()
    {
        float healthRegenerate = wallRegenerationButton.ButtonDownTime;
        float goldNeeded = wallRegenerationButton.ButtonDownTime * healthCost;
        if (goldCoinsCounter.Count >= goldNeeded)
        {
            goldCoinsCounter.AddResource(-(int)goldNeeded);
            castleHealthDecreaser.RegenerateHealth(healthRegenerate);
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

}
