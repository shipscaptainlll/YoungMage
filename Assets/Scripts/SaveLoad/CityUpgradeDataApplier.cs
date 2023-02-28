using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CityUpgradeDataApplier
{
    static CityUpgradeStateMachine cityUpgradeStateMachineLoaded;
    static CityUpgradeData cityUpgradeDataLoaded;

    public static void ApplyCityUpgradeData(CityUpgradeStateMachine cityUpgradeDataStateMachine, CityUpgradeData cityUpgradeDataData)
    {
        UpdateData(cityUpgradeDataStateMachine, cityUpgradeDataData);
        ApplyCastleState(cityUpgradeDataStateMachine, cityUpgradeDataData);
        DisconnectData();
    }

    static void UpdateData(CityUpgradeStateMachine cityUpgradeDataStateMachine, CityUpgradeData cityUpgradeDataData)
    {
        cityUpgradeStateMachineLoaded = cityUpgradeDataStateMachine;
        cityUpgradeDataLoaded = cityUpgradeDataData;
    }

    static void DisconnectData()
    {
        cityUpgradeStateMachineLoaded = null;
        cityUpgradeDataLoaded = null;
    }

    static void ApplyCastleState(CityUpgradeStateMachine cityUpgradeDataStateMachine, CityUpgradeData cityUpgradeData)
    {
        Debug.Log("Applying city upgrade state");
        if (cityUpgradeData.currentCastleHealth != 0) { Debug.Log("castle health was on " + cityUpgradeData.currentCastleHealth); cityUpgradeDataStateMachine.ApplyCastleHealth(cityUpgradeData); } else { Debug.Log("castle health was not on " + cityUpgradeData.currentCastleHealth); }
        if (cityUpgradeData.currentBlacksmithLevel != 0) { Debug.Log("blacksmith level was on " + cityUpgradeData.currentBlacksmithLevel); cityUpgradeDataStateMachine.ApplyBlacksmithParameters(cityUpgradeData); } else { Debug.Log("blacksmith level was not on " + cityUpgradeData.currentBlacksmithLevel); }
        if (cityUpgradeData.currentSphereLevel != 0 || cityUpgradeData.currentCountLevel != 0) { Debug.Log("castle sphere level was on " + cityUpgradeData.currentSphereLevel + " " + cityUpgradeData.currentCountLevel); cityUpgradeDataStateMachine.ApplyCastleData(cityUpgradeData); } else { Debug.Log("castle level was not on " + cityUpgradeData.currentSphereLevel + " " + cityUpgradeData.currentCountLevel); }
    }
}
