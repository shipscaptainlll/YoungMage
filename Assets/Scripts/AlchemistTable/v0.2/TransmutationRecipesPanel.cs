using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationRecipesPanel : MonoBehaviour
{
    [SerializeField] private Transform m_preLoadedTemplatesHolder;
    [SerializeField] private Transform m_recipesHolder;
    [SerializeField] private Transform m_slotsHolder;
    [SerializeField] private PotentialProductLibrary m_potentialProductLibrary;
    [SerializeField] private SoundManager m_soundManager;
    [SerializeField] private CounterManager m_counterManager;
    [SerializeField] private TransmutationErrorsNotificator m_transmutationErrorsNotificator;
    AudioSource changeElementSound;
    
    private Dictionary<int, bool> m_activatedRecipesDictionary = new Dictionary<int, bool>();
    
    public Dictionary<int, bool> ActivatedRecipesDictionary { get => m_activatedRecipesDictionary; }

    public void UpdateRecipesDictionary(int itemID)
    {
        m_activatedRecipesDictionary[itemID] = true;
        InstantiateNewRecipe(itemID);
    }

    private void Start()
    {
        foreach (int element in m_potentialProductLibrary.PotentialProducts.Keys)
        {
            m_activatedRecipesDictionary.Add(element, false);
        }
        
        /*
        foreach (Transform element in m_preLoadedTemplatesHolder)
        {
            InstantiateNewRecipe(Int32.Parse(element.name));
        }
        */
        changeElementSound = m_soundManager.FindSound("NewObjectAppearingUI");
    }

    public void ActivateRecipe(int itemID)
    {
        foreach (Transform element in m_slotsHolder)
        {
            if (element.GetChild(0).Find("Element").GetComponent<Element>().CustomID != 0)
            {
                element.GetChild(0).Find("Element").GetComponent<Element>().AttachedCounter.GetComponent<ICounter>().AddResource(1);
            }
            
            element.GetChild(0).Find("Element").GetComponent<Element>().CustomID = 0;
        }

        List<int> recipeRequiredItems = m_potentialProductLibrary.PotentialProducts[itemID];

        bool somethingWasMissing = false;
        
        int indexer = 0;
        
        foreach (int recipeItemID in recipeRequiredItems)
        {
            
            if (m_counterManager.TakeCounter(recipeItemID).GetComponent<ICounter>().Count <= 0)
            {
                somethingWasMissing = true;
                continue;
            }
            foreach (Transform element in m_slotsHolder)
            {
                if (element.GetChild(0).Find("Element").GetComponent<Element>().CustomID == 0)
                {
                    element.GetChild(0).Find("Element").GetComponent<Element>().CustomID = recipeRequiredItems[indexer];
                    m_counterManager.TakeCounter(recipeItemID).GetComponent<ICounter>().AddResource(-1);
                    break;
                }
            }
        }

        if (somethingWasMissing)
        {
            m_transmutationErrorsNotificator.ActivatePopup("Not enough resources!");
        }
        
        changeElementSound.Play();
    }

    public void InstantiateNewRecipe(int itemID)
    {
        Transform m_newRecipeTemplate = FindNewTemplate(itemID);
        if (m_newRecipeTemplate == null)
        {
            return;
        }
        m_transmutationErrorsNotificator.ActivatePopup("New recipe found: " + ItemsNames.GetName(itemID));
        Transform newRecipe = Instantiate(m_newRecipeTemplate, m_recipesHolder.position, m_recipesHolder.rotation);
        newRecipe.parent = m_recipesHolder;
        newRecipe.SetAsFirstSibling();
        newRecipe.localScale = new Vector3(0.7702f, 0.7702f, 0.7702f);
        RectTransform originRect = m_newRecipeTemplate.GetComponent<RectTransform>();
        newRecipe.GetComponent<RectTransform>().sizeDelta = new Vector2(originRect.rect.width, originRect.rect.height);
        newRecipe.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ResetRecipes()
    {
        m_activatedRecipesDictionary = new Dictionary<int, bool>();

        foreach (int element in m_potentialProductLibrary.PotentialProducts.Keys)
        {
            m_activatedRecipesDictionary.Add(element, false);
        }

        foreach (Transform element in m_recipesHolder)
        {
            DestroyImmediate(element.gameObject);
        }
    }

    public void UploadRecipes(int[] recipeIDs)
    {
        ResetRecipes();

        foreach (int element in recipeIDs)
        {
            m_activatedRecipesDictionary[element] = true;

            Transform m_newRecipeTemplate = FindNewTemplate(element);
            if (m_newRecipeTemplate == null)
            {
                return;
            }
            //m_transmutationErrorsNotificator.ActivatePopup("New recipe found: " + ItemsNames.GetName(itemID));
            Transform newRecipe = Instantiate(m_newRecipeTemplate, m_recipesHolder.position, m_recipesHolder.rotation);
            newRecipe.parent = m_recipesHolder;
            newRecipe.SetAsFirstSibling();
            newRecipe.localScale = new Vector3(0.7702f, 0.7702f, 0.7702f);
            RectTransform originRect = m_newRecipeTemplate.GetComponent<RectTransform>();
            newRecipe.GetComponent<RectTransform>().sizeDelta = new Vector2(originRect.rect.width, originRect.rect.height);
            newRecipe.GetComponent<CanvasGroup>().alpha = 1;
        }

        
    }

    Transform FindNewTemplate(int itemID)
    {
        foreach (Transform element in m_preLoadedTemplatesHolder)
        {
            if (element.name == itemID.ToString())
            {
                return element;
            }
        }

        return null;
    }
}
