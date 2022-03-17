using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasCoinsCatcher : MonoBehaviour
{
    [SerializeField] CoinsAccumulationModels coinsAccumulationModels;
    [SerializeField] Transform coinsAccumulationPosition;
    GameObject currentAccumulationForm = null;
    int coinsCount = 0;

    public event Action<Transform> CollectionUpdated = delegate { };
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
            AddToCount();
            CountCoins();
        }
    }

    public void AddToCount()
    {
        coinsCount += 10;
    }

    public void CountCoins()
    {
        GameObject potentialAccumulationForm = coinsAccumulationModels.TakeModel(coinsCount);
        if (currentAccumulationForm != potentialAccumulationForm && potentialAccumulationForm != null)
        {
            Debug.Log(potentialAccumulationForm);
            UpdateAccumulationState(potentialAccumulationForm);
        }
    }

    void UpdateAccumulationState(GameObject finalAccumulationForm)
    {
        if (currentAccumulationForm != null) { Destroy(currentAccumulationForm);
            currentAccumulationForm = null;
        }

        if (coinsCount < 2500)
        {
            currentAccumulationForm = Instantiate(finalAccumulationForm, coinsAccumulationPosition.position, Quaternion.Euler(new Vector3(90, 0, 0)));
        } else { currentAccumulationForm = Instantiate(finalAccumulationForm, coinsAccumulationPosition.position, Quaternion.Euler(new Vector3(0, 0, 0))); }
        
        currentAccumulationForm.AddComponent<AdditionalCoinsCatcher>();
        currentAccumulationForm.GetComponent<AdditionalCoinsCatcher>().MidasCoinsCatcher = transform;
        currentAccumulationForm.AddComponent<Rigidbody>();
        currentAccumulationForm.AddComponent<MeshCollider>().convex = true;
    }
}
