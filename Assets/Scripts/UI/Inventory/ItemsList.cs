using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public enum Items
    {
        nothing = 0,
        goldCoins = 1,
        stoneOre = 2,
        metalOre = 3,
        cursedOre = 4,
        earthStoneOre = 5,
        lavaStoneOre = 6,
        magicStoneOre = 7,
        waterStoneOre = 8,
        windStoneOre = 9,
        magicWand = 10
    }
}
