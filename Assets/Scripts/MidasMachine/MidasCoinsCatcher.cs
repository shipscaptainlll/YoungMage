using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasCoinsCatcher : MonoBehaviour
{
    [SerializeField] CoinsAccumulationModels coinsAccumulationModels;
    [SerializeField] Transform coinsAccumulationPosition;
    [SerializeField] Transform coinsAccumulationHolder;
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    GameObject currentAccumulationForm = null;
    int coinsCount = 0;

    public int CoinsCount { get { return coinsCount; } set { coinsCount = value; } }

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource singleCoinSound;
    AudioSource pileCoinsSoundFirst;
    AudioSource pileCoinsSoundSecond;
    AudioSource pileCoinsSoundThird;

    System.Random rand;
    public event Action<Transform> CollectionUpdated = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        singleCoinSound = soundManager.LocateAudioSource("CoinFallingMetal", transform);
        pileCoinsSoundFirst = soundManager.LocateAudioSource("CoinFallPilecoinsFirst", transform);
        pileCoinsSoundSecond = soundManager.LocateAudioSource("CoinFallPilecoinsSecond", transform);
        pileCoinsSoundThird = soundManager.LocateAudioSource("CoinFallPilecoinsThird", transform);

        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GlobalResource>() != null && other.GetComponent<GlobalResource>().ID == 1)
        {
            Destroy(other.gameObject);
            AddToCount();
            CountCoins();
        }
    }

    public void CollectAccumulatedGold()
    {
        goldCoinsCounter.AddResource(coinsCount);
        coinsCount = 0;
        CountCoins();
    }

    public void AddToCount()
    {
        coinsCount += 1;
    }

    public void CountCoins()
    {
        if (coinsCount == 0) { ResetModel(); return; }
        if (coinsCount == 1) { singleCoinSound.Play(); }
        else if (coinsCount > 1)
        {
            int randomInt = rand.Next(1, 4);
            if (randomInt == 1) { pileCoinsSoundFirst.Play(); }
            else if (randomInt == 2) { pileCoinsSoundSecond.Play(); }
            else if (randomInt > 2) { pileCoinsSoundThird.Play(); }
        }
        GameObject potentialAccumulationForm = coinsAccumulationModels.TakeModel(coinsCount);
        Debug.Log("here we gooo " + coinsCount);
        Debug.Log(currentAccumulationForm == null);
        Debug.Log(potentialAccumulationForm != null);
        if (currentAccumulationForm != potentialAccumulationForm && potentialAccumulationForm != null)
        {
            Debug.Log(potentialAccumulationForm);
            UpdateAccumulationState(potentialAccumulationForm);
        } else if (currentAccumulationForm == null && potentialAccumulationForm != null)
        {
            UpdateAccumulationState(potentialAccumulationForm);
        }
    }

    void ResetModel()
    {
        if (coinsAccumulationHolder.childCount > 0)
        {
            Destroy(coinsAccumulationHolder.GetChild(0).gameObject);
            currentAccumulationForm = null;
        }
        
    }

    void UpdateAccumulationState(GameObject finalAccumulationForm)
    {
        if (currentAccumulationForm != null) { Destroy(currentAccumulationForm);
            currentAccumulationForm = null;
        }


        Debug.Log("here we go ");
        if (coinsCount < 2500)
        {
            currentAccumulationForm = Instantiate(finalAccumulationForm, coinsAccumulationPosition.position, Quaternion.Euler(new Vector3(90, 0, 0)));
        } else { currentAccumulationForm = Instantiate(finalAccumulationForm, coinsAccumulationPosition.position, Quaternion.Euler(new Vector3(0, 0, 0))); }
        currentAccumulationForm.transform.parent = coinsAccumulationHolder;
        currentAccumulationForm.AddComponent<AdditionalCoinsCatcher>();
        currentAccumulationForm.GetComponent<AdditionalCoinsCatcher>().MidasCoinsCatcher = transform;
        currentAccumulationForm.AddComponent<Rigidbody>();
        //currentAccumulationForm.AddComponent<MeshCollider>().convex = true;
    }
}
