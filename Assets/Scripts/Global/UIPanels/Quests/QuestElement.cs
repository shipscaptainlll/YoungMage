using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class QuestElement : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField] QuestsDatabase questDatabase;
    [SerializeField] QuestsSubscriptionsManager subscriptionsManager;
    [SerializeField] Text descriptionText;
    [SerializeField] Text rewardText;
    [SerializeField] Text goalText;
    [SerializeField] Image progressImage;
    [SerializeField] Button questFinishingButton;
    [SerializeField] Color normalQuestColor;
    [SerializeField] Color completedQuestColor;
    

    Coroutine smoothIncreasement;
    float iteratedPercent;
    float increasementLeft;
    float lerpValue;
    bool isHoldingQuest;

    string description;
    int goal;
    int reward;
    int level;
    int id;

    float progressValue;
    bool isCompleted;


    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Goal
    {
        get { return goal; }
        set { goal = value; }
    }

    public int Reward
    {
        get { return reward; }
        set { reward = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public float ProgressValue
    {
        get { return progressValue; }
        set { progressValue = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isHoldingQuest = false;
    }


    public void QuestElementUpdate()
    {
        //Debug.Log(!isHoldingQuest + " " + !isCompleted + " " + transform.parent);
        //Debug.Log("we arehere " + transform + " " + transform.parent);
        if (!isHoldingQuest && !isCompleted)
        {
            //Debug.Log("hello there");
            isHoldingQuest = true;
            UploadNextQuest();
            if (isHoldingQuest)
            {
                subscriptionsManager.SubscribeOnProgress(this.GetComponent<QuestElement>());
                //Debug.Log("there");
                SynchronizeUI();
                ShowEffects();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddProgress();
        }
    }

    public void FinishQuest()
    {
        if (isCompleted)
        {
            //Debug.Log(1);
            isHoldingQuest = false;
            isCompleted = false;
            UpdateQuestsDatabase();
            subscriptionsManager.UnsubscribeFromProgress(this.GetComponent<QuestElement>());
            //Debug.Log(2);
            //SynchronizeUI();
            //Debug.Log(3);
            VisualizeQuestUncompleted();
            //Debug.Log(4);
            ShowEffects();
            
            //UploadNextQuest();
        }
        
    }

    void UpdateQuestsDatabase()
    {
        List<Quest> questsList = questDatabase.Quests;
        var requiredQuest = questsList.Find(item => item.Id == id);
        level++;
        requiredQuest.Level = level;
        questDatabase.TryReuploadQuest(id);
    }

    void UploadNextQuest()
    {
        //Debug.Log(questDatabase.Quests.Count + " " + questDatabase.activeQuestsIndexesSell.Count);
        if (questDatabase.Quests.Count > (questDatabase.activeQuestsIndexesSell.Count))
        {
            progressValue = 0;
            this.GetComponent<CanvasGroup>().alpha = 1;
            
            List<Quest> newList = questDatabase.Quests.Except(questDatabase.activeQuestsIndexesSell).ToList();
            foreach (Quest quest in questDatabase.Quests)
            {
                //Debug.Log(quest.Description);
            }
            foreach(Quest quest in questDatabase.activeQuestsIndexesSell)
            {
                //Debug.Log(quest.Description);
            }
            foreach (Quest quest in newList)
            {
                //Debug.Log(quest.Description);
            }
            int randomNumber = Random.Range(0, newList.Count - 1);
            //Debug.Log(randomNumber);
            //Debug.Log(newList.Count);
            //Debug.Log(newList[randomNumber]);
            /*
            while (questDatabase.activeQuestsIndexesSell.Contains(newList[randomNumber]))
            {
                helloThere++;

                randomNumber = Random.Range(0, newList.Count);
                randomQuestIndex = availableQuestsIndexes[randomNumber];
                if (helloThere > 1 ) { Debug.Log("Something went wrong"); return; }
            }
            */

            questDatabase.activeQuestsIndexesSell.Add(newList[randomNumber]);
            var newQuest = newList[randomNumber];
            //Debug.Log(newQuest);
            //Debug.Log(newQuest.Description);
            //Debug.Log(newQuest.Goal);
            if (newQuest.Level != 1)
            {
                newQuest.Goal = newQuest.Goal;
                newQuest.Reward = newQuest.Reward;
            }

            this.Description = newQuest.Description + " " + level;
            this.Goal = newQuest.Goal;
            this.Reward = newQuest.Reward;
            this.Level = newQuest.Level;
            this.Id = newQuest.Id;
            return;
        }
        isHoldingQuest = false;
        isCompleted = false;
        this.GetComponent<CanvasGroup>().alpha = 0;
    }  

    void SynchronizeUI()
    {
        UpdateDescription();
        UpdateGoal();
        UpdateProgressbar();
    }

    void UpdateDescription()
    {
        descriptionText.text = description;
        rewardText.text = reward.ToString();
    }

    void UpdateGoal()
    {
        goalText.text = (int) progressValue + "/" + goal;
    }

    void UpdateProgressbar()
    {
        float progressPercent = (float) ((float) progressValue / (float) goal);
        decimal progress = (decimal)(progressPercent);
        progressImage.fillAmount = (float) progress;
        if (progressPercent >= 1 && !isCompleted)
        {
            progressPercent = 1;
            isCompleted = true;
            VisualizeQuestCompleted();
            questDatabase.RecordCompleted(Id);
        }
    }

    void VisualizeQuestUncompleted()
    {
        transform.GetComponent<Image>().color = normalQuestColor;
    }

    void VisualizeQuestCompleted()
    {
        transform.GetComponent<Image>().color = completedQuestColor;
    }

    void ShowEffects()
    {
        //Debug.Log("Effects!");
    }

    void AddProgress()
    {
        if (progressValue < goal)
        {
            progressValue++;
            
            if (smoothIncreasement == null)
            {
                smoothIncreasement = StartCoroutine(SmoothIncrease());
            } else
            {
                StopCoroutine(smoothIncreasement);
                smoothIncreasement = null;
                progressValue = lerpValue * goal + 1;
                smoothIncreasement = StartCoroutine(SmoothIncrease());
            }
            UpdateGoal();
            UpdateProgressbar();
        } else
        {
            questFinishingButton.enabled = true;
        }
    }

    public void UpdateProgress(int newProgress)
    {
        if (progressValue < goal)
        {
            if (newProgress > goal) { progressValue = goal; } else { progressValue = newProgress; }

            if (smoothIncreasement == null)
            {
                smoothIncreasement = StartCoroutine(SmoothIncrease());
            }
            else
            {
                StopCoroutine(smoothIncreasement);
                smoothIncreasement = null;
                progressValue = lerpValue * goal + 1;
                smoothIncreasement = StartCoroutine(SmoothIncrease());
            }
            UpdateGoal();
            UpdateProgressbar();
        }
        else
        {
            questFinishingButton.enabled = true;
        }
    }

    IEnumerator SmoothIncrease()
    {
        float elapsed = 0;
        float increasementTime = 0.23f;
        float progressPercent = (float)((float)(progressValue - 1) / (float)goal);
        iteratedPercent = (float)((float)(progressValue)/ (float)goal);
        
        while (elapsed < increasementTime)
        {
            
            elapsed += Time.deltaTime;
            lerpValue = Mathf.Lerp(progressPercent, iteratedPercent, elapsed / increasementTime);
            //Debug.Log(transform + " " + lerpValue);
            progressImage.fillAmount = lerpValue;
            yield return null;
        }
        
        progressImage.fillAmount = iteratedPercent;
        smoothIncreasement = null;
        yield return null;
    }
}
