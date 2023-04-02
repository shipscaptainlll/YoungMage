using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmutationErrorsNotificator : MonoBehaviour
{
    [SerializeField] private Transform m_popupExample;
    [SerializeField] private Animator m_popupAnimator;
    [SerializeField] private Transform m_popupShowPosition;
    [SerializeField] private Transform m_popupHidePosition;
    [SerializeField] private Text m_popupText;
    
    public void ActivatePopup(string text)
    {
        m_popupExample.GetComponent<CanvasGroup>().alpha = 1;
        m_popupExample.position = m_popupShowPosition.position;
        m_popupAnimator.enabled = true;
        m_popupAnimator.Play("QuestPopupShow");
        m_popupText.text = text;
        StartCoroutine(HidePopup());
    }
    
    public void ActivatePopup()
    {
        m_popupExample.GetComponent<CanvasGroup>().alpha = 1;
        m_popupExample.position = m_popupShowPosition.position;
        m_popupAnimator.enabled = true;
        m_popupAnimator.Play("QuestPopupShow");
        StartCoroutine(HidePopup());
    }

    IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(4f);
        m_popupExample.GetComponent<CanvasGroup>().alpha = 0;
        m_popupExample.position = m_popupHidePosition.position;
    }
}
