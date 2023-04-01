using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCheckboxBehavior : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    
    
    
    public void MarkCheckboxFinished()
    {
        m_animator.enabled = true;
        m_animator.Play("CheckboxAppear");
        //transform.GetComponent<CanvasGroup>().alpha = 1;
    }
    
}
