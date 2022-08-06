using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    string description;
    int goal;
    int reward;
    int level = 1;
    int maxLevel;
    int id;
    int orderInList;

    public static int count = 1;



    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Goal
    {
        get { return goal; }
        set { goal = value *level + value + 1; }
    }

    public int Reward
    {
        get { return reward; }
        set { reward = (int) (value * 1.25f); }
    }

    public int Level
    {
        get { return level; }
        set { level = value;
        }
    }

    public int MaxLevel
    {
        get { return maxLevel; }
        set { maxLevel = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public int OrderInList
    {
        get { return orderInList; }
        set { orderInList = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        maxLevel = 5;
        level = 1;
    }

    /*
    public Quest(int id, string description, int goal, int reward)
    {
        this.Level = 1;
        this.Description = description;
        this.Goal = goal;
        this.Reward = reward;
        this.Id = id;

        count++;
    }
    */

    public static Quest Constructor(int id, string description, int goal, int reward)
    {
        //quest.Level = 1;
        //quest.Description = description;
        //quest.Goal = goal * quest.Level;
        //quest.Reward = reward;
        //quest.Id = id;

        //count++;
        //Debug.Log(quest);
        return null;
    }
}
