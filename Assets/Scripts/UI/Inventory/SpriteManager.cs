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

    public static Dictionary<int, Sprite> SpriteDictionary = new Dictionary<int, Sprite>();
    void Start()
    {
        FillDictionary();
    }

    public static Sprite GetSprite(int customeID)
    {
        return SpriteDictionary[customeID];
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

    void FillDictionary()
    {
        SpriteDictionary.Add(1, goldCoins);
        SpriteDictionary.Add(2, stoneOre);
        SpriteDictionary.Add(3, metalOre);
        SpriteDictionary.Add(4, cursedOre);
        SpriteDictionary.Add(5, earthStoneOre);
        SpriteDictionary.Add(6, lavaStoneOre);
        SpriteDictionary.Add(7, magicStoneOre);
        SpriteDictionary.Add(8, waterStoneOre);
        SpriteDictionary.Add(9, windStoneOre);
        SpriteDictionary.Add(10, magicWand);
        SpriteDictionary.Add(11, stoneHands);
        SpriteDictionary.Add(12, leggings);
        SpriteDictionary.Add(13, plateArmor);
        SpriteDictionary.Add(14, shoes);
        SpriteDictionary.Add(15, helm);
        SpriteDictionary.Add(16, gloves);
        SpriteDictionary.Add(17, bracers);
        SpriteDictionary.Add(18, stoneBrick);
        SpriteDictionary.Add(19, metalIngot);
        SpriteDictionary.Add(20, cursedIngot);
        SpriteDictionary.Add(21, earthStoneDust);
        SpriteDictionary.Add(22, lavaStoneDust);
        SpriteDictionary.Add(23, magicStoneDust);
        SpriteDictionary.Add(24, waterStoneDust);
        SpriteDictionary.Add(25, windStoneDust);
    }
}
