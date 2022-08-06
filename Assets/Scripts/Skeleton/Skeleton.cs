using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour, ISkeleton
{
    [SerializeField] string objectType;
    [SerializeField] string skeletonType;
    [SerializeField] Sprite skeletonImage;
    [SerializeField] int power;
    [SerializeField] int speed;

    int powerFinal;
    int inventoryPower;
    int speedFinal;
    int inventorySpeed;
    string occupation;
    List<GameObject> appliedInventory;

    public string ObjectType { get { return objectType; } }
    public string SkeletonType { get { return skeletonType; } }
    public Sprite SkeletonImage { get { return skeletonImage; } }
    public string Type { get { return "Normal"; } }
    public int Power { get { return power; } }
    public int Speed { get { return speed; } }
    public int FinalPower { get { return powerFinal; } }
    public int FinalSpeed { get { return speedFinal; } }
    public int InventoryPower { 
        get { return inventoryPower; }
        set { 
            inventoryPower = value;
            powerFinal = power + inventoryPower;
        }
    }
    public int InventorySpeed { 
        get { return inventorySpeed; }
        set { 
            inventorySpeed = value;
            speedFinal = speed + inventorySpeed;
        }
    }
    public string Occupation
    {
        get { return occupation; }
        set { occupation = value; }
    }
    public List<GameObject> AppliedInventory
    {
        get { return appliedInventory; }
        set { appliedInventory = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        occupation = "Making stone ore";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
