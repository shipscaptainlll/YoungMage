using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorObjectManager : MonoBehaviour
{
    [SerializeField] GameObject stoneHands;
    [SerializeField] GameObject leggings;
    [SerializeField] GameObject plateArmor;
    [SerializeField] GameObject shoes;
    [SerializeField] GameObject helm;
    [SerializeField] GameObject gloves;
    [SerializeField] GameObject bracers;


    void Start()
    {

    }

    public GameObject TakeObject(int customeID)
    {
        switch (customeID)
        {
            case 0:
                return null;
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
        }
        return null;
    }
}
