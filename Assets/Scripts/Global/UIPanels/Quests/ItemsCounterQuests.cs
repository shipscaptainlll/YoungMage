using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCounterQuests : MonoBehaviour
{
    int collectedCoinsCount;
    int collectedStoneoreCount;
    int collectedMetaloreCount;
    int collectedCursedoreCount;
    int collectedEarthstoneoreCount;
    int collectedLavastoneoreCount;
    int collectedMagicstoneoreCount;
    int collectedWaterstoneoreCount;
    int collectedWindstoneoreCount;
    int collectedStonehandsCount;
    int collectedLeggingsCount;
    int collectedPlatearmorCount;
    int collectedShoesCount;
    int collectedHelmsCount;
    int collectedGlovesCount;
    int collectedBracersCount;
    int collectedStonebricksCount;
    int collectedMetalingotsCount;
    int collectedCursedingotsCount;
    int collectedEarthstonedustCount;
    int collectedLavastonedustCount;
    int collectedMagicstonedustCount;
    int collectedWaterstonedustCount;
    int collectedWindstonedustCount;

    public int CollectedCoinsCount { get { return collectedCoinsCount; } }
    public int CollectedStoneoreCount { get { return collectedStoneoreCount; } }
    public int CollectedMetaloreCount { get { return collectedMetaloreCount; } }
    public int CollectedCursedoreCount { get { return collectedCursedoreCount; } }
    public int CollectedEarthstoneoreCount { get { return collectedEarthstoneoreCount; } }
    public int CollectedLavastoneoreCount { get { return collectedLavastoneoreCount; } }
    public int CollectedMagicstoneoreCount { get { return collectedMagicstoneoreCount; } }
    public int CollectedWaterstoneoreCount { get { return collectedWaterstoneoreCount; } }
    public int CollectedWindstoneoreCount { get { return collectedWindstoneoreCount; } }
    public int CollectedStonehandsCount { get { return collectedStonehandsCount; } }
    public int CollectedLeggingsCount { get { return collectedLeggingsCount; } }
    public int CollectedPlatearmorCount { get { return collectedPlatearmorCount; } }
    public int CollectedShoesCount { get { return collectedShoesCount; } }
    public int CollectedHelmsCount { get { return collectedHelmsCount; } }
    public int CollectedGlovesCount { get { return collectedGlovesCount; } }
    public int CollectedBracersCount { get { return collectedBracersCount; } }
    public int CollectedStonebricksCount { get { return collectedStonebricksCount; } }
    public int CollectedMetalingotsCount { get { return collectedMetalingotsCount; } }
    public int CollectedCursedingotsCount { get { return collectedCursedingotsCount; } }
    public int CollectedEarthstonedustCount { get { return collectedEarthstonedustCount; } }
    public int CollectedLavastonedustCount { get { return collectedLavastonedustCount; } }
    public int CollectedMagicstonedustCount { get { return collectedMagicstonedustCount; } }
    public int CollectedWaterstonedustCount { get { return collectedWaterstonedustCount; } }
    public int CollectedWindstonedustCount { get { return collectedWindstonedustCount; } }


    public event Action<int> coinsCollected = delegate { };
    public event Action<int> stoneoreCollected = delegate { };
    public event Action<int> metaloreCollected = delegate { };
    public event Action<int> cursedoreCollected = delegate { };
    public event Action<int> earthstoneoreCollected = delegate { };
    public event Action<int> lavastoneoreCollected = delegate { };
    public event Action<int> magicstoneoreCollected = delegate { };
    public event Action<int> waterstoneoreCollected = delegate { };
    public event Action<int> windstoneoreCollected = delegate { };
    public event Action<int> stonehandsCollected = delegate { };
    public event Action<int> leggingsCollected = delegate { };
    public event Action<int> platearmorsCollected = delegate { };
    public event Action<int> shoesCollected = delegate { };
    public event Action<int> helmsCollected = delegate { };
    public event Action<int> glovesCollected = delegate { };
    public event Action<int> bracersCollected = delegate { };
    public event Action<int> stonebricksCollected = delegate { };
    public event Action<int> metalingotsCollected = delegate { };
    public event Action<int> cursedingotsCollected = delegate { };
    public event Action<int> earthstonedustCollected = delegate { };
    public event Action<int> lavastonedustCollected = delegate { };
    public event Action<int> magicstonedustCollected = delegate { };
    public event Action<int> waterstonedustCollected = delegate { };
    public event Action<int> windstonedustCollected = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void countQuestItem(int id)
    {
        switch (id)
        {
            case 1:
                collectedCoinsCount++;
                if (coinsCollected != null) { coinsCollected(collectedCoinsCount); }
                //goldcoins
                break;
            case 2:
                collectedStoneoreCount++;
                if (stoneoreCollected != null) { stoneoreCollected(collectedStoneoreCount); }
                //stoneore
                break;
            case 3:
                collectedMetaloreCount++;
                if (metaloreCollected != null) { metaloreCollected(collectedMetaloreCount); }
                //metalOre
                break;
            case 4:
                collectedCursedoreCount++;
                if (cursedoreCollected != null) { cursedoreCollected(collectedCursedoreCount); }
                //cursedOre
                break;
            case 5:
                collectedEarthstoneoreCount++;
                if (earthstoneoreCollected != null) { earthstoneoreCollected(collectedEarthstoneoreCount); }
                //earthStoneOre
                break;
            case 6:
                collectedLavastoneoreCount++;
                if (lavastoneoreCollected != null) { lavastoneoreCollected(collectedLavastoneoreCount); }
                //lavaStoneOre
                break;
            case 7:
                collectedMagicstoneoreCount++;
                if (magicstoneoreCollected != null) { magicstoneoreCollected(collectedMagicstoneoreCount); }
                //magicStoneOre
                break;
            case 8:
                collectedWaterstoneoreCount++;
                if (waterstoneoreCollected != null) { waterstoneoreCollected(collectedWaterstoneoreCount); }
                //waterStoneOre
                break;
            case 9:
                collectedWindstoneoreCount++;
                if (windstoneoreCollected != null) { windstoneoreCollected(collectedWindstoneoreCount); }
                //windStoneOre
                break;
            case 11:
                collectedStonehandsCount++;
                if (stonehandsCollected != null) { stonehandsCollected(collectedStonehandsCount); }
                //stoneHands
                break;
            case 12:
                collectedLeggingsCount++;
                if (leggingsCollected != null) { leggingsCollected(collectedLeggingsCount); }
                //leggings
                break;
            case 13:
                collectedPlatearmorCount++;
                if (platearmorsCollected != null) { platearmorsCollected(collectedPlatearmorCount); }
                //plateArmor
                break;
            case 14:
                collectedShoesCount++;
                if (shoesCollected != null) { shoesCollected(collectedShoesCount); }
                //shoes
                break;
            case 15:
                collectedHelmsCount++;
                if (helmsCollected != null) { helmsCollected(collectedHelmsCount); }
                //helm
                break;
            case 16:
                collectedGlovesCount++;
                if (glovesCollected != null) { glovesCollected(collectedGlovesCount); }
                //gloves
                break;
            case 17:
                collectedBracersCount++;
                if (bracersCollected != null) { bracersCollected(collectedBracersCount); }
                //bracers
                break;
            case 18:
                collectedStonebricksCount++;
                if (stonebricksCollected != null) { stonebricksCollected(collectedStonebricksCount); }
                //stoneBrick
                break;
            case 19:
                collectedMetalingotsCount++;
                if (metalingotsCollected != null) { metalingotsCollected(collectedMetalingotsCount); }
                //metalIngot
                break;
            case 20:
                collectedCursedingotsCount++;
                if (cursedingotsCollected != null) { cursedingotsCollected(collectedCursedingotsCount); }
                //cursedIngot
                break;
            case 21:
                collectedEarthstonedustCount++;
                if (earthstonedustCollected != null) { earthstonedustCollected(collectedEarthstonedustCount); }
                //earthStoneDust
                break;
            case 22:
                collectedLavastonedustCount++;
                if (lavastonedustCollected != null) { lavastonedustCollected(collectedLavastonedustCount); }
                //lavastoneDust
                break;
            case 23:
                collectedMagicstonedustCount++;
                if (magicstonedustCollected != null) { magicstonedustCollected(collectedMagicstonedustCount); }
                //magicStoneDust
                break;
            case 24:
                collectedWaterstonedustCount++;
                if (waterstonedustCollected != null) { waterstonedustCollected(collectedWaterstonedustCount); }
                //waterStoneDust
                break;
            case 25:
                collectedWindstonedustCount++;
                if (windstonedustCollected != null) { windstonedustCollected(collectedWindstonedustCount); }
                //windStoneDust
                break;

        }
    }


    public void countDefractedQuest(int id)
    {
        switch (id)
        {
            case 18:
                collectedStonebricksCount++;
                if (stonebricksCollected != null) { stonebricksCollected(collectedStonebricksCount); }
                //stoneBrick
                break;
            case 19:
                collectedMetalingotsCount++;
                if (metalingotsCollected != null) { metalingotsCollected(collectedMetalingotsCount); }
                //metalIngot
                break;
            case 20:
                collectedCursedingotsCount++;
                if (cursedingotsCollected != null) { cursedingotsCollected(collectedCursedingotsCount); }
                //cursedIngot
                break;
            case 21:
                collectedEarthstonedustCount++;
                if (earthstonedustCollected != null) { earthstonedustCollected(collectedEarthstonedustCount); }
                //earthStoneDust
                break;
            case 22:
                collectedLavastonedustCount++;
                if (lavastonedustCollected != null) { lavastonedustCollected(collectedLavastonedustCount); }
                //lavastoneDust
                break;
            case 23:
                collectedMagicstonedustCount++;
                if (magicstonedustCollected != null) { magicstonedustCollected(collectedMagicstonedustCount); }
                //magicStoneDust
                break;
            case 24:
                collectedWaterstonedustCount++;
                if (waterstonedustCollected != null) { waterstonedustCollected(collectedWaterstonedustCount); }
                //waterStoneDust
                break;
            case 25:
                collectedWindstonedustCount++;
                if (windstonedustCollected != null) { windstonedustCollected(collectedWindstonedustCount); }
                //windStoneDust
                break;

        }
    }
}
