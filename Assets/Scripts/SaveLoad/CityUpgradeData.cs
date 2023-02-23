using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CityUpgradeData
{
    public float currentCastleHealth;
    public float currentBlacksmithLevel;
    public float currentSphereLevel;
    public float currentCountLevel;



    public CityUpgradeData(CityUpgradeStateMachine cityUpgradeStateMachine)
    {
        GetBlacksmithParameters(cityUpgradeStateMachine);
        GetCastleParameters(cityUpgradeStateMachine);
        GetWallsParameters(cityUpgradeStateMachine);
    }

    void GetBlacksmithParameters(CityUpgradeStateMachine cityUpgradeStateMachine)
    {
        currentBlacksmithLevel = cityUpgradeStateMachine.GetBlacksmithParameters();
    }

    void GetCastleParameters(CityUpgradeStateMachine cityUpgradeStateMachine)
    {
        currentSphereLevel = cityUpgradeStateMachine.GetSphereUpgradeLevel();
        currentCountLevel = cityUpgradeStateMachine.GetCountUpgradeLevel();
    }

    void GetWallsParameters(CityUpgradeStateMachine cityUpgradeStateMachine)
    {
        currentCastleHealth = cityUpgradeStateMachine.GetCastleHealth();
    }

}
