using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour, ISkeleton
{
    [SerializeField] string objectType;
    [SerializeField] string skeletonType;
    [SerializeField] Sprite skeletonImage;
    [SerializeField] private SkeletonDamageManager m_skeletonDamageManager;
    private int m_power;
    private float m_speed;
    
    int powerFinal;
    int inventoryPower;
    int speedFinal;
    int inventorySpeed;
    string occupation;
    List<GameObject> appliedInventory;
    private System.Random random;
    public string ObjectType { get { return objectType; } }
    public string SkeletonType { get { return skeletonType; } }
    public Sprite SkeletonImage { get { return skeletonImage; } }
    public string Type { get { return "Normal"; } }
    public int Power { get { return m_power; } set { m_power = value; } }
    public float Speed { get { return m_speed; } set { m_speed = value; } }
    public int FinalPower { get { return powerFinal;} }
    public float FinalSpeed { get { return speedFinal; } }
    
    public string Occupation
    {
        get { return occupation; }
        set { occupation = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random(transform.GetHashCode() + DateTime.Now.Millisecond);
        if (m_power == 0) { GeneratePowers(); }
        UpdatePower();
        occupation = "Making stone ore";
    }

    void UpdatePower()
    {
        m_skeletonDamageManager.UpdateCurrentDamage(m_power);
    }

    void GeneratePowers()
    {
        int randomPower = random.Next(1, 101);
        int randomSpeed = random.Next(1, 101);
        if (skeletonType == "Small Skeleton")
        {
            if (randomPower <= 50) { m_power = 1; }
            else if (randomPower > 50 && randomPower <= 75) { m_power = 2; }
            else if (randomPower > 75 && randomPower <= 90) { m_power = 3; }
            else if (randomPower > 90 && randomPower <= 97) { m_power = 4; }
            else if (randomPower > 97 && randomPower <= 100) { m_power = 5; }
            
            if (randomSpeed <= 50) { m_speed = 1.02f; }
            else if (randomSpeed > 50 && randomSpeed <= 75) { m_speed = 1.2f; }
            else if (randomSpeed > 75 && randomSpeed <= 90) { m_speed = 1.4f; }
            else if (randomSpeed > 90 && randomSpeed <= 97) { m_speed = 1.6f; }
            else if (randomSpeed > 97 && randomSpeed <= 100) { m_speed = 1.8f; }
        } else if (skeletonType == "Big Skeleton")
        {
            if (randomPower <= 50) { m_power = 5; }
            else if (randomPower > 50 && randomPower <= 75) { m_power = 7; }
            else if (randomPower > 75 && randomPower <= 90) { m_power = 9; }
            else if (randomPower > 90 && randomPower <= 97) { m_power = 10; }
            else if (randomPower > 97 && randomPower <= 100) { m_power = 11; }
            
            if (randomSpeed <= 50) { m_speed = 4; }
            else if (randomSpeed > 50 && randomSpeed <= 75) { m_speed = 4.6f; }
            else if (randomSpeed > 75 && randomSpeed <= 90) { m_speed = 5.2f; }
            else if (randomSpeed > 90 && randomSpeed <= 97) { m_speed = 5.6f; }
            else if (randomSpeed > 97 && randomSpeed <= 100) { m_speed = 5.9f; }
            
        } else if (skeletonType == "Lizard Skeleton")
        {
            if (randomPower <= 50) { m_power = 12; }
            else if (randomPower > 50 && randomPower <= 75) { m_power = 15; }
            else if (randomPower > 75 && randomPower <= 90) { m_power = 18; }
            else if (randomPower > 90 && randomPower <= 97) { m_power = 22; }
            else if (randomPower > 97 && randomPower <= 100) { m_power = 25; }
            
            if (randomSpeed <= 50) { m_speed = 10; }
            else if (randomSpeed > 50 && randomSpeed <= 75) { m_speed = 11.1f; }
            else if (randomSpeed > 75 && randomSpeed <= 90) { m_speed = 12.3f; }
            else if (randomSpeed > 90 && randomSpeed <= 97) { m_speed = 13.5f; }
            else if (randomSpeed > 97 && randomSpeed <= 100) { m_speed = 14.9f; }
            
        } 
        
    }
    
}
