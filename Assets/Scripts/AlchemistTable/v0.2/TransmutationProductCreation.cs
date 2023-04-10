using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class TransmutationProductCreation : MonoBehaviour
{
    [SerializeField] private TransmutationProcessing m_transmutationProcessing;
    [SerializeField] private Transform m_targetPosition;
    [SerializeField] private Transform m_elementsHolder;
    [SerializeField] private Transform m_potentialProductsTransform;

    [SerializeField] private VisualEffect m_productCreationVFX;
    [SerializeField] private VisualEffect m_productAppearVFX;
    [SerializeField] private PotentialProductLibrary m_potentialProductLibrary;
    [SerializeField] private SoundManager m_soundManager;
    [SerializeField] private TransmutationElementsManager m_transmutationElementsManager;
    [SerializeField] private TransmutationHandController m_transmutationHandController;
    [SerializeField] private TransmutationRecipesPanel m_transmutationRecipesPanel;
    private AudioSource m_visualisationSound;
    private int m_currentProductID;
    private Transform m_currentProductTransform;
    private List<Transform> EffectList = new List<Transform>();
    private List<Vector3> EffectsStartPositions = new List<Vector3>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform element in m_elementsHolder)
        {
            EffectList.Add(element.Find("Effects").Find("ParticleStrip").transform);
            EffectsStartPositions.Add(element.Find("Effects").Find("ParticleStrip").transform.position);
        }
        
        m_visualisationSound = m_soundManager.LocateAudioSource("TransmutationPotentialProduct", transform);
    }

    public void StartProcessing()
    {
        foreach (Transform element in EffectList)
        {
            element.position = m_targetPosition.position;
        }
        StartCoroutine(DelayThenFinish());
    }

    IEnumerator DelayThenFinish()
    {
        m_transmutationHandController.ShowHandProcessing();
        yield return new WaitForSeconds(1.5f);
        m_productCreationVFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        AppearProduct();
        m_transmutationHandController.HideHand();
        if (m_currentProductID != 0)
        {
            yield return new WaitForSeconds(3.5f);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }
        
        FinishProcessing();
    }

    public void AppearProduct()
    {
        int indexer = 0;
        foreach (Transform element in EffectList)
        {
            element.gameObject.SetActive(false);
            element.position = EffectsStartPositions[indexer];
            indexer++;
        }

        GetProductInstance();
        
        m_productCreationVFX.gameObject.SetActive(false);
        m_productAppearVFX.gameObject.SetActive(true);
        m_productAppearVFX.SendEvent("CharacterMoved");
        
        if (m_currentProductID != 0)
        {
            m_visualisationSound.Play();
            if (!m_transmutationRecipesPanel.ActivatedRecipesDictionary[m_currentProductID])
            {
                m_transmutationRecipesPanel.UpdateRecipesDictionary(m_currentProductID);
            }
            //m_currentProductTransform.GetComponent<MeshRenderer>().enabled = true;
            m_currentProductTransform.GetComponent<Animator>().enabled = true;
            m_currentProductTransform.GetComponent<Animator>().Play("AppearProduct");
        }
    }

    public void GetProductInstance()
    {
        m_currentProductID = 0;
        
        foreach (int element in m_transmutationElementsManager.ActivatedObjectsIDs.OrderBy(e => e))
        {
            Debug.Log("current in activated id " + element);
        }
        
        foreach (var element in m_potentialProductLibrary.PotentialProducts)
        {
            if (Enumerable.SequenceEqual(element.Value.OrderBy(e => e), m_transmutationElementsManager.ActivatedObjectsIDs.OrderBy(e => e)))
            {
                Debug.Log(element.Key + " found one");
                m_currentProductID = element.Key;
                break;
            }
            Debug.Log(element.Key + " is not the ame");
        }
        //visualise potential product
        if (m_currentProductID != 0)
        {
            foreach (Transform element in m_potentialProductsTransform)
            {
                Debug.Log(element.GetComponent<TransmutationProduct>().ID + " not one " + m_currentProductID);
                
                if (element.GetComponent<TransmutationProduct>().ID == m_currentProductID)
                {
                    m_currentProductTransform = element;
                    Debug.Log(element.GetComponent<TransmutationProduct>().ID + " is one " + m_currentProductID);
                    //mageThinking.Play();
                    //element.GetComponent<MeshRenderer>().enabled = true;
                    //element.GetComponent<BoxCollider>().enabled = true;
                    //potentialProductVisualised();
                    break;
                }
            }
        }
    }

    void FinishProcessing()
    {
        if (m_currentProductTransform != null)
        {
            m_currentProductTransform.GetComponent<Animator>().enabled = false;
        }
        
        m_productAppearVFX.gameObject.SetActive(false);
        if (m_currentProductID != 0)
        {
            m_currentProductTransform.GetComponent<MeshRenderer>().enabled = false;
        }

        m_currentProductID = 0;
        m_currentProductTransform = null;
        
        m_transmutationProcessing.DeactivateProcessing();
    }
}
