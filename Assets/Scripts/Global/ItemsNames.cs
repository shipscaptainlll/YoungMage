using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsNames : MonoBehaviour
{
    public string nothing;
    public string goldCoins;
    public string stoneOre;
    public string metalOre;
    public string cursedOre;
    public string earthStoneOre;
    public string lavaStoneOre;
    public string magicStoneOre;
    public string waterStoneOre;
    public string windStoneOre;
    public string magicWand;
    public string stoneHands;
    public string leggings;
    public string plateArmor;
    public string shoes;
    public string helm;
    public string gloves;
    public string bracers;
    public string stoneBrick;
    public string metalIngot;
    public string cursedIngot;
    public string earthStoneDust;
    public string lavaStoneDust;
    public string magicStoneDust;
    public string waterStoneDust;
    public string windStoneDust;
    public string skeletonScanner;
    private Coroutine DictionaryUpdate;
    
    public string Nothing { get => nothing; set => nothing = value; }
    public string GoldCoins { get => goldCoins; set => goldCoins = value; }
    public string StoneOre { get => stoneOre; set => stoneOre = value; }
    public string MetalOre { get => metalOre; set => metalOre = value; }
    public string CursedOre { get => cursedOre; set => cursedOre = value; }
    public string EarthStoneOre { get => earthStoneOre; set => earthStoneOre = value; }
    public string LavaStoneOre { get => lavaStoneOre; set => lavaStoneOre = value; }
    public string MagicStoneOre { get => magicStoneOre; set => magicStoneOre = value; }
    public string WaterStoneOre { get => waterStoneOre; set => waterStoneOre = value; }
    public string WindStoneOre { get => windStoneOre; set => windStoneOre = value; }
    public string MagicWand { get => magicWand; set => magicWand = value; }
    public string StoneHands { get => stoneHands; set => stoneHands = value; }
    public string Leggings { get => leggings; set => leggings = value; }
    public string PlateArmor { get => plateArmor; set => plateArmor = value; }
    public string Shoes { get => shoes; set => shoes = value; }
    public string Helm { get => helm; set => helm = value; }
    public string Gloves { get => gloves; set => gloves = value; }
    public string Bracers { get => bracers; set => bracers = value; }
    public string StoneBrick { get => stoneBrick; set => stoneBrick = value; }
    public string MetalIngot { get => metalIngot; set => metalIngot = value; }
    public string CursedIngot { get => cursedIngot; set => cursedIngot = value; }
    public string EarthStoneDust { get => earthStoneDust; set => earthStoneDust = value; }
    public string LavaStoneDust { get => lavaStoneDust; set => lavaStoneDust = value; }
    public string MagicStoneDust { get => magicStoneDust; set => magicStoneDust = value; }
    public string WaterStoneDust { get => waterStoneDust; set => waterStoneDust = value; }
    public string WindStoneDust { get => windStoneDust; set => windStoneDust = value; }
    public string SkeletonScanner { get => skeletonScanner; set => skeletonScanner = value; }

    public static Dictionary<int, string> NamesDictionary = new Dictionary<int, string>();
    void Start()
    {
        FillDictionary();
    }

    public static string GetName(int customeID)
    {
        return NamesDictionary[customeID];
    }

    IEnumerator UpdateDictionaryCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        NamesDictionary = new Dictionary<int, string>();
        FillDictionary();
        int indxer = 1;
        while (indxer <= NamesDictionary.Count)
        {
            //Debug.Log("indxer " + indxer + " " + NamesDictionary[indxer]);
            indxer++;
        }

        yield return null;
        DictionaryUpdate = null;
    }

    public void UpdateDictionary()
    {
        if (DictionaryUpdate != null) { StopCoroutine(DictionaryUpdate); }
        DictionaryUpdate = StartCoroutine(UpdateDictionaryCoroutine());
    }

    void FillDictionary()
    {
        NamesDictionary.Add(1, goldCoins);
        NamesDictionary.Add(2, stoneOre);
        NamesDictionary.Add(3, metalOre);
        NamesDictionary.Add(4, cursedOre);
        NamesDictionary.Add(5, earthStoneOre);
        NamesDictionary.Add(6, lavaStoneOre);
        NamesDictionary.Add(7, magicStoneOre);
        NamesDictionary.Add(8, waterStoneOre);
        NamesDictionary.Add(9, windStoneOre);
        NamesDictionary.Add(10, magicWand);
        NamesDictionary.Add(11, stoneHands);
        NamesDictionary.Add(12, leggings);
        NamesDictionary.Add(13, plateArmor);
        NamesDictionary.Add(14, shoes);
        NamesDictionary.Add(15, helm);
        NamesDictionary.Add(16, gloves);
        NamesDictionary.Add(17, bracers);
        NamesDictionary.Add(18, stoneBrick);
        NamesDictionary.Add(19, metalIngot);
        NamesDictionary.Add(20, cursedIngot);
        NamesDictionary.Add(21, earthStoneDust);
        NamesDictionary.Add(22, lavaStoneDust);
        NamesDictionary.Add(23, magicStoneDust);
        NamesDictionary.Add(24, waterStoneDust);
        NamesDictionary.Add(25, windStoneDust);
        NamesDictionary.Add(26, nothing);
        NamesDictionary.Add(27, skeletonScanner);
    }
}
