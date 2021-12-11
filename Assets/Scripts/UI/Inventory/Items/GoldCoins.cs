using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoins : MonoBehaviour, IItem
{
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    string name;
    int ammount;


    public string Name
    {
        get
        {
            return name;
        }
    }

    public int Ammount
    {
        get
        {
            return ammount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        goldCoinsCounter.AmountChanged += UpdateAmount;
        name = "goldCoins";
        ammount = 0;
    }

    void UpdateAmount(int newAmount)
    {
        ammount = newAmount;
        UpdateUI();
    }

    void UpdateUI()
    {
        transform.parent.Find("AmountCounter").GetComponent<Text>().text = ammount.ToString();
    }

    void Update()
    {
        ammount += 1;
        UpdateUI();
    }
}
