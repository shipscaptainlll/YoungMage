using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject goldCoins;
    [SerializeField] GameObject stoneOre;
    [SerializeField] GameObject metalOre;
    [SerializeField] GameObject cursedOre;
    [SerializeField] GameObject earthStoneOre;
    [SerializeField] GameObject lavaStoneOre;
    [SerializeField] GameObject magicStoneOre;
    [SerializeField] GameObject waterStoneOre;
    [SerializeField] GameObject windStoneOre;
    [SerializeField] GameObject magicWand;
    [SerializeField] GameObject stoneHands;
    [SerializeField] GameObject leggings;
    [SerializeField] GameObject plateArmor;
    [SerializeField] GameObject shoes;
    [SerializeField] GameObject helm;
    [SerializeField] GameObject gloves;
    [SerializeField] GameObject bracers;
    [SerializeField] GameObject stoneBrick;
    [SerializeField] GameObject metalIngot;
    [SerializeField] GameObject cursedIngot;
    [SerializeField] GameObject earthStoneDust;
    [SerializeField] GameObject lavaStoneDust;
    [SerializeField] GameObject magicStoneDust;
    [SerializeField] GameObject waterStoneDust;
    [SerializeField] GameObject windStoneDust;


    void Start()
    {
        
    }

    public GameObject TakeObject(int customeID)
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
        }
        return null;
    }

}
