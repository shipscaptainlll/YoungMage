using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
    internal int Sides { get; set; }
}

class Square : Shape
{
    public int GetSides()
    {
        return this.Sides;
    }
    void Start()
    {
        Shape sq = new Square();
        sq.Sides = 4;
    }
}

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
