using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPortalActivator : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] private LearningSkeletonsCatching m_learningSkeletonsCatching;
    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private CastleLookCatcher m_castleLookCatcher;
    bool insidePortal;

    public bool InsidePortal { get { return insidePortal; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            insidePortal = true;
            if (m_learningSkeletonsCatching.NextStep == 1)
            {
                m_learningSkeletonsCatching.ShowNextStep();
            }
            //particleSystem.Play();
            //Debug.Log("character entered");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            insidePortal = true;
            if (m_learningSkeletonsCatching.NextStep == 2  && m_cameraController.ObservedObject.transform != null && m_cameraController.ObservedObject.transform.GetComponent<CastleLookCatcher>() != null)
            {
                m_learningSkeletonsCatching.ShowNextStep();
                Destroy(m_castleLookCatcher.gameObject);
            }
            //Debug.Log("character stays");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            insidePortal = false;
            //particleSystem.Stop();
            //Debug.Log("character got out");
        }
    }
}
