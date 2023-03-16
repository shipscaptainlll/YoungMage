using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreMiningManager : MonoBehaviour
{
    Transform firstProductInstance;
    Transform secondProductInstance;
    MiningProductPopuper productPopuper;
    [SerializeField] OreHealthDecreaser oreHealthDecreaser;
    [SerializeField] ParticleSystem particlesPS;
    SkeletonBehavior connectedSkeleton;

    [Header("Audio Connection")]
    [SerializeField] SoundManager soundManager;
    AudioSource oreMiningSound;
    AudioSource popUpSound;
    [SerializeField] BookSpellsActivator bookSpellsActivator;

    float firstProductChances;
    float secondProductChances;

    static int firstProductCount = 0;
    static int secondProductCount = 0;

    bool healthVisible = false;

    int secondProductID;

    System.Random random;
    public bool HealthVisible { get { return healthVisible; } }
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
            bookSpellsActivator.CastMiningSpell();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        
        popUpSound = soundManager.LocateAudioSource("PopUp", transform);

        //oreHealthDecreaser = transform.Find("OreHealth").GetComponent<OreHealthDecreaser>();
        productPopuper = transform.Find("OrePopup").GetComponent<MiningProductPopuper>();
        oreHealthDecreaser.HealthReachedZero += PopUpOre;

        firstProductChances = transform.GetComponent<IOre>().FirstProductChances;
        secondProductChances = transform.GetComponent<IOre>().SecondProductChances;
        firstProductInstance = transform.GetComponent<IOre>().FirstProductInstance;
        secondProductInstance = transform.GetComponent<IOre>().SecondProductInstance;
        if (secondProductInstance != null && secondProductInstance.GetComponent<GlobalResource>() != null) { secondProductID = secondProductInstance.GetComponent<GlobalResource>().ID; } else { secondProductID = 2; }
        InitializeMiningSound();
        //Debug.Log(secondProductID);
        random = new System.Random();
        StartCoroutine(CountChances());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            
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

    void DecreaseOreHealth(int damage)
    {
        //Debug.Log("Hitted");
        particlesPS.Play();
        PlayMiningSound();
        oreHealthDecreaser.DealDamage(damage);
    }

    void PlayMiningSound()
    {
        oreMiningSound.Play();
    }

    void InitializeMiningSound()
    {
        if (secondProductID == 2)
        {
            oreMiningSound = soundManager.LocateAudioSource("StoneOreMining", transform);
            //Debug.Log("started playing sounds for stone");
        }
        else if (secondProductID == 3)
        {
            oreMiningSound = soundManager.LocateAudioSource("MetalOreMining", transform);
            //Debug.Log("started playing sounds for metal");
        }
        else if (secondProductID == 4)
        {
            oreMiningSound = soundManager.LocateAudioSource("CursedoreMining", transform);
            //Debug.Log("started playing sounds for metal");
        }
        else if (secondProductID == 5)
        {
            oreMiningSound = soundManager.LocateAudioSource("EarthstoneMining", transform);
            //Debug.Log("started playing sounds for earth");
        }
        else if (secondProductID == 6)
        {
            oreMiningSound = soundManager.LocateAudioSource("LavastoneOreMining", transform);
            //Debug.Log("started playing sounds for lava");
        }
        else if (secondProductID == 7)
        {
            oreMiningSound = soundManager.LocateAudioSource("MagicstoneoreMining", transform);
            //Debug.Log("started playing sounds for magic");
        }
        else if (secondProductID == 8)
        {
            oreMiningSound = soundManager.LocateAudioSource("WaterstoneOreMining", transform);
            //Debug.Log("started playing sounds for water");
        }
        else if (secondProductID == 9)
        {
            oreMiningSound = soundManager.LocateAudioSource("WindstoneOreMining", transform);
            //Debug.Log("started playing sounds for wind");
        }
    }

    public void VisualiseOreHealthbar()
    {
        oreHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        healthVisible = true;
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

    public void HideOreHealthbar()
    {
        oreHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        healthVisible = false;
    }

    void DisconnectScriptsInterraction(SkeletonBehavior connectingSkeleton)
    {
        connectedSkeleton.OreHitted -= DecreaseOreHealth;
    }

    void PopUpOre()
    {
        popUpSound.Play();
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
