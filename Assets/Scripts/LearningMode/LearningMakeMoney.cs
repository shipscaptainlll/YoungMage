using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningMakeMoney : MonoBehaviour, ILearningQuest
{
    [SerializeField] private int m_questId;
    [SerializeField] private LearningPopupsInstantiator m_learningPopupsInstantiator;
    [SerializeField] private string m_popUpText;

    [SerializeField] private Transform  m_questsHolder;
    [SerializeField] private TutorialsInstantiator m_tutorialsInstantiator;

    [SerializeField] private Transform m_checkboxesHolder;
    
    [Header("Steps invokers")]
    [SerializeField] private MidasCollectorCatcher m_midasCollectorCatcher;
    [SerializeField] private CollectGoldCoinsTrigger m_collectGoldCoinsTrigger;
    [SerializeField] private DropHandler m_dropHandler;
    [SerializeField] private QuickAccessHandController m_quickAccessHandController;
    [SerializeField] private Transform m_dragHandlersHolder;
    
    [SerializeField] private SoundManager m_soundManager;
    
    private int m_nextStep;

    public int NextStep => m_nextStep;
    public int questID
    {
        get => m_questId;
        set => m_questId = value;
    }

    public void ActivateQuestSequence()
    {
        ShowNextStep();
    }

    public void DeactivateQuestSequence()
    {
        LearningModeFlow.TryInitiateNextTutorial();
    }

    public void ShowNextStep()
    {
        if (m_nextStep == 0)
        {
            m_questsHolder.GetChild(0).GetComponent<Animator>().Play("PanelAppear");
        } else if (m_nextStep == 1)
        {
            foreach (Transform row in m_dragHandlersHolder)
            {
                foreach (Transform element in row)
                {
                    element.Find("Borders").Find("Element").GetComponent<DragHandler>().QuickAccessElementFilled += ShowNextStep;
                }
            }
            m_dropHandler.QuickAccessElementFilled += ShowNextStep;
            m_checkboxesHolder.GetChild(0).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
            m_soundManager.Play("QuestUIAppear");
        } else if (m_nextStep == 2)
        {
            foreach (Transform row in m_dragHandlersHolder)
            {
                foreach (Transform element in row)
                {
                    element.Find("Borders").Find("Element").GetComponent<DragHandler>().QuickAccessElementFilled -= ShowNextStep;
                }
            }
            m_dropHandler.QuickAccessElementFilled -= ShowNextStep;
            m_quickAccessHandController.ObjectHandsChanged += CheckStoneOreEquipped;
            m_checkboxesHolder.GetChild(1).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
            m_soundManager.Play("QuestUIAppear");
        }  else if (m_nextStep == 3)
        {
            m_quickAccessHandController.ObjectHandsChanged -= CheckStoneOreEquipped;
            m_midasCollectorCatcher.ResourceEnteredCollectorLearningTutorial += ShowNextStep;
            m_checkboxesHolder.GetChild(2).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
            m_soundManager.Play("QuestUIAppear");
        } else if (m_nextStep == 4)
        {
            m_midasCollectorCatcher.ResourceEnteredCollectorLearningTutorial -= ShowNextStep;
            m_collectGoldCoinsTrigger.GoldCollected += ShowNextStep;
            m_checkboxesHolder.GetChild(3).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
            m_soundManager.Play("QuestUIAppear");
        } else if (m_nextStep == 5)
        {
            m_collectGoldCoinsTrigger.GoldCollected -= ShowNextStep;
            m_checkboxesHolder.GetChild(4).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
            m_soundManager.Play("QuestUICompleted");
            m_questsHolder.GetChild(0).GetComponent<Animator>().Play("PanelDisappear");
            m_learningPopupsInstantiator.ActivatePopup(m_popUpText);
            //m_tutorialsInstantiator.ActivateTutorial(10);
            DeactivateQuestSequence();
        } 

        m_nextStep++;
    }
    
    public void CheckStoneOreEquipped()
    {
        if (m_quickAccessHandController.CurrentCustomID == 2)
        {
            ShowNextStep();
        }
    }
    
    IEnumerator ShowGraduallyTransform(Transform targetTransform, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0;
        float targetElapsed = 1;
        float currentCanvasAlpha = 0;
        CanvasGroup targetCanvasGroup = targetTransform.GetComponent<CanvasGroup>();
        while (elapsed < targetElapsed)
        {
            elapsed += Time.deltaTime;
            currentCanvasAlpha = Mathf.Lerp(0, 1, elapsed / targetElapsed);
            targetCanvasGroup.alpha = currentCanvasAlpha;
            yield return null;
        }
        targetCanvasGroup.alpha = 0;
        yield return null;
    }
    
    IEnumerator HideGraduallyTransform(Transform targetTransform, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0;
        float targetElapsed = 1;
        float currentCanvasAlpha = 1;
        CanvasGroup targetCanvasGroup = targetTransform.GetComponent<CanvasGroup>();
        while (elapsed < targetElapsed)
        {
            elapsed += Time.deltaTime;
            currentCanvasAlpha = Mathf.Lerp(1, 0, elapsed / targetElapsed);
            targetCanvasGroup.alpha = currentCanvasAlpha;
            yield return null;
        }
        targetCanvasGroup.alpha = 0;
        yield return null;
    }
    
}
