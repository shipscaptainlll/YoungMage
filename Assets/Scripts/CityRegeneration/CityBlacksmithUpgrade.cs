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

    [Header("Roates to inner elements")]
    [SerializeField] Scrollbar upgradesBar;
    [SerializeField] Transform contentHolder;
    [SerializeField] Text goldText;
    
    int upgradesMaxCount;
    int upgradeCurrentCount;
    float offsetUpgrade;

    int upgradeCost;
    float costModifier;

    bool coroutineIsRunning;
    Coroutine scrollingCoroutine;

    int countUpgradesQuests;
    public int CountUpgradesQuests { get { return countUpgradesQuests; } }
    public event Action<int> BlacksmithUpgradedQuests = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        upgradeCurrentCount = 1;
        offsetUpgrade = 0.075f;
        upgradeCost = 100;
        costModifier = 2.5f;
        upgradesMaxCount = contentHolder.childCount - 4;
        upgradesBar.value = ((float)(upgradeCurrentCount) / (float)(upgradesMaxCount+2.5f)) + offsetUpgrade;
        Debug.Log(upgradesBar.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeBlacksmith()
    {
        if (upgradeCurrentCount < upgradesMaxCount)
        {
            if (TakeUpgradeMoney())
            {
                suiNotificator.Notify("-" + upgradeCost);
                upgradeCurrentCount++;
                InitiateScrolling();
                
                UpgradeCost();
                UpdateCostText();
            } else
            {
                suiNotificator.Notify("not enough gold");
            }
        } else
        {
            suiNotificator.Notify("already maxed level");
        }
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
        upgradeCost = (int) (upgradeCost * costModifier);
    }

    void UpdateCostText()
    {
        goldText.text = upgradeCost.ToString();
        if (upgradeCurrentCount == upgradesMaxCount) { goldText.text = "MAX"; }
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
}
