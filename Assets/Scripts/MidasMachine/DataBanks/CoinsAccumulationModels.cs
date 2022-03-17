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
            case 2:
                return twoCoins;
            case 5:
                return fewCoins;
            case 10:
                return smallAmmountCoins;
            case 25:
                return normalAmmountCoins;
            case 100:
                return bigAmmountCoins;
            case 125:
                return bigSmallAmmountCoins;
            case 250:
                return bigNormalAmmountCoins;
            case 1000:
                return bigBigAmmountCoins;
            case 2500:
                return sacketAmmountCoins;
        }
        return null;
    }
}
