using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttachedObjects : MonoBehaviour
{

    Transform connectedHands;
    Transform connectedLeggings;
    Transform connectedArmor;
    Transform connectedShoes;
    Transform connectedHelm;
    Transform connectedGloves;
    Transform connectedBracers;


    public Transform ConnectedHands { get { return connectedHands; } set { connectedHands = value; } }
    public Transform ConnectedLeggings { get { return connectedLeggings; } set { connectedLeggings = value; } }
    public Transform ConnectedArmor { get { return connectedArmor; } set { connectedArmor = value; } }
    public Transform ConnectedShoes { get { return connectedShoes; } set { connectedShoes = value; } }
    public Transform ConnectedHelm { get { return connectedHelm; } set { connectedHelm = value; } }
    public Transform ConnectedGloves { get { return connectedGloves; } set { connectedGloves = value; } }
    public Transform ConnectedBracers { get { return connectedBracers; } set { connectedBracers = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
