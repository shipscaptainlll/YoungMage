using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defractor : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] StoneOreCounter stoneOreCounter;
    [SerializeField] GameObject resourceMesh;
    [SerializeField] Transform resourceInvoker;
    [SerializeField] GameObject productMesh;
    [SerializeField] Transform productInvoker;
    int productionCost;
    int productAmmount;
    bool isWorking;
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        contactManager.DefractorDetected += CalculateBehavior;
        isWorking = false;
        productionCost = 10;
        productAmmount = 2;
        GetComponent<Animator>().Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateBehavior()
    {
        if (stoneOreCounter.Count >= productionCost && !isWorking)
        {
            StartCoroutine(Work());
            stoneOreCounter.GetResource(productionCost);
        } else if (isWorking)
        {

        }
    }

    IEnumerator Work()
    {
        StartCoroutine(InvokeResource(productionCost));
        isWorking = true;
        GetComponent<Animator>().Play("Working");
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < productAmmount; i++)
        {
            InvokeProduct();
        }
        isWorking = false;
        GetComponent<Animator>().Play("Idle");
    }

    void InvokeProduct()
    {
        GameObject newShinyProduct = Instantiate(productMesh, productInvoker.position, productInvoker.rotation);

        Destroy(newShinyProduct, 5);
    }

    IEnumerator InvokeResource(int cyclesCount)
    {
        for (int i = 0; i < cyclesCount; i++)
        {
            int modifier = 20;
            float x = (float)random.Next(-10, 10) / modifier;
            float y = (float)random.Next(-10, 10) / modifier;
            Vector3 randomAddition = new Vector3(x, 0, y);
            GameObject newShinyResource = Instantiate(resourceMesh, resourceInvoker.position + randomAddition, resourceInvoker.rotation);

            Destroy(newShinyResource, 2);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
