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
        }
        return null;
    }

}
