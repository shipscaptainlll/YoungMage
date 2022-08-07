using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreHealthDecreaser : MonoBehaviour
{

    float minimalWidth = 0;
    float maximumWidth = 1000;
    float maximumHealth = 1000;
    float currentHealth = 1000;

    RectTransform healthTransform;
    public event Action HealthReachedZero = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        healthTransform = transform.Find("Borders").Find("Foreground").GetComponent<RectTransform>();
    }

    private void Update()
    {
        
    }

    public void DealDamage(float damage)
    {

        currentHealth -= damage;
        float leftHealthPercent = ((currentHealth - damage) / maximumWidth) * 100;
        //Debug.Log(leftHealthPercent);
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        int updatedWidth = (int)(leftHealthPercent * maximumWidth / 100);
        StartCoroutine(SmoothHealthDecrease(updatedWidth));
    }

    IEnumerator SmoothHealthDecrease(float updatedWidth)
    {
        float counter = 0;
        float smoothingDuration = 0.15f;
        float initialWidth = healthTransform.rect.width;
        float currentWidth = initialWidth;
        while(counter < smoothingDuration)
        {
            counter += Time.deltaTime;
            currentWidth = Mathf.Lerp(initialWidth, updatedWidth, counter / smoothingDuration);
            //Debug.Log(currentWidth);
            healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }
        healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
        if (currentHealth <= 0)
        {
            RefillHealth();
            if (HealthReachedZero != null) { HealthReachedZero(); }
        }
        yield return null;
    }

    void RefillHealth()
    {
        currentHealth = maximumHealth;
        StartCoroutine(SmoothHealthIncrease(maximumWidth));
    }

    IEnumerator SmoothHealthIncrease(float updatedWidth)
    {
        float counter = 0;
        float smoothingDuration = 0.15f;
        float initialWidth = healthTransform.rect.width;
        float currentWidth = initialWidth;
        while (counter < smoothingDuration)
        {
            counter += Time.deltaTime;
            currentWidth = Mathf.Lerp(0, updatedWidth, counter / smoothingDuration);
            //Debug.Log(currentWidth);
            healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }
        healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
        yield return null;
    }

    void UpdateOreHealth(float healthPercent)
    {
        
    }
}
