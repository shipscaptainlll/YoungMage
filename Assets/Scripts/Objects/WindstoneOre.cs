using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindstoneOre : MonoBehaviour, IOre
{
    [SerializeField] Transform firstProductInstance;
    [SerializeField] Transform secondProductInstance;
    [SerializeField] string objectType;
    [SerializeField] string type;
    [SerializeField] int health;
    [SerializeField] int hardness;
    [SerializeField] int regeneration;
    [SerializeField] Sprite firstProductImage;
    [SerializeField] Sprite secondProductImage;
    [SerializeField] int firstProductChances;
    [SerializeField] int secondProductChances;

    string objectOccupation;


    string occupation;
    public Transform FirstProductInstance { get { return firstProductInstance; } }
    public Transform SecondProductInstance { get { return secondProductInstance; } }
    public string ObjectType { get { return objectType; } }
    public string Type { get { return type; } }
    public int Health { get { return health; } }
    public int Hardness { get { return hardness; } }
    public int Regeneration { get { return regeneration; } }
    public Sprite FirstProductImage { get { return firstProductImage; } }
    public Sprite SecondProductImage { get { return secondProductImage; } }
    public int FirstProductChances { get { return firstProductChances; } }
    public int SecondProductChances { get { return secondProductChances; } }
    public string ObjectOccupation { get { return objectOccupation; } }

    

    public string ProductionType
    {
        get
        {
            return "WindstoneOreDust";
        }
    }

    public int ProductionPerCycle
    {
        get
        {
            return 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        objectOccupation = "not doing right now anything";
    }
}
