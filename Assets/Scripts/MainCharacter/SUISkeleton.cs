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
    [SerializeField] private Text m_skeletonPower;
    [SerializeField] private Text m_skeletonInventoryPower;
    [SerializeField] private Text m_skeletonSpeed;
    [SerializeField] private Text m_skeletonInventorySpeed;
    [SerializeField] Text objectAppliedInventory;
    [SerializeField] private Text m_skeletonOccupation;
    [SerializeField] Text appliedObjects;
    [SerializeField] Text appliedObjectsPower;
    [SerializeField] Transform borderedCanvas;
    private SkeletonsScanner m_lastSkeletonsScanner;
    
    public void ShowNewObject(AttachedItemsManager attachedItemsManager, SkeletonBehavior skeletonBehavior, SkeletonsScanner skeletonsScanner, Skeleton skeleton)
    {
        m_clickManager.LMBUped += HideElement;
        transform.localScale = new Vector3(1, 1, 1);
        transform.GetComponent<CanvasGroup>().alpha = 1;
        m_lastSkeletonsScanner = skeletonsScanner;
        skeletonsScanner.ActivateScanner();
        UpdateSkeletonOccupation(skeletonBehavior);
        CountSkeletonItems(attachedItemsManager);
        UpdateSkeletonPowers(skeleton, attachedItemsManager);
        UpdateSkeletonImage(skeletonBehavior);
    }

    public void HideElement()
    {
        m_clickManager.LMBUped -= HideElement;
        Debug.Log("Sui was hidded");
        if (m_lastSkeletonsScanner != null) { m_lastSkeletonsScanner.DeactivateScanner(); }
        transform.GetComponent<CanvasGroup>().alpha = 0;
    }

    void UpdateSkeletonPowers(Skeleton skeleton, AttachedItemsManager attachedItemsManager)
    {
        m_skeletonPower.text = skeleton.Power.ToString();
        m_skeletonSpeed.text = skeleton.Speed.ToString();
        m_skeletonInventoryPower.text = "(+" + attachedItemsManager.ItemsCummulativePower.ToString() + ")";
        m_skeletonInventorySpeed.text = "(+" + attachedItemsManager.ItemsCummulativeSpeed.ToString() + ")";
    }
    
    void UpdateSkeletonOccupation(SkeletonBehavior skeletonBehavior)
    {
        if (skeletonBehavior.Mining) { m_skeletonOccupation.text = "Mining ore"; }
        else if (skeletonBehavior.TacklingDoor) { m_skeletonOccupation.text = "Attacking doors"; }
        else if (skeletonBehavior.BeingUnconjured) { m_skeletonOccupation.text = "Destruction"; }
        else { m_skeletonOccupation.text = "Idle"; }
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
        string powersString = "";
        if (attachedItemsManager.StoneHandsEquiped)
        {
            itemsString += "\nhands";
            powersString += "\n+10";
        }
        if (attachedItemsManager.LeggingsEquiped)
        {
            itemsString += "\nleggings";
            powersString += "\n+7";
        }
        if (attachedItemsManager.ChainMailEquiped)
        {
            itemsString += "\narmor";
            powersString += "\n+15";
        }
        if (attachedItemsManager.BootsEquiped)
        {
            itemsString += "\nshoes";
            powersString += "\n+5";
        }
        if (attachedItemsManager.HelmEquiped)
        {
            itemsString += "\nhelm";
            powersString += "\n+10";
        }
        if (attachedItemsManager.GlovesEquiped)
        {
            itemsString += "\ngloves";
            powersString += "\n+5";
        }
        if (attachedItemsManager.VambraceEquiped)
        {
            itemsString += "\nbracers";
            powersString += "\n+3";
        }
        //Debug.Log(appliedObjects.text);
        appliedObjects.text = itemsString;
        appliedObjectsPower.text = powersString;
        //Debug.Log(appliedObjects.text);
    }
}
