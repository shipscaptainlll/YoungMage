using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsDatabase : MonoBehaviour
{
    [SerializeField] NewQuestsNotificator newQuestsNotificator;
    [SerializeField] CompletedQuestsNotificator completedQuestsNotificator;
    [SerializeField] Quest questTemplate;
    List<Quest> quests = new List<Quest>();
    List<Quest> finishedQuests = new List<Quest>();
    List<Quest> potentialQuests = new List<Quest>();
    List<int> nextVisualisedQuests = new List<int>();
    List<int> nextVisualisedCompletedQuests = new List<int>();
    public List<Quest> activeQuestsIndexesSell = new List<Quest>();
    public HashSet<int> activeQuestsIndexes = new HashSet<int>();

    Coroutine completedQuestRecorded;
    bool coroutineFinished = true;

    int shownQuests;
    int maxShownQuests = 5;
    int shownCompletedQuests;
    int maxShownCompletedQuests = 10;

    public int ShownQuests { get { return shownQuests; } }
    public int MaxShownQuests { get { return maxShownQuests; } }
    public List<Quest> Quests
    {
        get { return quests; }
    }


    public event Action newQuestAdded = delegate { };
    public event Action completedQuestAdded = delegate { };
    // Start is called before the first frame update
    void Awake()
    {
        //InstantiatePotentialQuests();
        //StartCoroutine(InstantiateStartQuests());
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log(quests[0] == activeQuestsIndexesSell[0]);
            quests.Remove(quests[0]);
            activeQuestsIndexesSell.Remove(activeQuestsIndexesSell[0]);
            Debug.Log(quests[0] == activeQuestsIndexesSell[0]);
            StartCoroutine(InstantiateCompletedQuests());
        }
    }

    IEnumerator AddNewQuests(int id)
    {
        //Debug.Log(id);
        Quest foundQuest = potentialQuests.Find(x => x.Id == id);

        //Debug.Log(ShownQuests + " " + maxShownQuests);
        if (foundQuest.Level == 1 && shownQuests < maxShownQuests)
        {
            //foundQuest = potentialQuests.Find(x => x.Id == id);
            shownQuests++;
            //Debug.Log("previously added " + foundQuest.Id + " " + foundQuest.Description);
            quests.Add(foundQuest);
            foreach (Quest quest in quests)
            {
                //Debug.Log("Available after adding quest " + quest.Id + " " + quest.Description);
            }
            //activeQuestsIndexesSell.Add(foundQuest);
            int helloThereMyBrohter = quests.FindIndex(x => x == foundQuest);
            //Debug.Log(" hello there brother " + helloThereMyBrohter);
            int newOrderId = quests.Count - 1;
            //Debug.Log(" woow its there " + newOrderId);
            quests[newOrderId].OrderInList = newOrderId;
            string questDescription = foundQuest.Description;
            newQuestsNotificator.InstantiateQuestElement(questDescription);
            if (newQuestAdded != null) { newQuestAdded(); }
            yield return null;
        } else if (foundQuest.Level == 1 && shownQuests >= maxShownQuests)
        {
            nextVisualisedQuests.Add(id);
        }
        else if (foundQuest.Level != 1)
        {
            quests.Add(foundQuest);
            int helloThereMyBrohter = quests.FindIndex(x => x == foundQuest);
            int newOrderId = quests.Count - 1;
            quests[newOrderId].OrderInList = newOrderId;
            string questDescription = foundQuest.Description;
            if (foundQuest.Level == 1)
            {
                newQuestsNotificator.InstantiateQuestElement(questDescription);
            }
            if (newQuestAdded != null) { newQuestAdded(); }
            yield return null;
        }
    }

    public void ShowResiduaryQuests()
    {
        shownQuests--;
        //Debug.Log(shownQuests);
        if (nextVisualisedQuests.Count != 0)
        {
            StartCoroutine(AddNewQuests(nextVisualisedQuests[0]));
            nextVisualisedQuests.RemoveAt(0);
            //Debug.Log("worked at " + transform);
        }
    }

    public void RecordCompleted(int id)
    {
        //Debug.Log(completedQuestRecorded);
        if (coroutineFinished)
        {
            
            //Debug.Log("trying to add new completed");
            completedQuestRecorded = StartCoroutine(RecordCompletedQuests(id));
        } else
        {
            //Debug.Log("instantiating wait sequence");
            StartCoroutine(WaitCompletedLine(id));
        }
    }

    public void TryReuploadQuest(int id)
    {
        //Debug.Log(5);
        Quest quest = quests.Find(x => x.Id == id);
        //Debug.Log(6);
        activeQuestsIndexesSell.Remove(quest);

        //Debug.Log(7);
        if (quest.Level < quest.MaxLevel)
        {
            
            quests.Remove(quest);
            //Debug.Log(8);
            StartCoroutine(AddNewQuests(id));
        }
        else
        {
            quests.Remove(quest);
            if (newQuestAdded != null) { newQuestAdded(); }
        }
        
    }

    IEnumerator RecordCompletedQuests(int id)
    {
        //Debug.Log(shownCompletedQuests + " shown plus maxShown " + maxShownCompletedQuests);
        if (shownCompletedQuests < maxShownCompletedQuests)
        {
            coroutineFinished = false;
            Quest foundQuest = quests.Find(x => x.Id == id);
            if (foundQuest == null)
            {
                Debug.Log("CAUTION quest was not found ");
                coroutineFinished = true;
                yield break;
            }
            //Debug.Log("adding of completed has started");
            shownCompletedQuests++;
            foreach (Quest quest in quests)
            {
                //Debug.Log("Available quest " + quest.Id + " " + quest.Description);
            }
            //Debug.Log(id);
            
            
            //Debug.Log(quests.Find(x => x.Id == id) + " quest ");
            //Debug.Log(potentialQuests.Find(x => x.Id == id) + " potential quest ");
            finishedQuests.Add(foundQuest);
            //Debug.Log(finishedQuests.Count);
            //Debug.Log(finishedQuests[0]);
            //int newOrderId = finishedQuests.Count - 1;
            //finishedQuests[newOrderId].OrderInList = newOrderId;
            string questDescription = foundQuest.Description;
            completedQuestsNotificator.InstantiateQuestElement(questDescription);
            if (completedQuestAdded != null) { completedQuestAdded(); }
            completedQuestRecorded = null;
            if (completedQuestRecorded != null)
            {
                StopCoroutine(completedQuestRecorded);
            }
            completedQuestRecorded = null;
            coroutineFinished = true;
            //Debug.Log(coroutineFinished + " right here we finishing coroutine");
            yield return null;
        }
        else
        {
            nextVisualisedCompletedQuests.Add(id);
            if (completedQuestRecorded != null)
            {
                StopCoroutine(completedQuestRecorded);
            }
            
        }
    }

    IEnumerator WaitCompletedLine(int id)
    {
        Debug.Log("Waiting in line there " + transform);
        yield return new WaitForSeconds(0.25f);
        RecordCompleted(id);
    }

    public void ShowResiduaryCompletedQuests()
    {
        shownCompletedQuests--;
        //Debug.Log(shownCompletedQuests);
        if (nextVisualisedCompletedQuests.Count != 0)
        {
            StartCoroutine(RecordCompletedQuests(nextVisualisedCompletedQuests[0]));
            nextVisualisedCompletedQuests.RemoveAt(0);
            //Debug.Log("worked at " + transform);
        }
    }

    void InstantiatePotentialQuests()
    {
        InstantiatePotentialQuest(0101, "Collect stone ore", 10, 10);
        InstantiatePotentialQuest(0102, "Defract stone ore", 20, 15);
        InstantiatePotentialQuest(0103, "Transmutate to gold stone ore", 10, 10);
        InstantiatePotentialQuest(0104, "Collect processed stone", 50, 20);
        InstantiatePotentialQuest(0105, "Transmutate to gold processed stone", 10, 10);
        InstantiatePotentialQuest(0106, "Create stone gloves", 1, 25);
        InstantiatePotentialQuest(0107, "Transmutate to gold gloves", 1, 10);

        InstantiatePotentialQuest(0201, "Collect metal ore", 10, 25);
        InstantiatePotentialQuest(0202, "Defract metal ore", 20, 40);
        InstantiatePotentialQuest(0203, "Transmutate to gold metal ore", 10, 20);
        InstantiatePotentialQuest(0204, "Collect processed metal", 50, 40);
        InstantiatePotentialQuest(0205, "Transmutate to gold processed metal", 10, 30);
        InstantiatePotentialQuest(0206, "Create metal leggings", 1, 30);
        InstantiatePotentialQuest(0207, "Create metal breast plate", 1, 50);
        InstantiatePotentialQuest(0208, "Create metal shoes", 1, 20);
        InstantiatePotentialQuest(0209, "Create metal helm", 1, 40);
        InstantiatePotentialQuest(0210, "Transmutate to gold leggings", 1, 30);
        InstantiatePotentialQuest(0211, "Transmutate to gold breast plate", 1, 50);
        InstantiatePotentialQuest(0212, "Transmutate to gold shoes", 1, 20);
        InstantiatePotentialQuest(0213, "Transmutate to gold helm", 1, 40);

        InstantiatePotentialQuest(0301, "Collect cursed ore", 5, 30);
        InstantiatePotentialQuest(0302, "Defract cursed ore", 10, 60);
        InstantiatePotentialQuest(0303, "Transmutate to gold cursed ore", 10, 40);
        InstantiatePotentialQuest(0304, "Collect cursed gem", 20, 10);
        InstantiatePotentialQuest(0305, "Transmutate to gold cursed gem", 10, 80);
        InstantiatePotentialQuest(0306, "Create cursed wrings", 1, 100);
        InstantiatePotentialQuest(0307, "Transmutate to gold wrings", 1, 100);

        InstantiatePotentialQuest(0401, "Collect magicstone ore", 1, 100);
        InstantiatePotentialQuest(0402, "Defract magicstone ore", 1, 100);
        InstantiatePotentialQuest(0403, "Transmutate to gold magicstone ore", 1, 100);
        InstantiatePotentialQuest(0404, "Collect magic crystall", 1, 100);
        InstantiatePotentialQuest(0405, "Transmutate to gold magic crystall", 1, 150);
        InstantiatePotentialQuest(0406, "Create magic gloves", 1, 200);
        InstantiatePotentialQuest(0407, "Transmutate to gold magic gloves", 1, 1000);

        InstantiatePotentialQuest(0501, "Collect windstone ore", 1, 100);
        InstantiatePotentialQuest(0502, "Defract windstone ore", 1, 100);
        InstantiatePotentialQuest(0503, "Transmutate to gold windstone ore", 1, 100);
        InstantiatePotentialQuest(0504, "Collect wind crystall", 1, 100);
        InstantiatePotentialQuest(0505, "Transmutate to gold wind crystall", 1, 1000);

        InstantiatePotentialQuest(0601, "Collect waterstone ore", 1, 100);
        InstantiatePotentialQuest(0602, "Defract waterstone ore", 1, 100);
        InstantiatePotentialQuest(0603, "Transmutate to gold waterstone ore", 1, 100);
        InstantiatePotentialQuest(0604, "Collect water crystall", 1, 100);
        InstantiatePotentialQuest(0605, "Transmutate to gold water crystall", 1, 1000);

        InstantiatePotentialQuest(0701, "Collect earthstone ore", 1, 100);
        InstantiatePotentialQuest(0702, "Defract earthstone ore", 1, 100);
        InstantiatePotentialQuest(0703, "Transmutate to gold earthstone ore", 1, 100);
        InstantiatePotentialQuest(0704, "Collect earth crystall", 1, 100);
        InstantiatePotentialQuest(0705, "Transmutate to gold earth crystall", 1, 1000);

        InstantiatePotentialQuest(0801, "Collect blackstone ore", 1, 100);
        InstantiatePotentialQuest(0802, "Defract blackstone ore", 1, 100);
        InstantiatePotentialQuest(0803, "Transmutate to gold blackstone ore", 1, 100);
        InstantiatePotentialQuest(0804, "Collect black crystall", 1, 100);
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
        InstantiatePotentialQuest(0017, "Collect objects", 50, 15);
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
        InstantiatePotentialQuest(1016, "Equip skeletons with 6 object", 1, 120);

        InstantiatePotentialQuest(1101, "Regenerate health to castle", 1, 50);
        InstantiatePotentialQuest(1102, "Upgrade castle wall", 1, 25);
        InstantiatePotentialQuest(1103, "Upgrade castle forge", 1, 50);
        InstantiatePotentialQuest(1104, "Upgrade castle temple", 1, 100);
        InstantiatePotentialQuest(1105, "Upgrade castle temple shards", 1, 100);
    }

    void InstantiatePotentialQuest(int id, string description, int goal, int reward)
    {
        Quest quest = Instantiate(questTemplate);
        quest.Id = id;
        quest.Description = description;
        quest.Goal = goal;
        //Debug.Log(quest.Goal + " goal actual + given value " + goal + " level " + quest.Level);
        quest.Reward = reward;
        potentialQuests.Add(quest);
    }



    IEnumerator InstantiateStartQuests()
    {
        yield return new WaitForSeconds(1f);
        //StartCoroutine(AddNewQuests(1001));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(0106));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(0206));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(0207));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(0208));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(0209));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(0306));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(0406));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(AddNewQuests(1007));
        yield return null;
        StartCoroutine(AddNewQuests(1001));
        yield return null;
        StartCoroutine(AddNewQuests(1003));
        yield return null;
        StartCoroutine(AddNewQuests(1002));
        yield return null;
        StartCoroutine(AddNewQuests(1004));
        yield return null;
        StartCoroutine(AddNewQuests(1005));
        yield return null;
        StartCoroutine(AddNewQuests(1006));
        yield return null;
    }
    IEnumerator InstantiateCompletedQuests()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(RecordCompletedQuests(0101));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(RecordCompletedQuests(0001));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(RecordCompletedQuests(1001));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(RecordCompletedQuests(1002));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(RecordCompletedQuests(1010));
        yield return new WaitForSeconds(0.24f);
        StartCoroutine(RecordCompletedQuests(1101));
        yield return null;
    }

}
