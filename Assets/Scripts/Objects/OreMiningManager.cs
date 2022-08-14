using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreMiningManager : MonoBehaviour
{
    Transform firstProductInstance;
    Transform secondProductInstance;
    MiningProductPopuper productPopuper;
    [SerializeField] OreHealthDecreaser oreHealthDecreaser;
    SkeletonBehavior connectedSkeleton;

    float firstProductChances;
    float secondProductChances;

    System.Random random;
    public Transform FirstProductInstance { get { return firstProductInstance; } }
    public Transform SecondProductInstance { get { return secondProductInstance; } }
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
        //oreHealthDecreaser = transform.Find("OreHealth").GetComponent<OreHealthDecreaser>();
        productPopuper = transform.Find("OrePopup").GetComponent<MiningProductPopuper>();
        oreHealthDecreaser.HealthReachedZero += PopUpOre;

        firstProductChances = transform.GetComponent<IOre>().FirstProductChances;
        secondProductChances = transform.GetComponent<IOre>().SecondProductChances;
        firstProductInstance = transform.GetComponent<IOre>().FirstProductInstance;
        secondProductInstance = transform.GetComponent<IOre>().SecondProductInstance;
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConnectSkeleton(SkeletonBehavior connectingSkeleton)
    {
        VisualiseOreHealthbar();
        Debug.Log("Connected");
        ConnectScriptsInterraction(connectingSkeleton);
    }

    void DisconnectSkeleton()
    {
        HideOreHealthbar();
        DisconnectScriptsInterraction(connectedSkeleton);
    }

    void DecreaseOreHealth()
    {
        Debug.Log("Hitted");
        oreHealthDecreaser.DealDamage(100);
    }

    void VisualiseOreHealthbar()
    {
        oreHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 1;
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
        Transform oreToInstantiate = ChooseProduct();
        productPopuper.PopupProduct(oreToInstantiate);
    }

    Transform ChooseProduct()
    {
        float randomValue = Random.value;
        Debug.Log("random value " + randomValue);
        if (randomValue < firstProductChances/100)
        {
            return firstProductInstance;
        } else
        {
            return secondProductInstance;
        }

    }
}
