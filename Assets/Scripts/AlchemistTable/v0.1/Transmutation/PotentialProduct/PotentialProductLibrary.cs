using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialProductLibrary : MonoBehaviour
{
    [SerializeField] GameObject stoneHands;
    [SerializeField] GameObject leggings;
    [SerializeField] GameObject plateArmor;
    [SerializeField] GameObject shoes;
    [SerializeField] GameObject helm;
    [SerializeField] GameObject glove;
    [SerializeField] GameObject bracers;
    [SerializeField] GameObject skeletonScanner;

    Dictionary<int, List<int>> potentialProducts = new Dictionary<int, List<int>>();

    public Dictionary<int, List<int>> PotentialProducts
    {
        get { return potentialProducts; }
    }

    public void Start()
    {
        potentialProducts.Add(11, new List<int> { 18, 18, 18 }); //leftHand
        potentialProducts.Add(14, new List<int> { 19, 19, 19 }); //shoes
        potentialProducts.Add(13, new List<int> { 19, 19, 19, 19, 19 }); //plateArmor
        potentialProducts.Add(12, new List<int> { 19, 19, 19, 19 }); //leggings
        potentialProducts.Add(15, new List<int> { 19, 19 }); //helm
        potentialProducts.Add(16, new List<int> { 18, 21, 22, 23, 24, 25 }); //rightGlove
        potentialProducts.Add(17, new List<int> { 20, 20 }); //bracers
        potentialProducts.Add(26, new List<int> { 18, 18, 18, 18, 18 }); //transmutation amulet
        potentialProducts.Add(27, new List<int> { 18, 18 }); //skeleton scanner 
    }
}
