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
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0002:
                break;
            case 0003:
                personMovement.CharacterRunnedStep += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterSecond);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0004:
                personMovement.CharacterJumped += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterFourth);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0005:
                personMovement.CharacterDoubleJumped += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterFifth);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0007:
                personMovement.CharacterShifted += questElement.UpdateProgress;
                questElement.UpdateProgress(personMovement.ProgressParameterThird);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0027:
                graphicsPanel.SettingChanged += questElement.UpdateProgress;
                audioPanel.SettingChanged += questElement.UpdateProgress;
                controlsPanel.SettingChanged += questElement.UpdateProgress;
                miscPanel.SettingChanged += questElement.UpdateProgress;
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0028:
                gameTimeManager.MinutesIngamePassed += questElement.UpdateProgress;
                questElement.UpdateProgress(gameTimeManager.ProgressParameter);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0031:
                mineCharacterCatcher.CharacterEnteredDungeon += questElement.UpdateProgress;
                questElement.UpdateProgress(mineCharacterCatcher.ProgressParameter);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0101:
                itemsCounterQuests.stoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedStoneoreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0102:
                itemsCounterQuests.stonebricksCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedStonebricksCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0104:
                itemsCounterQuests.stonebricksCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedStonebricksCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0201:
                itemsCounterQuests.metaloreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMetaloreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0202:
                itemsCounterQuests.metalingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMetalingotsCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0204:
                itemsCounterQuests.metalingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMetalingotsCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0301:
                itemsCounterQuests.cursedoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedCursedoreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0302:
                itemsCounterQuests.cursedingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedCursedingotsCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0304:
                itemsCounterQuests.cursedingotsCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedCursedingotsCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0401:
                itemsCounterQuests.magicstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMagicstoneoreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0402:
                itemsCounterQuests.magicstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMagicstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0404:
                itemsCounterQuests.magicstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedMagicstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0501:
                itemsCounterQuests.windstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWindstoneoreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0502:
                itemsCounterQuests.windstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWindstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0504:
                itemsCounterQuests.windstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWindstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0601:
                itemsCounterQuests.waterstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWaterstoneoreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0602:
                itemsCounterQuests.waterstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWaterstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0604:
                itemsCounterQuests.waterstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedWaterstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0701:
                itemsCounterQuests.earthstoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedEarthstoneoreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0702:
                itemsCounterQuests.earthstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedEarthstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0704:
                itemsCounterQuests.earthstonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedEarthstonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0801:
                itemsCounterQuests.lavastoneoreCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedLavastoneoreCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0802:
                itemsCounterQuests.lavastonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedLavastonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0804:
                itemsCounterQuests.lavastonedustCollected += questElement.UpdateProgress;
                questElement.UpdateProgress(itemsCounterQuests.CollectedLavastonedustCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 1001:
                contactedSkeletonsCounter.ContactedSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(contactedSkeletonsCounter.ContactedSkeletonsCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 1002:
                contactedSkeletonsCounter.ContactedSmallSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(contactedSkeletonsCounter.CountSmallSkeletons);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 1006:
                destroyedSkeletonsCounter.DestroyedSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(destroyedSkeletonsCounter.DestroyedSkeletonsCount);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 1007:
                destroyedSkeletonsCounter.DestroyedSmallSkeleton += questElement.UpdateProgress;
                questElement.UpdateProgress(destroyedSkeletonsCounter.CountSmallSkeletons);
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
        }
    }

    public void UnsubscribeFromProgress(QuestElement questElement)
    {
        switch (questElement.Id)
        {
            case 0001:
                personMovement.CharacterStepMade -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0002:
                break;
            case 0003:
                personMovement.CharacterRunnedStep -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0004:
                personMovement.CharacterJumped -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0005:
                personMovement.CharacterDoubleJumped -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0007:
                personMovement.CharacterShifted -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0027:
                graphicsPanel.SettingChanged -= questElement.UpdateProgress;
                audioPanel.SettingChanged -= questElement.UpdateProgress;
                controlsPanel.SettingChanged -= questElement.UpdateProgress;
                miscPanel.SettingChanged -= questElement.UpdateProgress;
                Debug.Log("was subscribed on progress " + questElement.Id);
                break;
            case 0028:
                gameTimeManager.MinutesIngamePassed -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0031:
                mineCharacterCatcher.CharacterEnteredDungeon -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0101:
                itemsCounterQuests.stoneoreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0102:
                itemsCounterQuests.stonebricksCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0104:
                itemsCounterQuests.stonebricksCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0201:
                itemsCounterQuests.metaloreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0202:
                itemsCounterQuests.metalingotsCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0204:
                itemsCounterQuests.metalingotsCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0301:
                itemsCounterQuests.cursedoreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0302:
                itemsCounterQuests.cursedingotsCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0304:
                itemsCounterQuests.cursedingotsCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0401:
                itemsCounterQuests.magicstoneoreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0402:
                itemsCounterQuests.magicstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0404:
                itemsCounterQuests.magicstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0501:
                itemsCounterQuests.windstoneoreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0502:
                itemsCounterQuests.windstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0504:
                itemsCounterQuests.windstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0601:
                itemsCounterQuests.waterstoneoreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0602:
                itemsCounterQuests.waterstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0604:
                itemsCounterQuests.waterstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0701:
                itemsCounterQuests.earthstoneoreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0702:
                itemsCounterQuests.earthstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0704:
                itemsCounterQuests.earthstonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0801:
                itemsCounterQuests.lavastoneoreCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0802:
                itemsCounterQuests.lavastonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 0804:
                itemsCounterQuests.lavastonedustCollected -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 1001:
                contactedSkeletonsCounter.ContactedSkeleton -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 1002:
                contactedSkeletonsCounter.ContactedSmallSkeleton -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 1006:
                destroyedSkeletonsCounter.DestroyedSkeleton -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
            case 1007:
                destroyedSkeletonsCounter.DestroyedSmallSkeleton -= questElement.UpdateProgress;
                Debug.Log("was unsubscribed on progress " + questElement.Id);
                break;
        }
    }

    public void HelloThere()
    {
        /*
        InstantiatePotentialQuest(0101, "Collect stone ore", 10, 10);+
        InstantiatePotentialQuest(0102, "Defract stone ore", 20, 15);+
        InstantiatePotentialQuest(0103, "Transmutate to gold stone ore", 10, 10);
        InstantiatePotentialQuest(0104, "Collect processed stone", 50, 20);+
        InstantiatePotentialQuest(0105, "Transmutate to gold processed stone", 10, 10);
        InstantiatePotentialQuest(0106, "Create stone gloves", 1, 25);
        InstantiatePotentialQuest(0107, "Transmutate to gold gloves", 1, 10);

        InstantiatePotentialQuest(0201, "Collect metal ore", 10, 25);+
        InstantiatePotentialQuest(0202, "Defract metal ore", 20, 40);+
        InstantiatePotentialQuest(0203, "Transmutate to gold metal ore", 10, 20);
        InstantiatePotentialQuest(0204, "Collect processed metal", 50, 40);+
        InstantiatePotentialQuest(0205, "Transmutate to gold processed metal", 10, 30);
        InstantiatePotentialQuest(0206, "Create metal leggings", 1, 30);
        InstantiatePotentialQuest(0207, "Create metal breast plate", 1, 50);
        InstantiatePotentialQuest(0208, "Create metal shoes", 1, 20);
        InstantiatePotentialQuest(0209, "Create metal helm", 1, 40);
        InstantiatePotentialQuest(0210, "Transmutate to gold leggings", 1, 30);
        InstantiatePotentialQuest(0211, "Transmutate to gold breast plate", 1, 50);
        InstantiatePotentialQuest(0212, "Transmutate to gold shoes", 1, 20);
        InstantiatePotentialQuest(0213, "Transmutate to gold helm", 1, 40);

        InstantiatePotentialQuest(0301, "Collect cursed ore", 5, 30);+
        InstantiatePotentialQuest(0302, "Defract cursed ore", 10, 60);+
        InstantiatePotentialQuest(0303, "Transmutate to gold cursed ore", 10, 40);
        InstantiatePotentialQuest(0304, "Collect cursed gem", 20, 10);+
        InstantiatePotentialQuest(0305, "Transmutate to gold cursed gem", 10, 80);
        InstantiatePotentialQuest(0306, "Create cursed wrings", 1, 100);
        InstantiatePotentialQuest(0307, "Transmutate to gold wrings", 1, 100);

        InstantiatePotentialQuest(0401, "Collect magicstone ore", 1, 100);+
        InstantiatePotentialQuest(0402, "Defract magicstone ore", 1, 100);+
        InstantiatePotentialQuest(0403, "Transmutate to gold magicstone ore", 1, 100);
        InstantiatePotentialQuest(0404, "Collect magic crystall", 1, 100);+
        InstantiatePotentialQuest(0405, "Transmutate to gold magic crystall", 1, 150);
        InstantiatePotentialQuest(0406, "Create magic gloves", 1, 200);
        InstantiatePotentialQuest(0407, "Transmutate to gold magic gloves", 1, 1000);

        InstantiatePotentialQuest(0501, "Collect windstone ore", 1, 100);+
        InstantiatePotentialQuest(0502, "Defract windstone ore", 1, 100);+
        InstantiatePotentialQuest(0503, "Transmutate to gold windstone ore", 1, 100);
        InstantiatePotentialQuest(0504, "Collect wind crystall", 1, 100);+
        InstantiatePotentialQuest(0505, "Transmutate to gold wind crystall", 1, 1000);

        InstantiatePotentialQuest(0601, "Collect waterstone ore", 1, 100);+
        InstantiatePotentialQuest(0602, "Defract waterstone ore", 1, 100);+
        InstantiatePotentialQuest(0603, "Transmutate to gold waterstone ore", 1, 100);
        InstantiatePotentialQuest(0604, "Collect water crystall", 1, 100);+
        InstantiatePotentialQuest(0605, "Transmutate to gold water crystall", 1, 1000);

        InstantiatePotentialQuest(0701, "Collect earthstone ore", 1, 100);+
        InstantiatePotentialQuest(0702, "Defract earthstone ore", 1, 100);
        InstantiatePotentialQuest(0703, "Transmutate to gold earthstone ore", 1, 100);
        InstantiatePotentialQuest(0704, "Collect earth crystall", 1, 100);+
        InstantiatePotentialQuest(0705, "Transmutate to gold earth crystall", 1, 1000);

        InstantiatePotentialQuest(0801, "Collect blackstone ore", 1, 100);+
        InstantiatePotentialQuest(0802, "Defract blackstone ore", 1, 100);
        InstantiatePotentialQuest(0803, "Transmutate to gold blackstone ore", 1, 100);
        InstantiatePotentialQuest(0804, "Collect black crystall", 1, 100);+
        InstantiatePotentialQuest(0805, "Transmutate to gold black crystall", 1, 1000);

        InstantiatePotentialQuest(0001, "Walk", 10, 10);
        InstantiatePotentialQuest(0002, "Crouch with skeleton arch mage", 10, 20);
        InstantiatePotentialQuest(0003, "Run", 10, 10);
        InstantiatePotentialQuest(0004, "Jump", 2, 10);
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
        InstantiatePotentialQuest(0017, "Collect objects", 50, 15); InstantiatePotentialQuest(0029, "Cast a tornado spell", 1, 10); InstantiatePotentialQuest(1010, "Attach a skeleton to ore", 1, 10);
        InstantiatePotentialQuest(0018, "Defract objects", 50, 25);
        InstantiatePotentialQuest(0019, "Transmutate to gold ore", 50, 25);
        InstantiatePotentialQuest(0020, "Transmutate to gold processed objects", 25, 25);
        InstantiatePotentialQuest(0021, "Transmutate to gold wearable items", 5, 30);
        InstantiatePotentialQuest(0022, "Create something", 1, 25);
        InstantiatePotentialQuest(0023, "Open portal", 1, 10);
        InstantiatePotentialQuest(0024, "Try to wake an old mage", 1, 10);
        InstantiatePotentialQuest(0025, "Scare pegions", 1, 10);
        InstantiatePotentialQuest(0026, "Cast spells", 100, 10);
        InstantiatePotentialQuest(0027, "Play around with game settings", 1, 10);
        InstantiatePotentialQuest(0028, "Play game minutes", 1, 10);
        InstantiatePotentialQuest(0029, "Cast a tornado spell", 1, 10);
        InstantiatePotentialQuest(0030, "Refresh your senses with tasty gulash", 1, 10);
        InstantiatePotentialQuest(0031, "Enter dungeons", 5, 10);
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
        InstantiatePotentialQuest(1102, "Upgrade castle wall", 1, 25);
        InstantiatePotentialQuest(1103, "Upgrade castle forge", 1, 50);
        InstantiatePotentialQuest(1104, "Upgrade castle temple", 1, 100);
        */
    }

}
