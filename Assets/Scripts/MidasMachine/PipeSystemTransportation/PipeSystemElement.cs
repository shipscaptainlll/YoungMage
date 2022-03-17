using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystemElement : MonoBehaviour
{
    MidasPipesTransmission midasPipesTransmission;

    public MidasPipesTransmission MidasPipesTransmission { set { midasPipesTransmission = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObject()
    {
        midasPipesTransmission.StartCoinTransportation();
        Destroy(this.gameObject);
    }

    void InstantiateCoin()
    {
        midasPipesTransmission.LetCoinOut();
        Destroy(this.gameObject);
    }
}
