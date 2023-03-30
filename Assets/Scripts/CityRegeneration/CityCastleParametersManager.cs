using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCastleParametersManager : MonoBehaviour
{
    [SerializeField] private int[] m_regenerationLevels;
    [SerializeField] private int[] m_upgradeCostLevels;
    [SerializeField] private int[] m_buyCostLevels;

    
    

    private static readonly Dictionary<int, int> _regenerationParameters = new Dictionary<int, int>();
    private static readonly Dictionary<int, int> _magicUpgradeCost = new Dictionary<int, int>();
    private static readonly Dictionary<int, int> _sphereCostParameters = new Dictionary<int, int>();
    private void Awake()
    {
        FillRegenerationDictionary();
        FillMagicUpgradesDictionary();
    }

    public static int GetRegenerationAmmount(int magicPotencyLevel)
    {
        return _regenerationParameters[magicPotencyLevel];
    }
    
    public static int GetMagicUpgradeCost(int magicPotenceLevel)
    {
        return _magicUpgradeCost[magicPotenceLevel];
    }
    
    public static int GetSphereCost(int numberOfCurrentSpheres)
    {
        return _magicUpgradeCost[numberOfCurrentSpheres];
    }

    void FillRegenerationDictionary()
    {
        _regenerationParameters.Add(0, m_regenerationLevels[0]);
        _regenerationParameters.Add(1, m_regenerationLevels[1]);
        _regenerationParameters.Add(2, m_regenerationLevels[2]);
        _regenerationParameters.Add(3, m_regenerationLevels[3]);
        _regenerationParameters.Add(4, m_regenerationLevels[4]);
        _regenerationParameters.Add(5, m_regenerationLevels[5]);
    }

    void FillMagicUpgradesDictionary()
    {
        _magicUpgradeCost.Add(0, m_upgradeCostLevels[0]);
        _magicUpgradeCost.Add(1, m_upgradeCostLevels[1]);
        _magicUpgradeCost.Add(2, m_upgradeCostLevels[2]);
        _magicUpgradeCost.Add(3, m_upgradeCostLevels[3]);
        _magicUpgradeCost.Add(4, m_upgradeCostLevels[4]);
        _magicUpgradeCost.Add(5, m_upgradeCostLevels[5]);
        //WallUpgradeCost.Add(6, costLevels[5]);
    }
    
    void FillSphereCostDictionary()
    {
        _sphereCostParameters.Add(0, m_buyCostLevels[0]);
        _sphereCostParameters.Add(1, m_buyCostLevels[1]);
        _sphereCostParameters.Add(2, m_buyCostLevels[2]);
        _sphereCostParameters.Add(3, m_buyCostLevels[3]);
        _sphereCostParameters.Add(4, m_buyCostLevels[4]);
        _sphereCostParameters.Add(5, m_buyCostLevels[5]);
        //WallUpgradeCost.Add(6, costLevels[5]);
    }
}
