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

    static int firstProductCount = 0;
    static int secondProductCount = 0;

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
        StartCoroutine(CountChances());
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
        //Debug.Log("Hitted");
        oreHealthDecreaser.DealDamage(100);
    }

    void VisualiseOreHealthbar()
    {
        oreHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    void ConnectScriptsInterraction(SkeletonBehavior connectingSkeleton)
    {
        
        connectingSkeleton.OreHitted += DecreaseOreHealth;
        connectingSkeleton.ObjectConnected += NotifyHealthDecreser;
    }

    void NotifyHealthDecreser()
    {
        oreHealthDecreaser.CalculateDamage(connectedSkeleton);
    }

    void HideOreHealthbar()
    {
        oreHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 0;
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
        //Debug.Log("random value " + randomValue + " first chances " + firstProductChances/100);

        if (randomValue < firstProductChances/100)
        {
            firstProductCount++;
            //Debug.Log("first product count " + firstProductCount + " second product count " + secondProductCount + " analyzed chances " + 100 * (firstProductCount/(firstProductCount + secondProductCount)));
            return firstProductInstance;
        } else
        {
            secondProductCount++;
            //Debug.Log("first product count " + firstProductCount + " second product count " + secondProductCount + " analyzed chances " + 100 * (firstProductCount / (firstProductCount + secondProductCount)));
            return secondProductInstance;
        }

    }

    IEnumerator CountChances()
    {
        while (true)
        {
            ChooseProduct();
            yield return new WaitForSeconds(0.033f);
        }
        
    }
}
