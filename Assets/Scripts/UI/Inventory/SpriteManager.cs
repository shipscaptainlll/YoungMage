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
    [SerializeField] Sprite magicWand;
    [SerializeField] Sprite stoneHands;
    [SerializeField] Sprite leggings;
    [SerializeField] Sprite plateArmor;
    [SerializeField] Sprite shoes;
    [SerializeField] Sprite helm;
    [SerializeField] Sprite gloves;
    [SerializeField] Sprite bracers;
    [SerializeField] Sprite stoneBrick;
    [SerializeField] Sprite metalIngot;
    [SerializeField] Sprite cursedIngot;
    [SerializeField] Sprite earthStoneDust;
    [SerializeField] Sprite lavaStoneDust;
    [SerializeField] Sprite magicStoneDust;
    [SerializeField] Sprite waterStoneDust;
    [SerializeField] Sprite windStoneDust;


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
        return nothing;
    }
}
