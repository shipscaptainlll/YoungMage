using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    [SerializeField] Transform goldCoins;
    [SerializeField] Transform stoneOre;
    [SerializeField] Transform metalOre;
    [SerializeField] Transform cursedOre;
    [SerializeField] Transform earthStoneOre;
    [SerializeField] Transform lavaStoneOre;
    [SerializeField] Transform magicStoneOre;
    [SerializeField] Transform waterStoneOre;
    [SerializeField] Transform windStoneOre;
    [SerializeField] Transform magicWand;
    [SerializeField] Transform stoneHands;
    [SerializeField] Transform leggings;
    [SerializeField] Transform plateArmor;
    [SerializeField] Transform shoes;
    [SerializeField] Transform helm;
    [SerializeField] Transform gloves;
    [SerializeField] Transform bracers;
    [SerializeField] Transform stoneBrick;
    [SerializeField] Transform metalIngot;
    [SerializeField] Transform cursedIngot;
    [SerializeField] Transform earthStoneDust;
    [SerializeField] Transform lavaStoneDust;
    [SerializeField] Transform magicStoneDust;
    [SerializeField] Transform waterStoneDust;
    [SerializeField] Transform windStoneDust;
    [SerializeField] Transform skeletonCounter;


    void Start()
    {
        
    }

    public Transform TakeCounter(int customeID)
    {
        switch (customeID)
        {
            case 0:
                return null;
            case 1:
                return goldCoins;
            case 2:
                return stoneOre;
            case 3:
                return metalOre;
            case 4:
                return cursedOre;
            case 5:
                return earthStoneOre;
            case 6:
                return lavaStoneOre;
            case 7:
                return magicStoneOre;
            case 8:
                return waterStoneOre;
            case 9:
                return windStoneOre;
            case 10:
                return magicWand;
            case 11:
                return stoneHands;
            case 12:
                return leggings;
            case 13:
                return plateArmor;
            case 14:
                return shoes;
            case 15:
                return helm;
            case 16:
                return gloves;
            case 17:
                return bracers;
            case 18:
                return stoneBrick;
            case 19:
                return metalIngot;
            case 20:
                return cursedIngot;
            case 21:
                return earthStoneDust;
            case 22:
                return lavaStoneDust;
            case 23:
                return magicStoneDust;
            case 24:
                return waterStoneDust;
            case 25:
                return windStoneDust;
            case 27:
                return skeletonCounter;
        }
        return null;
    }

}
