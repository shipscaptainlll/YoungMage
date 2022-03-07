using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletProductShower : MonoBehaviour
{
    [SerializeField] Transform amuletsHolder;
    [SerializeField] TransmutateAmuletsCounter transmutateAmuletsCounter;

    // Start is called before the first frame update
    void Start()
    {
        transmutateAmuletsCounter.AmuletAdded += VisualizeNextAmulet;
        HideAllAmulets();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void VisualizeNextAmulet()
    {
        //Debug.Log("hello there");
        for (int i = 0; i < transmutateAmuletsCounter.Count; i++)
        {
            //Debug.Log(amuletsHolder.GetChild(i));
            amuletsHolder.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            amuletsHolder.GetChild(i).GetComponent<TransmutationAmulet>().Activated = true;
        }
    }

    void HideAllAmulets()
    {
        foreach (Transform element in amuletsHolder)
        {
            element.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
