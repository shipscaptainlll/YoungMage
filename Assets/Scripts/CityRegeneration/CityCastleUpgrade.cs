using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityCastleUpgrade : MonoBehaviour
{
    [Header("Attached main scripts")]
    [SerializeField] GoldCoinsCounter goldCoinsCounter;

    [Header("Roates to inner elements")]
    [SerializeField] Scrollbar upgradesBar;
    [SerializeField] Transform contentHolder;
    [SerializeField] Transform countButtonsHolder;
    [SerializeField] Transform fillShardsHolder;
    [SerializeField] Text sphereGoldText;
    [SerializeField] Text countGoldText;
    [SerializeField] Text fillGoldText;
    [SerializeField] Text fillText;


    int sphereUpgradesMaxCount;
    int sphereUpgradeCurrentCount;
    float offsetUpgrade;

    int countUpgradesMaxCount;
    int countUpgradeCurrentCount;

    int sphereUpgradeCost;
    float sphereCostModifier;

    int countUpgradeCost;
    float countcostModifier;

    int fillCost;
    int fillAmmount;

    List<Transform> availableButtons;
    List<Transform> availableShards;
    Transform currentShard;
    bool coroutineIsRunning;
    Coroutine scrollingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        sphereUpgradeCurrentCount = 1;
        countUpgradeCurrentCount = 1;
        offsetUpgrade = 0.075f;
        sphereUpgradeCost = 100;
        sphereCostModifier = 2.5f;
        countUpgradeCost = 1000;
        countcostModifier = 2f;
        sphereUpgradesMaxCount = contentHolder.childCount - 4;
        countUpgradesMaxCount = 4;
        foreach (Transform button in countButtonsHolder)
        {
            availableButtons.Add(button);
        }
        foreach (Transform shard in fillShardsHolder)
        {
            availableShards.Add(shard);
        }
        upgradesBar.value = ((float)(sphereUpgradeCurrentCount) / (float)(sphereUpgradesMaxCount + 2.5f)) + offsetUpgrade;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeSpheresEnergy()
    {
        Debug.Log("there hello if");
        if (sphereUpgradeCurrentCount < sphereUpgradesMaxCount)
        {
            if (TakeUpgradeMoney(sphereUpgradeCost))
            {
                sphereUpgradeCurrentCount++;
                InitiateScrolling();

                UpgradeCost(sphereUpgradeCost, sphereCostModifier);
                UpdateCostText(sphereGoldText, sphereUpgradeCost, sphereUpgradeCurrentCount, sphereUpgradesMaxCount);
            }
        }
    }

    public void UpgradeSpheresCount()
    {
        if (countUpgradeCurrentCount < countUpgradesMaxCount)
        {
            if (TakeUpgradeMoney(countUpgradeCost))
            {
                countUpgradeCurrentCount++;
                availableShards[countUpgradeCurrentCount - 1].GetComponent<CanvasGroup>().alpha = 1;
                ShowNextButton();
                ShowNextShard();
                UpgradeCost(countUpgradeCost, countcostModifier);
                UpdateCostText(countGoldText, countUpgradeCost, countUpgradeCurrentCount, countUpgradesMaxCount);
            }
        }
    }

    bool TakeUpgradeMoney(int upgradeCost)
    {
        if (goldCoinsCounter.Count <= upgradeCost)
        {
            goldCoinsCounter.AddResource(-(int)upgradeCost);
            return true;
        }
        return false;
    }

    void UpgradeCost(float upgradeCost, float costModifier)
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

    void ShowNextButton()
    {
        var color = availableButtons[countUpgradeCurrentCount].GetComponent<Image>().color;
        color.a = 1f;
        availableButtons[countUpgradeCurrentCount].GetComponent<Image>().color = color;
        if (countUpgradeCurrentCount != countUpgradesMaxCount)
        {
            color = availableButtons[countUpgradeCurrentCount + 1].GetComponent<Image>().color;
            color.a = 0.25f;
            availableButtons[countUpgradeCurrentCount + 1].GetComponent<Image>().color = color;
            availableButtons[countUpgradeCurrentCount + 1].GetComponent<Button>().enabled = true;
        }
    }

    void ShowNextShard()
    {
        availableShards[countUpgradeCurrentCount - 1].GetComponent<CanvasGroup>().alpha = 1;
    }
}
