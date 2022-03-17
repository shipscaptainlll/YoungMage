using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCoinsCatcher : MonoBehaviour
{
    [SerializeField] Transform midasCoinsCatcher;

    public Transform MidasCoinsCatcher { set { midasCoinsCatcher = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GlobalResource>() != null && other.GetComponent<GlobalResource>().ID == 1)
        {
            Destroy(other.gameObject);
            midasCoinsCatcher.GetComponent<MidasCoinsCatcher>().AddToCount();
            midasCoinsCatcher.GetComponent<MidasCoinsCatcher>().CountCoins();
        }
    }
}
