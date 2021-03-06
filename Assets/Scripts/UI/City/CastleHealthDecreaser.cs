using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthDecreaser : MonoBehaviour
{

    float minimalWidth = 0;
    float maximumWidth = 10000;
    float currentHealth = 10000;
    float calibrationHealth;
    public float MaximumWidth
    {
        get { return maximumWidth; }
    }

    public float CurrentWidth
    {
        get {
            float leftHealthPercent = ((calibrationHealth) / maximumWidth) * 100;
            float currentWidth = Mathf.Clamp(leftHealthPercent, 0, 100);
            return currentWidth; }
    }

    public float CurrentHealth
    {
        set
        {
            currentHealth = value;
        }
    }

    public float CalibrationHealth
    {
        get
        {
            return calibrationHealth;
        }
    }
    public event Action<float> CastleRegenerationStarted = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            RegenerateHealth(1500);
        }
    }


    public void DealDamage(float damage)
    {
        
        currentHealth -= damage;
        float leftHealthPercent = ((currentHealth - damage) / maximumWidth) * 100;
        //Debug.Log(leftHealthPercent);
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        UpdateCastleHealth(leftHealthPercent);
    }

    void UpdateCastleHealth(float healthPercent)
    {
        int updatedWidth = (int) (healthPercent * maximumWidth / 100);
        transform.Find("Borders").Find("Foreground").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
    }

    public void RegenerateHealth(float health)
    {
        calibrationHealth = currentHealth;
        currentHealth += health;
        
        float leftHealthPercent = ((currentHealth) / maximumWidth) * 100;
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        if (CastleRegenerationStarted != null) { CastleRegenerationStarted(leftHealthPercent); }
    }
}
