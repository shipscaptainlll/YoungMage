using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreMiningManager : MonoBehaviour
{
    [SerializeField] Transform productInstance;
    MiningProductPopuper productPopuper;
    OreHealthDecreaser oreHealthDecreaser;
    SkeletonBehavior connectedSkeleton;

    public Transform ProductInstance { get { return productInstance; } }
    public SkeletonBehavior ConnectedSkeleton
    {
        get
        {
            return connectedSkeleton;
        }
        set
        {
            if (value == null)
            {
                DisconnectSkeleton();
            } else
            {
                ConnectSkeleton(value);
            }
            connectedSkeleton = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        oreHealthDecreaser = transform.Find("OreHealth").GetComponent<OreHealthDecreaser>();
        productPopuper = transform.Find("OrePopup").GetComponent<MiningProductPopuper>();
        oreHealthDecreaser.HealthReachedZero += PopUpOre;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConnectSkeleton(SkeletonBehavior connectingSkeleton)
    {
        VisualiseOreHealthbar();
        ConnectScriptsInterraction(connectingSkeleton);
    }

    void DisconnectSkeleton()
    {
        HideOreHealthbar();
        DisconnectScriptsInterraction(connectedSkeleton);
    }

    void DecreaseOreHealth()
    {
        oreHealthDecreaser.DealDamage(100);
    }

    void VisualiseOreHealthbar()
    {
        oreHealthDecreaser.gameObject.SetActive(true);
    }

    void ConnectScriptsInterraction(SkeletonBehavior connectingSkeleton)
    {
        connectingSkeleton.OreHitted += DecreaseOreHealth;
    }

    void HideOreHealthbar()
    {
        oreHealthDecreaser.gameObject.SetActive(false);
    }

    void DisconnectScriptsInterraction(SkeletonBehavior connectingSkeleton)
    {
        connectedSkeleton.OreHitted -= DecreaseOreHealth;
    }

    void PopUpOre()
    {
        productPopuper.PopupProduct();
    }
}
