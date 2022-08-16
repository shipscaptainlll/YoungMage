using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsSubscriptionsManager : MonoBehaviour
{
    [SerializeField] PersonMovement personMovement;
    [SerializeField] GameTimeManager gameTimeManager;
    [SerializeField] GraphicsPanel graphicsPanel;
    [SerializeField] AudioPanel audioPanel;
    [SerializeField] ControlsPanel controlsPanel;
    [SerializeField] MiscPanel miscPanel;
    [SerializeField] MineCharacterCatcher mineCharacterCatcher;
    [SerializeField] ContactedSkeletonsCounter contactedSkeletonsCounter;
    [SerializeField] DestroyedSkeletonsCounter destroyedSkeletonsCounter;
    [SerializeField] ItemsCounterQuests itemsCounterQuests;
    [SerializeField] TornadoInstantiator tornadoInstantiator;
    [SerializeField] CityCastleUpgrade cityCastleUpgrade;
    [SerializeField] CityWallUpgrade cityWallUpgrade;
    [SerializeField] CityBlacksmithUpgrade cityBlacksmithUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubscribeOnProgress(QuestElement questElement)
    {
        switch (questElement.Id)
        {
            case 0001:
                personMovement.CharacterStepMade += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameter);
                break;
            case 0002:
                break;
            case 0003:
                personMovement.CharacterRunnedStep += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterSecond);
                break;
            case 0004:
                personMovement.CharacterJumped += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterFourth);
                break;
            case 0005:
                personMovement.CharacterDoubleJumped += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterFifth);
                break;
            case 0007:
                personMovement.CharacterShifted += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterThird);
                break;
            case 0017:
                itemsCounterQuests.thingsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedThingsCount);
                break;
            case 0018:
                itemsCounterQuests.thingsDefracted += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.DefractedThingsCount);
                break;
            case 0019:
                itemsCounterQuests.oreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedOreCount);
                break;
            case 0020:
                itemsCounterQuests.processedTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedProcessedCount);
                break;
            case 0021:
                itemsCounterQuests.wearableTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedWearablesCount);
                break;
            case 0022:
                itemsCounterQuests.thingsCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedThingsCount);
                break;
            case 0027:
                graphicsPanel.SettingChanged += questElement.UpdateProgress;
                audioPanel.SettingChanged += questElement.UpdateProgress;
                controlsPanel.SettingChanged += questElement.UpdateProgress;
                miscPanel.SettingChanged += questElement.UpdateProgress;
                break;
            case 0028:
                gameTimeManager.MinutesIngamePassed += questElement.UpdateProgress;
                questElement.UpdateProgress(gameTimeManager.ProgressParameter);
                break;
            case 0029:
                tornadoInstantiator.TornadoInstantiatedQuests += questElement.UpdateProgress;
                questElement.UpdateProgress(tornadoInstantiator.TornadoCountQuests);
                break;
            case 0031:
                mineCharacterCatcher.CharacterEnteredDungeon += questElement.UpdateProgress;
                questElement.UpdateProgress(mineCharacterCatcher.ProgressParameter);
                break;
            case 0101:
                itemsCounterQuests.stoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedStoneoreCount);
                break;
            case 0102:
                itemsCounterQuests.stonebricksCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedStonebricksCount);
                break;
            case 0103:
                itemsCounterQuests.stoneoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedStoneoreCount);
                break;
            case 0104:
                itemsCounterQuests.stonebricksCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedStonebricksCount);
                break;
            case 0105:
                itemsCounterQuests.stonebricksTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedStonebricksCount);
                break;
            case 0106:
                itemsCounterQuests.stonehandsCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedStonehandsCount);
                break;
            case 0107:
                itemsCounterQuests.stonehandsTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedStonehandsCount);
                break;
            case 0201:
                itemsCounterQuests.metaloreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMetaloreCount);
                break;
            case 0202:
                itemsCounterQuests.metalingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMetalingotsCount);
                break;
            case 0203:
                itemsCounterQuests.metaloreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedMetaloreCount);
                break;
            case 0204:
                itemsCounterQuests.metalingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMetalingotsCount);
                break;
            case 0205:
                itemsCounterQuests.metalingotsTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedMetalingotsCount);
                break;
            case 0206:
                itemsCounterQuests.leggingsCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedLeggingsCount);
                break;
            case 0207:
                itemsCounterQuests.platearmorsCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedPlatearmorCount);
                break;
            case 0208:
                itemsCounterQuests.shoesCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedShoesCount);
                break;
            case 0209:
                itemsCounterQuests.helmsCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedHelmsCount);
                break;
            case 0210:
                itemsCounterQuests.leggingsTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedLeggingsCount);
                break;
            case 0211:
                itemsCounterQuests.platearmorsTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedPlatearmorCount);
                break;
            case 0212:
                itemsCounterQuests.shoesTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedShoesCount);
                break;
            case 0213:
                itemsCounterQuests.helmsTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedHelmsCount);
                break;
            case 0301:
                itemsCounterQuests.cursedoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedCursedoreCount);
                break;
            case 0302:
                itemsCounterQuests.cursedingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedCursedingotsCount);
                break;
            case 0303:
                itemsCounterQuests.cursedoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedCursedoreCount);
                break;
            case 0304:
                itemsCounterQuests.cursedingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedCursedingotsCount);
                break;
            case 0305:
                itemsCounterQuests.cursedingotsTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedCursedingotsCount);
                break;
            case 0306:
                itemsCounterQuests.bracersCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedBracersCount);
                break;
            case 0307:
                itemsCounterQuests.bracersTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedBracersCount);
                break;
            case 0401:
                itemsCounterQuests.magicstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMagicstoneoreCount);
                break;
            case 0402:
                itemsCounterQuests.magicstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMagicstonedustCount);
                break;
            case 0403:
                itemsCounterQuests.magicstoneoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedMagicstoneoreCount);
                break;
            case 0404:
                itemsCounterQuests.magicstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMagicstonedustCount);
                break;
            case 0405:
                itemsCounterQuests.magicstonedustTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedMagicstonedustCount);
                break;
            case 0406:
                itemsCounterQuests.glovesCreated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CreatedGlovesCount);
                break;
            case 0407:
                itemsCounterQuests.glovesTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedGlovesCount);
                break;
            case 0501:
                itemsCounterQuests.windstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWindstoneoreCount);
                break;
            case 0502:
                itemsCounterQuests.windstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWindstonedustCount);
                break;
            case 0503:
                itemsCounterQuests.windstoneoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedWindstoneoreCount);
                break;
            case 0504:
                itemsCounterQuests.windstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWindstonedustCount);
                break;
            case 0505:
                itemsCounterQuests.windstonedustTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedWindstonedustCount);
                break;
            case 0601:
                itemsCounterQuests.waterstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWaterstoneoreCount);
                break;
            case 0602:
                itemsCounterQuests.waterstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWaterstonedustCount);
                break;
            case 0603:
                itemsCounterQuests.waterstoneoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedWaterstoneoreCount);
                break;
            case 0604:
                itemsCounterQuests.waterstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWaterstonedustCount);
                break;
            case 0605:
                itemsCounterQuests.waterstonedustTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedWaterstonedustCount);
                break;
            case 0701:
                itemsCounterQuests.earthstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedEarthstoneoreCount);
                break;
            case 0702:
                itemsCounterQuests.earthstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedEarthstonedustCount);
                break;
            case 0703:
                itemsCounterQuests.earthstoneoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedEarthstoneoreCount);
                break;
            case 0704:
                itemsCounterQuests.earthstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedEarthstonedustCount);
                break;
            case 0705:
                itemsCounterQuests.earthstoneoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedEarthstonedustCount);
                break;
            case 0801:
                itemsCounterQuests.lavastoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedLavastoneoreCount);
                break;
            case 0802:
                itemsCounterQuests.lavastonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedLavastonedustCount);
                break;
            case 0803:
                itemsCounterQuests.lavastoneoreTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedLavastoneoreCount);
                break;
            case 0804:
                itemsCounterQuests.lavastonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedLavastonedustCount);
                break;
            case 0805:
                itemsCounterQuests.lavastonedustTransmutated += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.TransmutatedLavastonedustCount);
                break;
            case 1001:
                contactedSkeletonsCounter.ContactedSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(contactedSkeletonsCounter.ContactedSkeletonsCount);
                break;
            case 1002:
                contactedSkeletonsCounter.ContactedSmallSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(contactedSkeletonsCounter.CountSmallSkeletons);
                break;
            case 1006:
                destroyedSkeletonsCounter.DestroyedSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(destroyedSkeletonsCounter.DestroyedSkeletonsCount);
                break;
            case 1007:
                destroyedSkeletonsCounter.DestroyedSmallSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(destroyedSkeletonsCounter.CountSmallSkeletons);
                break;
            case 1101:
                cityWallUpgrade.HealthRegeneratedQuests += questElement.UpdateProgress;
                questElement.UpdateProgress(cityWallUpgrade.CountReneratedQuests);
                break;
            case 1103:
                cityBlacksmithUpgrade.BlacksmithUpgradedQuests += questElement.UpdateProgress;
                questElement.UpdateProgress(cityBlacksmithUpgrade.CountUpgradesQuests);
                break;
            case 1104:
                cityCastleUpgrade.CastleUpgradedQuests += questElement.UpdateProgress;
                questElement.UpdateProgress(cityCastleUpgrade.CountUpgradesQuests);
                break;
            case 1105:
                cityCastleUpgrade.ShardsUpgradedQuests += questElement.UpdateProgress;
                questElement.UpdateProgress(cityCastleUpgrade.CountShardsQuests);
                break;
        }
    }

    public void UnsubscribeFromProgress(QuestElement questElement)
    {
        switch (questElement.Id)
        {
            case 0001:
                personMovement.CharacterStepMade -= questElement.UpdateProgress;
                break;
            case 0002:
                break;
            case 0003:
                personMovement.CharacterRunnedStep -= questElement.UpdateProgress;
                break;
            case 0004:
                personMovement.CharacterJumped -= questElement.UpdateProgress;
                break;
            case 0005:
                personMovement.CharacterDoubleJumped -= questElement.UpdateProgress;
                break;
            case 0007:
                personMovement.CharacterShifted -= questElement.UpdateProgress;
                break;
            case 0017:
                itemsCounterQuests.thingsCollected -= questElement.UpdateProgress;
                break;
            case 0018:
                itemsCounterQuests.thingsDefracted -= questElement.UpdateProgress;
                break;
            case 0019:
                itemsCounterQuests.oreTransmutated -= questElement.UpdateProgress;
                break;
            case 0020:
                itemsCounterQuests.processedTransmutated -= questElement.UpdateProgress;
                break;
            case 0021:
                itemsCounterQuests.wearableTransmutated -= questElement.UpdateProgress;
                break;
            case 0022:
                itemsCounterQuests.thingsCreated -= questElement.UpdateProgress;
                break;
            case 0027:
                graphicsPanel.SettingChanged -= questElement.UpdateProgress;
                audioPanel.SettingChanged -= questElement.UpdateProgress;
                controlsPanel.SettingChanged -= questElement.UpdateProgress;
                miscPanel.SettingChanged -= questElement.UpdateProgress;
                break;
            case 0028:
                gameTimeManager.MinutesIngamePassed -= questElement.UpdateProgress;
                break;
            case 0029:
                tornadoInstantiator.TornadoInstantiatedQuests -= questElement.UpdateProgress;
                break;
            case 0031:
                mineCharacterCatcher.CharacterEnteredDungeon -= questElement.UpdateProgress;
                break;
            case 0101:
                itemsCounterQuests.stoneoreCollected -= questElement.UpdateProgress;
                break;
            case 0102:
                itemsCounterQuests.stonebricksCollected -= questElement.UpdateProgress;
                break;
            case 0103:
                itemsCounterQuests.stoneoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0104:
                itemsCounterQuests.stonebricksCollected -= questElement.UpdateProgress;
                break;
            case 0105:
                itemsCounterQuests.stonebricksTransmutated -= questElement.UpdateProgress;
                break;
            case 0106:
                itemsCounterQuests.stonehandsCreated -= questElement.UpdateProgress;
                break;
            case 0107:
                itemsCounterQuests.stonehandsTransmutated -= questElement.UpdateProgress;
                break;
            case 0201:
                itemsCounterQuests.metaloreCollected -= questElement.UpdateProgress;
                break;
            case 0202:
                itemsCounterQuests.metalingotsCollected -= questElement.UpdateProgress;
                break;
            case 0203:
                itemsCounterQuests.metaloreTransmutated -= questElement.UpdateProgress;
                break;
            case 0204:
                itemsCounterQuests.metalingotsCollected -= questElement.UpdateProgress;
                break;
            case 0205:
                itemsCounterQuests.metalingotsTransmutated -= questElement.UpdateProgress;
                break;
            case 0206:
                itemsCounterQuests.leggingsCreated -= questElement.UpdateProgress;
                break;
            case 0207:
                itemsCounterQuests.platearmorsCreated -= questElement.UpdateProgress;
                break;
            case 0208:
                itemsCounterQuests.shoesCreated -= questElement.UpdateProgress;
                break;
            case 0209:
                itemsCounterQuests.helmsCreated -= questElement.UpdateProgress;
                break;
            case 0210:
                itemsCounterQuests.leggingsTransmutated -= questElement.UpdateProgress;
                break;
            case 0211:
                itemsCounterQuests.platearmorsTransmutated -= questElement.UpdateProgress;
                break;
            case 0212:
                itemsCounterQuests.shoesTransmutated -= questElement.UpdateProgress;
                break;
            case 0213:
                itemsCounterQuests.helmsTransmutated -= questElement.UpdateProgress;
                break;
            case 0301:
                itemsCounterQuests.cursedoreCollected -= questElement.UpdateProgress;
                break;
            case 0302:
                itemsCounterQuests.cursedingotsCollected -= questElement.UpdateProgress;
                break;
            case 0303:
                itemsCounterQuests.cursedoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0304:
                itemsCounterQuests.cursedingotsCollected -= questElement.UpdateProgress;
                break;
            case 0305:
                itemsCounterQuests.cursedingotsTransmutated -= questElement.UpdateProgress;
                break;
            case 0306:
                itemsCounterQuests.bracersCreated -= questElement.UpdateProgress;
                break;
            case 0307:
                itemsCounterQuests.bracersTransmutated -= questElement.UpdateProgress;
                break;
            case 0401:
                itemsCounterQuests.magicstoneoreCollected -= questElement.UpdateProgress;
                break;
            case 0402:
                itemsCounterQuests.magicstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0403:
                itemsCounterQuests.magicstoneoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0404:
                itemsCounterQuests.magicstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0405:
                itemsCounterQuests.magicstonedustTransmutated -= questElement.UpdateProgress;
                break;
            case 0406:
                itemsCounterQuests.glovesCreated -= questElement.UpdateProgress;
                break;
            case 0407:
                itemsCounterQuests.glovesTransmutated -= questElement.UpdateProgress;
                break;
            case 0501:
                itemsCounterQuests.windstoneoreCollected -= questElement.UpdateProgress;
                break;
            case 0502:
                itemsCounterQuests.windstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0503:
                itemsCounterQuests.windstoneoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0504:
                itemsCounterQuests.windstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0505:
                itemsCounterQuests.windstonedustTransmutated -= questElement.UpdateProgress;
                break;
            case 0601:
                itemsCounterQuests.waterstoneoreCollected -= questElement.UpdateProgress;
                break;
            case 0602:
                itemsCounterQuests.waterstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0603:
                itemsCounterQuests.waterstoneoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0604:
                itemsCounterQuests.waterstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0605:
                itemsCounterQuests.waterstonedustTransmutated -= questElement.UpdateProgress;
                break;
            case 0701:
                itemsCounterQuests.earthstoneoreCollected -= questElement.UpdateProgress;
                break;
            case 0702:
                itemsCounterQuests.earthstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0703:
                itemsCounterQuests.earthstoneoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0704:
                itemsCounterQuests.earthstonedustCollected -= questElement.UpdateProgress;
                break;
            case 0705:
                itemsCounterQuests.earthstoneoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0801:
                itemsCounterQuests.lavastoneoreCollected -= questElement.UpdateProgress;
                break;
            case 0802:
                itemsCounterQuests.lavastonedustCollected -= questElement.UpdateProgress;
                break;
            case 0803:
                itemsCounterQuests.lavastoneoreTransmutated -= questElement.UpdateProgress;
                break;
            case 0804:
                itemsCounterQuests.lavastonedustCollected -= questElement.UpdateProgress;
                break;
            case 0805:
                itemsCounterQuests.lavastonedustTransmutated -= questElement.UpdateProgress;
                break;
            case 1001:
                contactedSkeletonsCounter.ContactedSkeleton -= questElement.UpdateProgress;
                break;
            case 1002:
                contactedSkeletonsCounter.ContactedSmallSkeleton -= questElement.UpdateProgress;
                break;
            case 1006:
                destroyedSkeletonsCounter.DestroyedSkeleton -= questElement.UpdateProgress;
                break;
            case 1007:
                destroyedSkeletonsCounter.DestroyedSmallSkeleton -= questElement.UpdateProgress;
                break;
            case 1101:
                cityWallUpgrade.HealthRegeneratedQuests -= questElement.UpdateProgress;
                break;
            case 1103:
                cityBlacksmithUpgrade.BlacksmithUpgradedQuests -= questElement.UpdateProgress;
                break;
            case 1104:
                cityCastleUpgrade.CastleUpgradedQuests -= questElement.UpdateProgress;
                break;
            case 1105:
                cityCastleUpgrade.ShardsUpgradedQuests -= questElement.UpdateProgress;
                break;
        }
    }

    public void SayHello(int there)
    {
        Debug.Log("well hello ther");
    }

    public void HelloThere()
    {
        /*
        InstantiatePotentialQuest(0101, "Collect stone ore", 10, 10);+
        InstantiatePotentialQuest(0102, "Defract stone ore", 20, 15);+
        InstantiatePotentialQuest(0103, "Transmutate to gold stone ore", 10, 10);+
        InstantiatePotentialQuest(0104, "Collect processed stone", 50, 20);+
        InstantiatePotentialQuest(0105, "Transmutate to gold processed stone", 10, 10);+
        InstantiatePotentialQuest(0106, "Create stone gloves", 1, 25);+
        InstantiatePotentialQuest(0107, "Transmutate to gold gloves", 1, 10);+

        InstantiatePotentialQuest(0201, "Collect metal ore", 10, 25);+
        InstantiatePotentialQuest(0202, "Defract metal ore", 20, 40);+
        InstantiatePotentialQuest(0203, "Transmutate to gold metal ore", 10, 20);+
        InstantiatePotentialQuest(0204, "Collect processed metal", 50, 40);+
        InstantiatePotentialQuest(0205, "Transmutate to gold processed metal", 10, 30);+
        InstantiatePotentialQuest(0206, "Create metal leggings", 1, 30);+
        InstantiatePotentialQuest(0207, "Create metal breast plate", 1, 50);+
        InstantiatePotentialQuest(0208, "Create metal shoes", 1, 20);+
        InstantiatePotentialQuest(0209, "Create metal helm", 1, 40);+
        InstantiatePotentialQuest(0210, "Transmutate to gold leggings", 1, 30);+
        InstantiatePotentialQuest(0211, "Transmutate to gold breast plate", 1, 50);+
        InstantiatePotentialQuest(0212, "Transmutate to gold shoes", 1, 20);+
        InstantiatePotentialQuest(0213, "Transmutate to gold helm", 1, 40);+

        InstantiatePotentialQuest(0301, "Collect cursed ore", 5, 30);+
        InstantiatePotentialQuest(0302, "Defract cursed ore", 10, 60);+
        InstantiatePotentialQuest(0303, "Transmutate to gold cursed ore", 10, 40);+
        InstantiatePotentialQuest(0304, "Collect cursed gem", 20, 10);+
        InstantiatePotentialQuest(0305, "Transmutate to gold cursed gem", 10, 80);+
        InstantiatePotentialQuest(0306, "Create cursed wrings", 1, 100);+
        InstantiatePotentialQuest(0307, "Transmutate to gold wrings", 1, 100);+

        InstantiatePotentialQuest(0401, "Collect magicstone ore", 1, 100);+
        InstantiatePotentialQuest(0402, "Defract magicstone ore", 1, 100);+
        InstantiatePotentialQuest(0403, "Transmutate to gold magicstone ore", 1, 100);+
        InstantiatePotentialQuest(0404, "Collect magic crystall", 1, 100);+
        InstantiatePotentialQuest(0405, "Transmutate to gold magic crystall", 1, 150);+
        InstantiatePotentialQuest(0406, "Create magic gloves", 1, 200);+++++
        InstantiatePotentialQuest(0407, "Transmutate to gold magic gloves", 1, 1000);+

        InstantiatePotentialQuest(0501, "Collect windstone ore", 1, 100);+
        InstantiatePotentialQuest(0502, "Defract windstone ore", 1, 100);+
        InstantiatePotentialQuest(0503, "Transmutate to gold windstone ore", 1, 100);+
        InstantiatePotentialQuest(0504, "Collect wind crystall", 1, 100);+
        InstantiatePotentialQuest(0505, "Transmutate to gold wind crystall", 1, 1000);+

        InstantiatePotentialQuest(0601, "Collect waterstone ore", 1, 100);+
        InstantiatePotentialQuest(0602, "Defract waterstone ore", 1, 100);+
        InstantiatePotentialQuest(0603, "Transmutate to gold waterstone ore", 1, 100);+
        InstantiatePotentialQuest(0604, "Collect water crystall", 1, 100);+
        InstantiatePotentialQuest(0605, "Transmutate to gold water crystall", 1, 1000);+

        InstantiatePotentialQuest(0701, "Collect earthstone ore", 1, 100);+
        InstantiatePotentialQuest(0702, "Defract earthstone ore", 1, 100);+
        InstantiatePotentialQuest(0703, "Transmutate to gold earthstone ore", 1, 100);+
        InstantiatePotentialQuest(0704, "Collect earth crystall", 1, 100);+
        InstantiatePotentialQuest(0705, "Transmutate to gold earth crystall", 1, 1000);+

        InstantiatePotentialQuest(0801, "Collect blackstone ore", 1, 100);+
        InstantiatePotentialQuest(0802, "Defract blackstone ore", 1, 100);+
        InstantiatePotentialQuest(0803, "Transmutate to gold blackstone ore", 1, 100);+
        InstantiatePotentialQuest(0804, "Collect black crystall", 1, 100);+
        InstantiatePotentialQuest(0805, "Transmutate to gold black crystall", 1, 1000);+

        InstantiatePotentialQuest(0001, "Walk", 10, 10);+
        InstantiatePotentialQuest(0002, "Crouch with skeleton arch mage", 10, 20);
        InstantiatePotentialQuest(0003, "Run", 10, 10);+
        InstantiatePotentialQuest(0004, "Jump", 2, 10);+
        InstantiatePotentialQuest(0005, "Try double jump", 1, 10);
        InstantiatePotentialQuest(0006, "Jump on the bed", 5, 10);
        InstantiatePotentialQuest(0007, "Use shift spell", 1, 10);
        InstantiatePotentialQuest(0008, "Hit wall", 1, 10);
        InstantiatePotentialQuest(0009, "Hit bookshelf", 1, 10);
        InstantiatePotentialQuest(0010, "Hit crates", 1, 10);
        InstantiatePotentialQuest(0011, "Hit potions table", 1, 10);
        InstantiatePotentialQuest(0012, "Hit wooden door", 1, 10);
        InstantiatePotentialQuest(0013, "Jump on the table", 1, 10);
        InstantiatePotentialQuest(0014, "Jump on the chair", 1, 10);
        InstantiatePotentialQuest(0015, "Regenerate objects", 1, 50);
        InstantiatePotentialQuest(0016, "Hit your head", 1, 10);
        InstantiatePotentialQuest(0017, "Collect objects", 50, 15);
        InstantiatePotentialQuest(0029, "Cast a tornado spell", 1, 10); InstantiatePotentialQuest(1010, "Attach a skeleton to ore", 1, 10);
        InstantiatePotentialQuest(0018, "Defract objects", 50, 25); InstantiatePotentialQuest(0019, "Transmutate to gold ore", 50, 25); InstantiatePotentialQuest(0020, "Transmutate to gold processed objects", 25, 25); InstantiatePotentialQuest(0021, "Transmutate to gold wearable items", 5, 30);
        InstantiatePotentialQuest(0022, "Create something", 1, 25);+
        
        
        
        InstantiatePotentialQuest(0023, "Open portal", 1, 10);
        InstantiatePotentialQuest(0024, "Try to wake an old mage", 1, 10);
        InstantiatePotentialQuest(0025, "Scare pegions", 1, 10);
        InstantiatePotentialQuest(0026, "Cast spells", 100, 10);
        InstantiatePotentialQuest(0027, "Play around with game settings", 1, 10);
        InstantiatePotentialQuest(0028, "Play game minutes", 1, 10);+
        InstantiatePotentialQuest(0029, "Cast a tornado spell", 1, 10);
        InstantiatePotentialQuest(0030, "Refresh your senses with tasty gulash", 1, 10);
        InstantiatePotentialQuest(0031, "Enter dungeons", 5, 10);+
        InstantiatePotentialQuest(0032, "Work with ores simultaneously", 2, 10);

        InstantiatePotentialQuest(1001, "Conjure a skeleton", 1, 10);
        InstantiatePotentialQuest(1002, "Conjure small skeleton", 1, 10);
        InstantiatePotentialQuest(1003, "Conjure big skeleton", 1, 25);
        InstantiatePotentialQuest(1004, "Conjure lizard skeleton", 1, 50);
        InstantiatePotentialQuest(1005, "Hit a skeleton with something", 1, 10);
        InstantiatePotentialQuest(1006, "Destroy a skeleton", 1, 10);
        InstantiatePotentialQuest(1007, "Destroy small skeleton", 1, 10);
        InstantiatePotentialQuest(1008, "Destroy big skeleton", 1, 25);
        InstantiatePotentialQuest(1009, "Destroy lizard skeleton", 1, 50);
        InstantiatePotentialQuest(1010, "Attach a skeleton to ore", 1, 10);
        InstantiatePotentialQuest(1011, "Equip skeletons with 1 object", 1, 20);
        InstantiatePotentialQuest(1012, "Equip skeletons with 2 object", 1, 40);
        InstantiatePotentialQuest(1013, "Equip skeletons with 3 object", 1, 50);
        InstantiatePotentialQuest(1014, "Equip skeletons with 4 object", 1, 60);
        InstantiatePotentialQuest(1015, "Equip skeletons with 5 object", 1, 100);

        InstantiatePotentialQuest(1101, "Regenerate health to castle", 1, 50);
        InstantiatePotentialQuest(1103, "Upgrade castle forge", 1, 50);
        InstantiatePotentialQuest(1104, "Upgrade castle temple", 1, 100);
        (1105, "Upgrade castle temple shards", 1, 100);
        */
    }

}
