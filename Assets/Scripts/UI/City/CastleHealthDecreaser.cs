using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthDecreaser : MonoBehaviour
{
    [SerializeField] private CastleDamageCalculator m_castleDamageCalculator;
    [SerializeField] private CityCastleUpgrade m_cityCastleUpgrade;
    [SerializeField] private Text m_healthCounter;
    
    float minimalWidth = 0;
    float maximumWidth = 10000;
    private float maximumHealth = 250;
    float currentHealth = 100;
    float calibrationHealth;

    public float MaximumWidth
    {
        get
        {
            return maximumWidth;
            
        }
    }

    public float CurrentWidth
    {
        get {
            float leftHealthPercent = ((calibrationHealth) / maximumHealth) * 100;
            float currentWidth = Mathf.Clamp(leftHealthPercent, 0, 100);
            return currentWidth; }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public int MaximumHealth
    {
        get { return (int) maximumHealth; }
    }

    public float CalibrationHealth
    {
        get
        {
            return calibrationHealth;
        }
    }
    public event Action<float> CastleRegenerationStarted = delegate { };
    public event Action<int> CastleHealthChanged = delegate { };

    private void Start()
    {
        m_healthCounter.text = ((int)currentHealth).ToString();
        RegenerateHealth(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DealDamage(10);
        }
    }


    public void DealDamage(float damage)
    {
        if (currentHealth - damage > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            RegeneratoToMaximum();
            return;
        }
        
        m_healthCounter.text = ((int)currentHealth).ToString();
        float leftHealthPercent = ((currentHealth - damage) / maximumHealth) * 100;
        //Debug.Log(leftHealthPercent);
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        UpdateCastleHealth(leftHealthPercent);
        CastleHealthChanged((int)currentHealth);
    }

    void UpdateCastleHealth(float healthPercent)
    {
        int updatedWidth = (int) (healthPercent * maximumWidth / 100);
        
        transform.Find("Borders").Find("Background").Find("Foreground").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
        
    }

    public void RegeneratoToMaximum()
    {
        currentHealth = m_cityCastleUpgrade.RegenerationLevel * maximumHealth / 100;
        m_healthCounter.text = ((int)currentHealth).ToString();
        Debug.Log("has been updated to " + m_cityCastleUpgrade.RegenerationLevel);
        m_castleDamageCalculator.StartCityRegeneration(m_cityCastleUpgrade.RegenerationLevel);
        m_cityCastleUpgrade.DestroyOneSpehere();
    }

    public void RegenerateHealth(float health)
    {
        calibrationHealth = currentHealth;
        if (currentHealth + health <= maximumHealth)
        {
            currentHealth += health;
        } else
        {
            currentHealth = maximumHealth;
        }

        m_healthCounter.text = ((int)currentHealth).ToString();
        
        float leftHealthPercent = ((currentHealth) / maximumHealth) * 100;
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        if (CastleRegenerationStarted != null) { CastleRegenerationStarted(leftHealthPercent); }
        CastleHealthChanged((int)currentHealth);
    }
}
