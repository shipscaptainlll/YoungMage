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

    [Header("Audio Connection")]
    [SerializeField] SoundManager soundManager;
    AudioSource stoneoreMiningSound;
    AudioSource metaloreMiningClosingSound;

    float firstProductChances;
    float secondProductChances;

    static int firstProductCount = 0;
    static int secondProductCount = 0;

    int secondProductID;

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
        
        stoneoreMiningSound = soundManager.LocateAudioSource("StoneOreMining", transform);
        metaloreMiningClosingSound = soundManager.LocateAudioSource("MetalOreMining", transform);
        
        //oreHealthDecreaser = transform.Find("OreHealth").GetComponent<OreHealthDecreaser>();
        productPopuper = transform.Find("OrePopup").GetComponent<MiningProductPopuper>();
        oreHealthDecreaser.HealthReachedZero += PopUpOre;

        firstProductChances = transform.GetComponent<IOre>().FirstProductChances;
        secondProductChances = transform.GetComponent<IOre>().SecondProductChances;
        firstProductInstance = transform.GetComponent<IOre>().FirstProductInstance;
        secondProductInstance = transform.GetComponent<IOre>().SecondProductInstance;
        if (secondProductInstance != null && secondProductInstance.GetComponent<GlobalResource>() != null) { secondProductID = secondProductInstance.GetComponent<GlobalResource>().ID; } else { secondProductID = 2; }
        
        Debug.Log(secondProductID);
        random = new System.Random();
        StartCoroutine(CountChances());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            if (secondProductID == 2)
            {
                stoneoreMiningSound.Play();
                Debug.Log("started playing sounds for stone");
            }
            else if (secondProductID == 3)
            {
                metaloreMiningClosingSound.Play();
                Debug.Log("started playing sounds for metal");
            }
        }
        
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
        PlayMiningSound();
        oreHealthDecreaser.DealDamage(100);
    }

    void PlayMiningSound()
    {
        if (secondProductID == 2)
        {
            stoneoreMiningSound.Play();
        } else if (secondProductID == 3)
        {
            metaloreMiningClosingSound.Play();
        }
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
