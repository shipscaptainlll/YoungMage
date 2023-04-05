using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationableItemsDatabase : MonoBehaviour
{

    public static bool isTransmutationable(int itemID)
    {
        switch (itemID)
        {
            case 18:
                return true;
            case 19:
                return true;
            case 20:
                return true;
            case 21:
                return true;
            case 22:
                return true;
            case 23:
                return true;
            case 24:
                return true;
            case 25:
                return true;
        }

        return false;
    }
    
}
