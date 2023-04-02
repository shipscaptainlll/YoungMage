using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationBaseObjectsBehavior : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private ParticleSystem m_shiningPS;
    [SerializeField] private ParticleSystem m_destructionSparklesPS;
    [SerializeField] private ParticleSystem m_destructionPS;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void ActivateFloating()
    {
        m_animator.enabled = true;
        
    }

    public void ActivateShining()
    {
        m_shiningPS.Play();
    }

    public void HideObject()
    {
        m_animator.enabled = false;
        
        m_shiningPS.gameObject.SetActive(false);
        m_destructionSparklesPS.gameObject.SetActive(false);
        m_destructionPS.gameObject.SetActive(false);

        StartCoroutine(SetOffAfterDelay(3));
    }

    public void ActivateDecomposition()
    {
        m_shiningPS.gameObject.SetActive(false);
        
        m_destructionSparklesPS.gameObject.SetActive(true);
        m_destructionPS.gameObject.SetActive(true);
        
        m_destructionSparklesPS.Play();
        m_destructionPS.Play();
    }

    IEnumerator SetOffAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
