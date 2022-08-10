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
    int createdStonehandsCount;
    int createdLeggingsCount;
    int createdPlatearmorCount;
    int createdShoesCount;
    int createdHelmsCount;
    int createdGlovesCount;
    int createdBracersCount;
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
    public int CreatedStonehandsCount { get { return createdStonehandsCount; } }
    public int CreatedLeggingsCount { get { return createdLeggingsCount; } }
    public int CreatedPlatearmorCount { get { return createdPlatearmorCount; } }
    public int CreatedShoesCount { get { return createdShoesCount; } }
    public int CreatedHelmsCount { get { return createdHelmsCount; } }
    public int CreatedGlovesCount { get { return createdGlovesCount; } }
    public int CreatedBracersCount { get { return createdBracersCount; } }
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
    public event Action<int> stonehandsCreated = delegate { };
    public event Action<int> leggingsCreated = delegate { };
    public event Action<int> platearmorsCreated = delegate { };
    public event Action<int> shoesCreated = delegate { };
    public event Action<int> helmsCreated = delegate { };
    public event Action<int> glovesCreated = delegate { };
    public event Action<int> bracersCreated = delegate { };
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

    public void countCreatedQuest(int id)
    {
        Debug.Log(id);
        switch (id)
        {
            case 11:
                createdStonehandsCount++;
                if (stonehandsCreated != null) { stonehandsCreated(createdStonehandsCount); Debug.Log("stone hands created"); }
                //stoneHands
                break;
            case 12:
                createdLeggingsCount++;
                if (leggingsCreated != null) { leggingsCreated(createdLeggingsCount); }
                //leggings
                break;
            case 13:
                createdPlatearmorCount++;
                if (platearmorsCreated != null) { platearmorsCreated(createdPlatearmorCount); }
                //plateArmor
                break;
            case 14:
                createdShoesCount++;
                if (shoesCreated != null) { shoesCreated(createdShoesCount); }
                //shoes
                break;
            case 15:
                createdHelmsCount++;
                if (helmsCreated != null) { helmsCreated(createdHelmsCount); }
                //helm
                break;
            case 16:
                createdGlovesCount++;
                if (glovesCreated != null) { glovesCreated(createdGlovesCount); }
                //gloves
                break;
            case 17:
                createdBracersCount++;
                if (bracersCreated != null) { bracersCreated(createdBracersCount); }
                //bracers
                break;
        }
    }
}
