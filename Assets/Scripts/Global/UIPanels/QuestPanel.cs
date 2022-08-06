using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] GoldCoinsCounter goldCoinsCounter;

    [SerializeField] Transform typeSubpanel;
    [SerializeField] Transform addTypeSubpanel;
    [SerializeField] Transform subpanelsHolder;
    [SerializeField] Transform questPanel;
    [SerializeField] Transform questPanelHolder;

    QuestElement currentClosedQuest;
    int questSubpanelsCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void FinishQuest(QuestElement questElement)
    {
        currentClosedQuest = questElement;

        GetReward();
        UpgradeQuestLevel();
        CloseQuest();
    }

    void GetReward()
    {
        int reward = currentClosedQuest.Reward;
        goldCoinsCounter.AddResource(reward);
    }

    void UpgradeQuestLevel()
    {
        currentClosedQuest.Level += 1;
    }

    void CloseQuest()
    {
        Debug.Log("Quest is closed");
        currentClosedQuest = null;
    }

    public void AddQuestSubpanel()
    {
        questSubpanelsCount++;
        Transform newPanel = Instantiate(typeSubpanel, subpanelsHolder.position, subpanelsHolder.rotation);
        newPanel.parent = subpanelsHolder;
        newPanel.localScale = typeSubpanel.localScale;
        newPanel.name = "Quest Supanel " + questSubpanelsCount;
        newPanel.Find("Content").Find("TypeName").Find("Text").GetComponent<Text>().text
            = "Slot " + questSubpanelsCount;
        newPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(typeSubpanel.GetComponent<RectTransform>().rect.width, 
            typeSubpanel.GetComponent<RectTransform>().rect.height);
        newPanel.SetAsLastSibling();
        addTypeSubpanel.SetAsLastSibling();
        

        AddQuestPanel();
    }

    void AddQuestPanel()
    {
        Transform newPanel = Instantiate(questPanel, questPanelHolder.position, questPanelHolder.rotation);
        newPanel.parent = questPanelHolder;
        newPanel.localScale = questPanel.localScale;
        foreach (Transform horizontalRow in newPanel.Find("Rows"))
        {
            foreach (Transform quest in horizontalRow)
            {
                quest.GetComponent<RectTransform>().sizeDelta = new Vector2(92, 55);
            }
        }
        newPanel.GetComponent<CanvasGroup>().alpha = 0;
        newPanel.name = "Quest Panel " + questSubpanelsCount;
        newPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(questPanel.GetComponent<RectTransform>().rect.width,
            questPanel.GetComponent<RectTransform>().rect.height);
        newPanel.SetAsLastSibling();
        addTypeSubpanel.SetAsLastSibling();
    }

    public void ChangePanel(Transform questSubpanel)
    {
        string digitInName = Regex.Match(questSubpanel.name, @"\d+").Value;
        int questSubpanelIndex = Int32.Parse(digitInName);
        Debug.Log("opening " + questSubpanelIndex);

        foreach (Transform questPanel in questPanelHolder)
        {
            string digitQuestpanelName = Regex.Match(questPanel.name, @"\d+").Value;
            int questPanelIndex = Int32.Parse(digitQuestpanelName);
            
            if (questPanelIndex == questSubpanelIndex)
            {
                questPanelHolder.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                questPanel.GetComponent<CanvasGroup>().alpha = 1;
                questPanel.SetAsFirstSibling();
                
                Debug.Log(questSubpanel + " matches with " + questPanel);
            }
        }
    }
}
