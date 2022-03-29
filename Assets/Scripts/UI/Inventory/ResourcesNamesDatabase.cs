using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesNamesDatabase : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetResourceName(int ID)
    {
        switch (ID)
        {
            case 0:
                return "nothing";
            case 1:
                return "Gold Coin";
            case 2:
                return "Stone Ore";
            case 3:
                return "Metal Ore";
            case 4:
                return "Cursed Ore";
            case 5:
                return "Earthstone Ore";
            case 6:
                return "Lavastone Ore";
            case 7:
                return "Magicstone Ore";
            case 8:
                return "Waterstone Ore";
            case 9:
                return "Windstone Ore";
            case 10:
                return "Magic Wand";
            case 11:
                return "Stone Hand";
            case 12:
                return "Leggings";
            case 13:
                return "Plate Armor";
            case 14:
                return "Shoes";
            case 15:
                return "Helm";
            case 16:
                return "Gloves";
            case 17:
                return "Bracers";
            case 18:
                return "Stone Brick";
            case 19:
                return "Metal Ingot";
            case 20:
                return "Cursed Ingot";
            case 21:
                return "Earthstone Dust";
            case 22:
                return "Lavastone Dust";
            case 23:
                return "Magicstone Dust";
            case 24:
                return "Waterstone Dust";
            case 25:
                return "Windstone Dust";

        }
        return "Nothing";

    }
}
