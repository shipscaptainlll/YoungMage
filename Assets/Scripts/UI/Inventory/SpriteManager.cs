using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] Sprite nothing;
    [SerializeField] Sprite goldCoins;
    [SerializeField] Sprite stoneOre;
    [SerializeField] Sprite metalOre;
    [SerializeField] Sprite cursedOre;
    [SerializeField] Sprite earthStoneOre;
    [SerializeField] Sprite lavaStoneOre;
    [SerializeField] Sprite magicStoneOre;
    [SerializeField] Sprite waterStoneOre;
    [SerializeField] Sprite windStoneOre;


    void Start()
    {
        
    }

    public Sprite TakeSprite(int customeID)
    {
        switch (customeID)
        {
            case 0:
                return nothing;
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
        }
        return nothing;
    }

}
