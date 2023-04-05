using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmutationDesintegrationNotificator : MonoBehaviour
{
    [SerializeField] private Transform m_popupExample;
    [SerializeField] private Animator m_popupAnimator;
    [SerializeField] private Transform m_popupShowPosition;
    [SerializeField] private Transform m_popupHidePosition;
    [SerializeField] private Text m_popupText;
    private bool m_popUpActive;
    
    public void ActivatePopup(string text)
    {
        if (m_popUpActive)
        {
            StopAllCoroutines();
        }
        m_popupExample.GetComponent<CanvasGroup>().alpha = 1;
        m_popupExample.position = m_popupShowPosition.position;
        m_popupAnimator.enabled = true;
        m_popupAnimator.Play("PopupFast");
        m_popupText.text = text;
        m_popUpActive = true;
        
        StartCoroutine(HidePopup());
    }
    
    public void ActivatePopup()
    {
        if (m_popUpActive)
        {
            StopAllCoroutines();
        }
        m_popupExample.GetComponent<CanvasGroup>().alpha = 1;
        m_popupExample.position = m_popupShowPosition.position;
        m_popupAnimator.enabled = true;
        m_popupAnimator.Play("PopupFast");
        m_popUpActive = true;
        
        StartCoroutine(HidePopup());
    }

    IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(3f);
        m_popupExample.GetComponent<CanvasGroup>().alpha = 0;
        m_popupExample.position = m_popupHidePosition.position;
        m_popUpActive = false;
    }
}
