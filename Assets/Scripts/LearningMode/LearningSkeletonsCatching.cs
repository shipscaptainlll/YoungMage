using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LearningSkeletonsCatching : MonoBehaviour, ILearningQuest
{
    [SerializeField] private int m_questId;
    [SerializeField] private LearningPopupsInstantiator m_learningPopupsInstantiator;

    [SerializeField] private Transform  m_questsHolder;
    [SerializeField] private TutorialsInstantiator m_tutorialsInstantiator;

    [SerializeField] private Transform m_checkboxesHolder;
    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private PortalOpener m_portalOpener;
    [SerializeField] private Portal2 m_portal;
    
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

    public static void DeactivateQuestSequence()
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
            m_checkboxesHolder.GetChild(0).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
        } else if (m_nextStep == 2)
        {
            m_portalOpener.PortalJustOpened += ShowNextStep;
            //m_tutorialsInstantiator.ActivateTutorial(2);
            m_checkboxesHolder.GetChild(1).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
        } else if (m_nextStep == 3)
        {
            m_portalOpener.PortalJustOpened -= ShowNextStep;
            m_portal.SkeletonWasFound += ShowNextStep;
            m_checkboxesHolder.GetChild(2).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
            
            
        } else if (m_nextStep == 4)
        {
            m_portal.SkeletonWasFound -= ShowNextStep;
            m_checkboxesHolder.GetChild(3).GetComponent<LearningCheckboxBehavior>().MarkCheckboxFinished();
            
            m_questsHolder.GetChild(0).GetComponent<Animator>().Play("PanelDisappear");
            
            m_learningPopupsInstantiator.ActivatePopup("woooow1");
            
            
            
            DeactivateQuestSequence();
        } 

        m_nextStep++;
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
