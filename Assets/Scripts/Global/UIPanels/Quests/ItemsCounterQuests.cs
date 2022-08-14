using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCounterQuests : MonoBehaviour
{
    int collectedThingsCount;
    int defractedThingsCount;
    int createdThingsCount;
    int transmutatedOreCount;
    int transmutatedProcessedCount;
    int transmutatedWearablesCount;
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

    public int CollectedThingsCount { get { return collectedThingsCount; } }
    public int DefractedThingsCount { get { return defractedThingsCount; } }
    public int CreatedThingsCount { get { return createdThingsCount; } }
    public int TransmutatedOreCount { get { return transmutatedOreCount; } }
    public int TransmutatedProcessedCount { get { return transmutatedProcessedCount; } }
    public int TransmutatedWearablesCount { get { return transmutatedWearablesCount; } }
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


    public event Action<int> thingsCollected = delegate { };
    public event Action<int> thingsDefracted = delegate { };
    public event Action<int> thingsCreated = delegate { };
    public event Action<int> oreTransmutated = delegate { };
    public event Action<int> processedTransmutated = delegate { };
    public event Action<int> wearableTransmutated = delegate { };
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
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedCoinsCount++;
                if (coinsCollected != null) { coinsCollected(collectedCoinsCount); }
                //goldcoins
                break;
            case 2:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedStoneoreCount++;
                if (stoneoreCollected != null) { stoneoreCollected(collectedStoneoreCount); }
                //stoneore
                break;
            case 3:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedMetaloreCount++;
                if (metaloreCollected != null) { metaloreCollected(collectedMetaloreCount); }
                //metalOre
                break;
            case 4:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedCursedoreCount++;
                if (cursedoreCollected != null) { cursedoreCollected(collectedCursedoreCount); }
                //cursedOre
                break;
            case 5:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedEarthstoneoreCount++;
                if (earthstoneoreCollected != null) { earthstoneoreCollected(collectedEarthstoneoreCount); }
                //earthStoneOre
                break;
            case 6:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedLavastoneoreCount++;
                if (lavastoneoreCollected != null) { lavastoneoreCollected(collectedLavastoneoreCount); }
                //lavaStoneOre
                break;
            case 7:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedMagicstoneoreCount++;
                if (magicstoneoreCollected != null) { magicstoneoreCollected(collectedMagicstoneoreCount); }
                //magicStoneOre
                break;
            case 8:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedWaterstoneoreCount++;
                if (waterstoneoreCollected != null) { waterstoneoreCollected(collectedWaterstoneoreCount); }
                //waterStoneOre
                break;
            case 9:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedWindstoneoreCount++;
                if (windstoneoreCollected != null) { windstoneoreCollected(collectedWindstoneoreCount); }
                //windStoneOre
                break;
            case 18:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedStonebricksCount++;
                if (stonebricksCollected != null) { stonebricksCollected(collectedStonebricksCount); }
                //stoneBrick
                break;
            case 19:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedMetalingotsCount++;
                if (metalingotsCollected != null) { metalingotsCollected(collectedMetalingotsCount); }
                //metalIngot
                break;
            case 20:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedCursedingotsCount++;
                if (cursedingotsCollected != null) { cursedingotsCollected(collectedCursedingotsCount); }
                //cursedIngot
                break;
            case 21:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedEarthstonedustCount++;
                if (earthstonedustCollected != null) { earthstonedustCollected(collectedEarthstonedustCount); }
                //earthStoneDust
                break;
            case 22:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedLavastonedustCount++;
                if (lavastonedustCollected != null) { lavastonedustCollected(collectedLavastonedustCount); }
                //lavastoneDust
                break;
            case 23:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedMagicstonedustCount++;
                if (magicstonedustCollected != null) { magicstonedustCollected(collectedMagicstonedustCount); }
                //magicStoneDust
                break;
            case 24:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
                collectedWaterstonedustCount++;
                if (waterstonedustCollected != null) { waterstonedustCollected(collectedWaterstonedustCount); }
                //waterStoneDust
                break;
            case 25:
                collectedThingsCount++; if (thingsCollected != null) { thingsCollected(collectedThingsCount); }
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
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
                collectedStonebricksCount++;
                if (stonebricksCollected != null) { stonebricksCollected(collectedStonebricksCount); }
                //stoneBrick
                break;
            case 19:
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
                collectedMetalingotsCount++;
                if (metalingotsCollected != null) { metalingotsCollected(collectedMetalingotsCount); }
                //metalIngot
                break;
            case 20:
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
                collectedCursedingotsCount++;
                if (cursedingotsCollected != null) { cursedingotsCollected(collectedCursedingotsCount); }
                //cursedIngot
                break;
            case 21:
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
                collectedEarthstonedustCount++;
                if (earthstonedustCollected != null) { earthstonedustCollected(collectedEarthstonedustCount); }
                //earthStoneDust
                break;
            case 22:
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
                collectedLavastonedustCount++;
                if (lavastonedustCollected != null) { lavastonedustCollected(collectedLavastonedustCount); }
                //lavastoneDust
                break;
            case 23:
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
                collectedMagicstonedustCount++;
                if (magicstonedustCollected != null) { magicstonedustCollected(collectedMagicstonedustCount); }
                //magicStoneDust
                break;
            case 24:
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
                collectedWaterstonedustCount++;
                if (waterstonedustCollected != null) { waterstonedustCollected(collectedWaterstonedustCount); }
                //waterStoneDust
                break;
            case 25:
                defractedThingsCount++; if (thingsDefracted != null) { thingsDefracted(defractedThingsCount); }
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
                createdThingsCount++; if (thingsCreated != null) { thingsCreated(createdThingsCount); }
                createdStonehandsCount++;
                if (stonehandsCreated != null) { stonehandsCreated(createdStonehandsCount); Debug.Log("stone hands created"); }
                //stoneHands
                break;
            case 12:
                createdThingsCount++; if (thingsCreated != null) { thingsCreated(createdThingsCount); }
                createdLeggingsCount++;
                if (leggingsCreated != null) { leggingsCreated(createdLeggingsCount); }
                //leggings
                break;
            case 13:
                createdThingsCount++; if (thingsCreated != null) { thingsCreated(createdThingsCount); }
                createdPlatearmorCount++;
                if (platearmorsCreated != null) { platearmorsCreated(createdPlatearmorCount); }
                //plateArmor
                break;
            case 14:
                createdThingsCount++; if (thingsCreated != null) { thingsCreated(createdThingsCount); }
                createdShoesCount++;
                if (shoesCreated != null) { shoesCreated(createdShoesCount); }
                //shoes
                break;
            case 15:
                createdThingsCount++; if (thingsCreated != null) { thingsCreated(createdThingsCount); }
                createdHelmsCount++;
                if (helmsCreated != null) { helmsCreated(createdHelmsCount); }
                //helm
                break;
            case 16:
                createdThingsCount++; if (thingsCreated != null) { thingsCreated(createdThingsCount); }
                createdGlovesCount++;
                if (glovesCreated != null) { glovesCreated(createdGlovesCount); }
                //gloves
                break;
            case 17:
                createdThingsCount++; if (thingsCreated != null) { thingsCreated(createdThingsCount); }
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
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }
                transmutatedStoneoreCount++;
                if (stoneoreTransmutated != null) { stoneoreTransmutated(transmutatedStoneoreCount); }
                //stoneore
                break;
            case 3:
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }
                transmutatedMetaloreCount++;
                if (metaloreTransmutated != null) { metaloreTransmutated(transmutatedMetaloreCount); }
                //metalOre
                break;
            case 4:
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }
                transmutatedCursedoreCount++;
                if (cursedoreTransmutated != null) { cursedoreTransmutated(transmutatedCursedoreCount); }
                //cursedOre
                break;
            case 5:
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }
                transmutatedEarthstoneoreCount++;
                if (earthstoneoreTransmutated != null) { earthstoneoreTransmutated(transmutatedEarthstoneoreCount); }
                //earthStoneOre
                break;
            case 6:
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }
                transmutatedLavastoneoreCount++;
                if (lavastoneoreTransmutated != null) { lavastoneoreTransmutated(transmutatedLavastoneoreCount); }
                //lavaStoneOre
                break;
            case 7:
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }
                transmutatedMagicstoneoreCount++;
                if (magicstoneoreTransmutated != null) { magicstoneoreTransmutated(transmutatedMagicstoneoreCount); }
                //magicStoneOre
                break;
            case 8:
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }
                transmutatedWaterstoneoreCount++;
                if (waterstoneoreTransmutated != null) { waterstoneoreTransmutated(transmutatedWaterstoneoreCount); }
                //waterStoneOre
                break;
            case 9:
                transmutatedOreCount++; if (oreTransmutated != null) { oreTransmutated(transmutatedOreCount); }         
                transmutatedWindstoneoreCount++;
                if (windstoneoreTransmutated != null) { windstoneoreTransmutated(transmutatedWindstoneoreCount); }
                //windStoneOre
                break;
            case 11:
                transmutatedWearablesCount++; if (wearableTransmutated != null) { wearableTransmutated(transmutatedWearablesCount); }
                transmutatedStonehandsCount++;
                if (stonehandsTransmutated != null) { stonehandsTransmutated(transmutatedStonehandsCount); }
                //stoneHands
                break;
            case 12:
                transmutatedWearablesCount++; if (wearableTransmutated != null) { wearableTransmutated(transmutatedWearablesCount); }
                transmutatedLeggingsCount++;
                if (leggingsTransmutated != null) { leggingsTransmutated(transmutatedLeggingsCount); }
                //leggings
                break;
            case 13:
                transmutatedWearablesCount++; if (wearableTransmutated != null) { wearableTransmutated(transmutatedWearablesCount); }
                transmutatedPlatearmorCount++;
                if (platearmorsTransmutated != null) { platearmorsTransmutated(transmutatedPlatearmorCount); }
                //plateArmor
                break;
            case 14:
                transmutatedWearablesCount++; if (wearableTransmutated != null) { wearableTransmutated(transmutatedWearablesCount); }
                transmutatedShoesCount++;
                if (shoesTransmutated != null) { shoesTransmutated(transmutatedShoesCount); }
                //shoes
                break;
            case 15:
                transmutatedWearablesCount++; if (wearableTransmutated != null) { wearableTransmutated(transmutatedWearablesCount); }
                transmutatedHelmsCount++;
                if (helmsTransmutated != null) { helmsTransmutated(transmutatedHelmsCount); }
                //helm
                break;
            case 16:
                transmutatedWearablesCount++; if (wearableTransmutated != null) { wearableTransmutated(transmutatedWearablesCount); }
                transmutatedGlovesCount++;
                if (glovesTransmutated != null) { glovesTransmutated(transmutatedGlovesCount); }
                //gloves
                break;
            case 17:
                transmutatedWearablesCount++; if (wearableTransmutated != null) { wearableTransmutated(transmutatedWearablesCount); }
                transmutatedBracersCount++;
                if (bracersTransmutated != null) { bracersTransmutated(transmutatedBracersCount); }
                //bracers
                break;
            case 18:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }
                transmutatedStonebricksCount++;
                if (stonebricksTransmutated != null) { stonebricksTransmutated(transmutatedStonebricksCount); }
                //stoneBrick
                break;
            case 19:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }
                transmutatedMetalingotsCount++;
                if (metalingotsTransmutated != null) { metalingotsTransmutated(transmutatedMetalingotsCount); }
                //metalIngot
                break;
            case 20:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }
                transmutatedCursedingotsCount++;
                if (cursedingotsTransmutated != null) { cursedingotsTransmutated(transmutatedCursedingotsCount); }
                //cursedIngot
                break;
            case 21:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }
                transmutatedEarthstonedustCount++;
                if (earthstonedustTransmutated != null) { earthstonedustTransmutated(transmutatedEarthstonedustCount); }
                //earthStoneDust
                break;
            case 22:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }
                transmutatedLavastonedustCount++;
                if (lavastonedustTransmutated != null) { lavastonedustTransmutated(transmutatedLavastonedustCount); }
                //lavastoneDust
                break;
            case 23:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }
                transmutatedMagicstonedustCount++;
                if (magicstonedustTransmutated != null) { magicstonedustTransmutated(transmutatedMagicstonedustCount); }
                //magicStoneDust
                break;
            case 24:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }
                transmutatedWaterstonedustCount++;
                if (waterstonedustTransmutated != null) { waterstonedustTransmutated(transmutatedWaterstonedustCount); }
                //waterStoneDust
                break;
            case 25:
                transmutatedProcessedCount++; if (processedTransmutated != null) { processedTransmutated(transmutatedProcessedCount); }                                                                                 
                transmutatedWindstonedustCount++;
                if (windstonedustTransmutated != null) { windstonedustTransmutated(transmutatedWindstonedustCount); }
                //windStoneDust
                break;

        }
    }

}
