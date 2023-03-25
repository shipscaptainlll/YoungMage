using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUISkeleton : MonoBehaviour
{
    [SerializeField] private ClickManager m_clickManager;
    [SerializeField] CameraSUIInformer cameraSUIInformer;
    [SerializeField] private Sprite m_smallSkeletonSprite;
    [SerializeField] private Sprite m_bigSkeletonSprite;
    [SerializeField] private Sprite m_lizardSkeletonSprite;
    [SerializeField] private Text m_skeltonName;
    [SerializeField] private Image m_skeletonImage;
    [SerializeField] Text objectPower;
    [SerializeField] Text objectInventoryPower;
    [SerializeField] Text objectSpeed;
    [SerializeField] Text objectInventorySpeed;
    [SerializeField] Text objectAppliedInventory;
    [SerializeField] Text objectOccupation;
    [SerializeField] Text appliedObjects;
    [SerializeField] Transform borderedCanvas;
    private SkeletonsScanner m_lastSkeletonsScanner;
    
    public void ShowNewObject(AttachedItemsManager attachedItemsManager, SkeletonBehavior skeletonBehavior, SkeletonsScanner skeletonsScanner)
    {
        m_clickManager.LMBUped += HideElement;
        transform.localScale = new Vector3(1, 1, 1);
        transform.GetComponent<CanvasGroup>().alpha = 1;
        m_lastSkeletonsScanner = skeletonsScanner;
        skeletonsScanner.ActivateScanner();
        CountSkeletonItems(attachedItemsManager);
        UpdateSkeletonImage(skeletonBehavior);
    }

    public void HideElement()
    {
        m_clickManager.LMBUped -= HideElement;
        Debug.Log("Sui was hidded");
        if (m_lastSkeletonsScanner != null) { m_lastSkeletonsScanner.DeactivateScanner(); }
        transform.GetComponent<CanvasGroup>().alpha = 0;
    }

    void UpdateSkeletonImage(SkeletonBehavior skeletonBehavior)
    {
        if (skeletonBehavior.IsSmallSkeleton)
        {
            m_skeletonImage.sprite = m_smallSkeletonSprite;
            m_skeltonName.text = "small skeleton";
        } else if (skeletonBehavior.IsBigSkeleton)
        {
            m_skeletonImage.sprite = m_bigSkeletonSprite;
            m_skeltonName.text = "big skeleton";
        } else if (skeletonBehavior.IsLizardSkeleton)
        {
            m_skeletonImage.sprite = m_lizardSkeletonSprite;
            m_skeltonName.text = "lizard skeleton";
        } 
    }
    
    void CountSkeletonItems(AttachedItemsManager attachedItemsManager)
    {
        string itemsString = "";
        if (attachedItemsManager.StoneHandsEquiped)
        {
            itemsString += "\nhands";
        }
        if (attachedItemsManager.LeggingsEquiped)
        {
            itemsString += "\nleggings";
        }
        if (attachedItemsManager.ChainMailEquiped)
        {
            itemsString += "\narmor";
        }
        if (attachedItemsManager.BootsEquiped)
        {
            itemsString += "\nshoes";
        }
        if (attachedItemsManager.HelmEquiped)
        {
            itemsString += "\nhelm";
        }
        if (attachedItemsManager.GlovesEquiped)
        {
            itemsString += "\ngloves";
        }
        if (attachedItemsManager.VambraceEquiped)
        {
            itemsString += "\nbracers";
        }
        //Debug.Log(appliedObjects.text);
        appliedObjects.text = itemsString;
        //Debug.Log(appliedObjects.text);
    }
}
