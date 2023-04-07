using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TransmutationBaseObjectsBehavior : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private ParticleSystem m_shiningPS;
    [SerializeField] private ParticleSystem m_destructionSparklesPS;
    [SerializeField] private VisualEffect m_destructionVFX;
    [SerializeField] private int objectId;
    [SerializeField] private DestroyableObjects m_destroyableObjects;
    private MeshRenderer m_meshRenderer;
    
    
    private GameObject m_destroyableObject;
    private AudioSource m_audioSource;
    [SerializeField] private SoundManager m_soundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        m_meshRenderer = transform.GetComponent<MeshRenderer>();
    }
    
    public void ActivateFloating()
    {
        m_animator.enabled = true;
        m_meshRenderer.enabled = true;
    }

    public void ActivateShining()
    {
        m_shiningPS.Play();
    }

    public void DeactivateShining()
    {
        m_shiningPS.Stop();
        m_shiningPS.gameObject.SetActive(false);
        m_shiningPS.gameObject.SetActive(true);
    }

    public void HideObject()
    {
        m_animator.enabled = false;
        
        //m_shiningPS.gameObject.SetActive(false);
        m_destructionSparklesPS.gameObject.SetActive(false);
        m_destructionVFX.gameObject.SetActive(false);
        m_meshRenderer.enabled = false;
        StartCoroutine(SetOffAfterDelay(0));
    }

    public void ActivateDecomposition()
    {
        //m_shiningPS.gameObject.SetActive(false);
        
        m_destructionSparklesPS.gameObject.SetActive(true);
        m_destructionVFX.gameObject.SetActive(true);
        
        m_destructionSparklesPS.Play();
        m_destructionVFX.Play();
        
        m_destroyableObject = Instantiate(m_destroyableObjects.TakeObject(objectId), transform.position, transform.rotation);
        
        m_audioSource = m_soundManager.LocateAudioSource("RockoreDestruction", m_destroyableObject.transform);
        m_audioSource.Play();
        m_destroyableObject.transform.position = transform.position;
        m_destroyableObject.AddComponent<DestroyableObject>();
        
        m_meshRenderer.enabled = false;
    }

    IEnumerator SetOffAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        
        //gameObject.SetActive(false);
    }
}
