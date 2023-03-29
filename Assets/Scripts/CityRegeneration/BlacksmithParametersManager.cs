using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithParametersManager : MonoBehaviour
{
    [SerializeField] private int[] m_defenceLevels;
    [SerializeField] private int[] m_costLevels;

    private static readonly Dictionary<int, int> _wallDefenceParameters = new Dictionary<int, int>();
    private static readonly Dictionary<int, int> _wallUpgradeCost = new Dictionary<int, int>();
    private void Awake()
    {
        FillDefencesDictionary();
        FillUpgradesDictionary();
    }

    public static int GetWallDefence(int blacksmithShieldLevel)
    {
        return _wallDefenceParameters[blacksmithShieldLevel];
    }
    
    public static int GetUpgradeCost(int blacksmithShieldLevel)
    {
        return _wallUpgradeCost[blacksmithShieldLevel];
    }

    void FillDefencesDictionary()
    {
        _wallDefenceParameters.Add(1, m_defenceLevels[0]);
        _wallDefenceParameters.Add(2, m_defenceLevels[1]);
        _wallDefenceParameters.Add(3, m_defenceLevels[2]);
        _wallDefenceParameters.Add(4, m_defenceLevels[3]);
        _wallDefenceParameters.Add(5, m_defenceLevels[4]);
        _wallDefenceParameters.Add(6, m_defenceLevels[5]);
    }

    void FillUpgradesDictionary()
    {
        _wallUpgradeCost.Add(1, m_costLevels[0]);
        _wallUpgradeCost.Add(2, m_costLevels[1]);
        _wallUpgradeCost.Add(3, m_costLevels[2]);
        _wallUpgradeCost.Add(4, m_costLevels[3]);
        _wallUpgradeCost.Add(5, m_costLevels[4]);
        //WallUpgradeCost.Add(6, costLevels[5]);
    }
}
