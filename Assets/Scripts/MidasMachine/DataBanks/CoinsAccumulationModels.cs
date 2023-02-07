using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsAccumulationModels : MonoBehaviour
{
    [SerializeField] GameObject oneCoin;
    [SerializeField] GameObject twoCoins;
    [SerializeField] GameObject fewCoins;
    [SerializeField] GameObject smallAmmountCoins;
    [SerializeField] GameObject normalAmmountCoins;
    [SerializeField] GameObject bigAmmountCoins;
    [SerializeField] GameObject bigSmallAmmountCoins;
    [SerializeField] GameObject bigNormalAmmountCoins;
    [SerializeField] GameObject bigBigAmmountCoins;
    [SerializeField] GameObject sacketAmmountCoins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject TakeModel(int count)
    {
        switch (count)
        {
            case 0:
                return null;
            case 1:
                return oneCoin;
            case int n when (n >= 2 && n <5):
                return twoCoins;
            case int n when (n >= 5 && n < 10):
                return fewCoins;
            case int n when (n >= 10 && n < 25):
                return smallAmmountCoins;
            case int n when (n >= 25 && n < 100):
                return normalAmmountCoins;
            case int n when (n >= 100 && n < 125):
                return bigAmmountCoins;
            case int n when (n >= 125 && n < 250):
                return bigSmallAmmountCoins;
            case int n when (n >= 250 && n < 1000):
                return bigNormalAmmountCoins;
            case int n when (n >= 1000 && n < 2500):
                return bigBigAmmountCoins;
            case int n when (n >= 2500):
                return sacketAmmountCoins;
        }
        return null;
    }
}
