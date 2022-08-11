using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCounterQuests : MonoBehaviour
{
    int collectedCoinsCount;
    int collectedStoneoreCount;
    int transmutatedStoneoreCount;
    int collectedMetaloreCount;
    int transmutatedMetaloreCount;
    int collectedCursedoreCount;
    int transmutatedCursedoreCount;
    int collectedEarthstoneoreCount;
    int transmutatedEarthstoneoreCount;
    int collectedLavastoneoreCount;
    int transmutatedLavastoneoreCount;
    int collectedMagicstoneoreCount;
    int transmutatedMagicstoneoreCount;
    int collectedWaterstoneoreCount;
    int transmutatedWaterstoneoreCount;
    int collectedWindstoneoreCount;
    int transmutatedWindstoneoreCount;
    int createdStonehandsCount;
    int transmutatedStonehandsCount;
    int createdLeggingsCount;
    int transmutatedLeggingsCount;
    int createdPlatearmorCount;
    int transmutatedPlatearmorCount;
    int createdShoesCount;
    int transmutatedShoesCount;
    int createdHelmsCount;
    int transmutatedHelmsCount;
    int createdGlovesCount;
    int transmutatedGlovesCount;
    int createdBracersCount;
    int transmutatedBracersCount;
    int collectedStonebricksCount;
    int transmutatedStonebricksCount;
    int collectedMetalingotsCount;
    int transmutatedMetalingotsCount;
    int collectedCursedingotsCount;
    int transmutatedCursedingotsCount;
    int collectedEarthstonedustCount;
    int transmutatedEarthstonedustCount;
    int collectedLavastonedustCount;
    int transmutatedLavastonedustCount;
    int collectedMagicstonedustCount;
    int transmutatedMagicstonedustCount;
    int collectedWaterstonedustCount;
    int transmutatedWaterstonedustCount;
    int collectedWindstonedustCount;
    int transmutatedWindstonedustCount;

    public int CollectedCoinsCount { get { return collectedCoinsCount; } }
    public int CollectedStoneoreCount { get { return collectedStoneoreCount; } }
    public int TransmutatedStoneoreCount { get { return transmutatedStoneoreCount; } }
    public int CollectedMetaloreCount { get { return collectedMetaloreCount; } }
    public int TransmutatedMetaloreCount { get { return transmutatedMetaloreCount; } }
    public int CollectedCursedoreCount { get { return collectedCursedoreCount; } }
    public int TransmutatedCursedoreCount { get { return transmutatedCursedoreCount; } }
    public int CollectedEarthstoneoreCount { get { return collectedEarthstoneoreCount; } }
    public int TransmutatedEarthstoneoreCount { get { return transmutatedEarthstoneoreCount; } }
    public int CollectedLavastoneoreCount { get { return collectedLavastoneoreCount; } }
    public int TransmutatedLavastoneoreCount { get { return transmutatedLavastoneoreCount; } }
    public int CollectedMagicstoneoreCount { get { return collectedMagicstoneoreCount; } }
    public int TransmutatedMagicstoneoreCount { get { return transmutatedMagicstoneoreCount; } }
    public int CollectedWaterstoneoreCount { get { return collectedWaterstoneoreCount; } }
    public int TransmutatedWaterstoneoreCount { get { return transmutatedWaterstoneoreCount; } }
    public int CollectedWindstoneoreCount { get { return collectedWindstoneoreCount; } }
    public int TransmutatedWindstoneoreCount { get { return transmutatedWindstoneoreCount; } }
    public int CreatedStonehandsCount { get { return createdStonehandsCount; } }
    public int TransmutatedStonehandsCount { get { return transmutatedStonehandsCount; } }
    public int CreatedLeggingsCount { get { return createdLeggingsCount; } }
    public int TransmutatedLeggingsCount { get { return transmutatedLeggingsCount; } }
    public int CreatedPlatearmorCount { get { return createdPlatearmorCount; } }
    public int TransmutatedPlatearmorCount { get { return transmutatedPlatearmorCount; } }
    public int CreatedShoesCount { get { return createdShoesCount; } }
    public int TransmutatedShoesCount { get { return transmutatedShoesCount; } }
    public int CreatedHelmsCount { get { return createdHelmsCount; } }
    public int TransmutatedHelmsCount { get { return transmutatedHelmsCount; } }
    public int CreatedGlovesCount { get { return createdGlovesCount; } }
    public int TransmutatedGlovesCount { get { return transmutatedGlovesCount; } }
    public int CreatedBracersCount { get { return createdBracersCount; } }
    public int TransmutatedBracersCount { get { return transmutatedBracersCount; } }
    public int CollectedStonebricksCount { get { return collectedStonebricksCount; } }
    public int TransmutatedStonebricksCount { get { return transmutatedStonebricksCount; } }
    public int CollectedMetalingotsCount { get { return collectedMetalingotsCount; } }
    public int TransmutatedMetalingotsCount { get { return transmutatedMetalingotsCount; } }
    public int CollectedCursedingotsCount { get { return collectedCursedingotsCount; } }
    public int TransmutatedCursedingotsCount { get { return transmutatedCursedingotsCount; } }
    public int CollectedEarthstonedustCount { get { return collectedEarthstonedustCount; } }
    public int TransmutatedEarthstonedustCount { get { return transmutatedEarthstonedustCount; } }
    public int CollectedLavastonedustCount { get { return collectedLavastonedustCount; } }
    public int TransmutatedLavastonedustCount { get { return transmutatedLavastonedustCount; } }
    public int CollectedMagicstonedustCount { get { return collectedMagicstonedustCount; } }
    public int TransmutatedMagicstonedustCount { get { return transmutatedMagicstonedustCount; } }
    public int CollectedWaterstonedustCount { get { return collectedWaterstonedustCount; } }
    public int TransmutatedWaterstonedustCount { get { return transmutatedWaterstonedustCount; } }
    public int CollectedWindstonedustCount { get { return collectedWindstonedustCount; } }
    public int TransmutatedWindstonedustCount { get { return transmutatedWindstonedustCount; } }


    public event Action<int> coinsCollected = delegate { };
    public event Action<int> stoneoreCollected = delegate { };
    public event Action<int> stoneoreTransmutated = delegate { };
    public event Action<int> metaloreCollected = delegate { };
    public event Action<int> metaloreTransmutated = delegate { };
    public event Action<int> cursedoreCollected = delegate { };
    public event Action<int> cursedoreTransmutated = delegate { };
    public event Action<int> earthstoneoreCollected = delegate { };
    public event Action<int> earthstoneoreTransmutated = delegate { };
    public event Action<int> lavastoneoreCollected = delegate { };
    public event Action<int> lavastoneoreTransmutated = delegate { };
    public event Action<int> magicstoneoreCollected = delegate { };
    public event Action<int> magicstoneoreTransmutated = delegate { };
    public event Action<int> waterstoneoreCollected = delegate { };
    public event Action<int> waterstoneoreTransmutated = delegate { };
    public event Action<int> windstoneoreCollected = delegate { };
    public event Action<int> windstoneoreTransmutated = delegate { };
    public event Action<int> stonehandsCreated = delegate { };
    public event Action<int> stonehandsTransmutated = delegate { };
    public event Action<int> leggingsCreated = delegate { };
    public event Action<int> leggingsTransmutated = delegate { };
    public event Action<int> platearmorsCreated = delegate { };
    public event Action<int> platearmorsTransmutated = delegate { };
    public event Action<int> shoesCreated = delegate { };
    public event Action<int> shoesTransmutated = delegate { };
    public event Action<int> helmsCreated = delegate { };
    public event Action<int> helmsTransmutated = delegate { };
    public event Action<int> glovesCreated = delegate { };
    public event Action<int> glovesTransmutated = delegate { };
    public event Action<int> bracersCreated = delegate { };
    public event Action<int> bracersTransmutated = delegate { };
    public event Action<int> stonebricksCollected = delegate { };
    public event Action<int> stonebricksTransmutated = delegate { };
    public event Action<int> metalingotsCollected = delegate { };
    public event Action<int> metalingotsTransmutated = delegate { };
    public event Action<int> cursedingotsCollected = delegate { };
    public event Action<int> cursedingotsTransmutated = delegate { };
    public event Action<int> earthstonedustCollected = delegate { };
    public event Action<int> earthstonedustTransmutated = delegate { };
    public event Action<int> lavastonedustCollected = delegate { };
    public event Action<int> lavastonedustTransmutated = delegate { };
    public event Action<int> magicstonedustCollected = delegate { };
    public event Action<int> magicstonedustTransmutated = delegate { };
    public event Action<int> waterstonedustCollected = delegate { };
    public event Action<int> waterstonedustTransmutated = delegate { };
    public event Action<int> windstonedustCollected = delegate { };
    public event Action<int> windstonedustTransmutated = delegate { };
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


    public void countMidasedItem(int id)
    {
        switch (id)
        {
            case 2:
                transmutatedStoneoreCount++;
                if (stoneoreTransmutated != null) { stoneoreTransmutated(transmutatedStoneoreCount); }
                //stoneore
                break;
            case 3:
                transmutatedMetaloreCount++;
                if (metaloreTransmutated != null) { metaloreTransmutated(transmutatedMetaloreCount); }
                //metalOre
                break;
            case 4:
                transmutatedCursedoreCount++;
                if (cursedoreTransmutated != null) { cursedoreTransmutated(transmutatedCursedoreCount); }
                //cursedOre
                break;
            case 5:
                transmutatedEarthstoneoreCount++;
                if (earthstoneoreTransmutated != null) { earthstoneoreTransmutated(transmutatedEarthstoneoreCount); }
                //earthStoneOre
                break;
            case 6:
                transmutatedLavastoneoreCount++;
                if (lavastoneoreTransmutated != null) { lavastoneoreTransmutated(transmutatedLavastoneoreCount); }
                //lavaStoneOre
                break;
            case 7:
                transmutatedMagicstoneoreCount++;
                if (magicstoneoreTransmutated != null) { magicstoneoreTransmutated(transmutatedMagicstoneoreCount); }
                //magicStoneOre
                break;
            case 8:
                transmutatedWaterstoneoreCount++;
                if (waterstoneoreTransmutated != null) { waterstoneoreTransmutated(transmutatedWaterstoneoreCount); }
                //waterStoneOre
                break;
            case 9:
                transmutatedWindstoneoreCount++;
                if (windstoneoreTransmutated != null) { windstoneoreTransmutated(transmutatedWindstoneoreCount); }
                //windStoneOre
                break;
            case 11:
                transmutatedStonehandsCount++;
                if (stonehandsTransmutated != null) { stonehandsTransmutated(transmutatedStonehandsCount); }
                //stoneHands
                break;
            case 12:
                transmutatedLeggingsCount++;
                if (leggingsTransmutated != null) { leggingsTransmutated(transmutatedLeggingsCount); }
                //leggings
                break;
            case 13:
                transmutatedPlatearmorCount++;
                if (platearmorsTransmutated != null) { platearmorsTransmutated(transmutatedPlatearmorCount); }
                //plateArmor
                break;
            case 14:
                transmutatedShoesCount++;
                if (shoesTransmutated != null) { shoesTransmutated(transmutatedShoesCount); }
                //shoes
                break;
            case 15:
                transmutatedHelmsCount++;
                if (helmsTransmutated != null) { helmsTransmutated(transmutatedHelmsCount); }
                //helm
                break;
            case 16:
                transmutatedGlovesCount++;
                if (glovesTransmutated != null) { glovesTransmutated(transmutatedGlovesCount); }
                //gloves
                break;
            case 17:
                transmutatedBracersCount++;
                if (bracersTransmutated != null) { bracersTransmutated(transmutatedBracersCount); }
                //bracers
                break;
            case 18:
                transmutatedStonebricksCount++;
                if (stonebricksTransmutated != null) { stonebricksTransmutated(transmutatedStonebricksCount); }
                //stoneBrick
                break;
            case 19:
                transmutatedMetalingotsCount++;
                if (metalingotsTransmutated != null) { metalingotsTransmutated(transmutatedMetalingotsCount); }
                //metalIngot
                break;
            case 20:
                transmutatedCursedingotsCount++;
                if (cursedingotsTransmutated != null) { cursedingotsTransmutated(transmutatedCursedingotsCount); }
                //cursedIngot
                break;
            case 21:
                transmutatedEarthstonedustCount++;
                if (earthstonedustTransmutated != null) { earthstonedustTransmutated(transmutatedEarthstonedustCount); }
                //earthStoneDust
                break;
            case 22:
                transmutatedLavastonedustCount++;
                if (lavastonedustTransmutated != null) { lavastonedustTransmutated(transmutatedLavastonedustCount); }
                //lavastoneDust
                break;
            case 23:
                transmutatedMagicstonedustCount++;
                if (magicstonedustTransmutated != null) { magicstonedustTransmutated(transmutatedMagicstonedustCount); }
                //magicStoneDust
                break;
            case 24:
                transmutatedWaterstonedustCount++;
                if (waterstonedustTransmutated != null) { waterstonedustTransmutated(transmutatedWaterstonedustCount); }
                //waterStoneDust
                break;
            case 25:
                transmutatedWindstonedustCount++;
                if (windstonedustTransmutated != null) { windstonedustTransmutated(transmutatedWindstonedustCount); }
                //windStoneDust
                break;

        }
    }

}
