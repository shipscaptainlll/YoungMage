using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningScreen : MonoBehaviour, ILearningQuest
{
    [SerializeField] private int m_questId;

    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private PersonMovement m_personMovement;
    [SerializeField] private SavePanel m_savePanel;
    [SerializeField] private ClickManager m_clickManager;
    [SerializeField] private PanelsManager m_panelsManager;

    [SerializeField] private Transform m_LearningTransform;
    [SerializeField] private Transform m_LearningTextsHolder;
    [SerializeField] private Transform m_LearningCursor;
    [SerializeField] private Animator m_LearningCursorAnimator;

    [SerializeField] private TutorialInvoker m_tutorialInvoker;
    
    private int m_nextStep;

    public int questID
    {
        get => m_questId;
        set => m_questId = value;
    }

    public void ActivateQuestSequence()
    {
        DisableInterfacesElements();
        ShowNextStep();
        m_clickManager.LMBClicked += ShowNextStep;
    }

    public void DeactivateQuestSequence()
    {
        EnableInterfacesElements();
        m_clickManager.LMBClicked -= ShowNextStep;
        
    }

    public void ShowNextStep()
    {
        if (m_nextStep == 0) {
            m_LearningCursor.gameObject.SetActive(true);
            m_LearningTransform.GetComponent<CanvasGroup>().alpha = 1;
            m_LearningCursor.GetComponent<CanvasGroup>().alpha = 1;
            m_LearningTextsHolder.GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
            m_LearningCursorAnimator.Play("CastleHealthArrow");
        } else if (m_nextStep == 1)
        {
            m_LearningTextsHolder.GetChild(1).GetComponent<CanvasGroup>().alpha = 0;
            m_LearningTextsHolder.GetChild(2).GetComponent<CanvasGroup>().alpha = 1;
            m_LearningCursorAnimator.Play("HealthArrow");
        } else if (m_nextStep == 2)
        {
            m_LearningTextsHolder.GetChild(2).GetComponent<CanvasGroup>().alpha = 0;
            m_LearningTextsHolder.GetChild(3).GetComponent<CanvasGroup>().alpha = 1;
            m_LearningCursorAnimator.Play("CrystalsArrow");
        } else if (m_nextStep == 3)
        {
            m_LearningTextsHolder.GetChild(3).GetComponent<CanvasGroup>().alpha = 0;
            m_LearningTextsHolder.GetChild(4).GetComponent<CanvasGroup>().alpha = 1;
            
            m_LearningCursorAnimator.Play("GoldArrow");
        } else if (m_nextStep == 4)
        {
            m_LearningTextsHolder.GetChild(4).GetComponent<CanvasGroup>().alpha = 0;
            m_LearningCursor.gameObject.SetActive(false);
            
            StartCoroutine(HideGraduallyTransform());
            m_LearningCursor.GetComponent<CanvasGroup>().alpha = 0;
            DeactivateQuestSequence();
            m_tutorialInvoker.InvokeTutorial();
        }

        m_nextStep++;
    }

    void DisableInterfacesElements()
    {
        m_cameraController.TutorialModeActivated = true;
        m_personMovement.TutorialModeActivated = true;
        m_savePanel.IsTutorialMode = true; //covers both saving and loading
        m_panelsManager.TutorialMode = true;
    }
    
    void EnableInterfacesElements()
    {
        m_cameraController.TutorialModeActivated = false;
        m_personMovement.TutorialModeActivated = false;
        m_savePanel.IsTutorialMode = false; //covers both saving and loading
        m_panelsManager.TutorialMode = false;
    }

    IEnumerator HideGraduallyTransform()
    {
        float elapsed = 0;
        float targetElapsed = 1;
        float currentCanvasAlpha = 2;
        CanvasGroup targetCanvasGroup = m_LearningTransform.GetComponent<CanvasGroup>();
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
