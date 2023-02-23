using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityUpgradeStateMachine : MonoBehaviour
{
    [SerializeField] CityBlacksmithUpgrade cityBlacksmithUpgrade;
    [SerializeField] CityCastleUpgrade cityCastleUpgrade;
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;


    public float GetCastleHealth()
    {
        return castleHealthDecreaser.CurrentHealth;
    }

    public void ApplyCastleHealth(CityUpgradeData cityUpgradeData)
    {
        castleHealthDecreaser.CurrentHealth = cityUpgradeData.currentCastleHealth;
    }

    public float GetBlacksmithParameters()
    {
        return cityBlacksmithUpgrade.UpgradeCurrentCount;
    }

    public void ApplyBlacksmithParameters(CityUpgradeData cityUpgradeData)
    {
        cityBlacksmithUpgrade.UploadBlacksmithLevel((int) cityUpgradeData.currentBlacksmithLevel); 
    }

    public float GetSphereUpgradeLevel()
    {
        return cityCastleUpgrade.SphereUpgradeCurrentCount;
    }

    public float GetCountUpgradeLevel()
    {
        return cityCastleUpgrade.CountUpgradeCurrentCount;
    }

    public void ApplyCastleData(CityUpgradeData cityUpgradeData)
    {
        cityCastleUpgrade.UploadCastleData((int)cityUpgradeData.currentSphereLevel, (int)cityUpgradeData.currentCountLevel);
    }

}
