using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasCauldron : MonoBehaviour
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
        contactManager.MidasCauldronDetected += CalculateBehavior;
        isWorking = false;
        productionCost = 10;
        productAmmount = 1;
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
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < productAmmount; i++)
        {
            InvokeProduct();
        }
        isWorking = false;
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
            float xR = (float)random.Next(-180, 180);
            float yR = (float)random.Next(-180, 180);
            float zR = (float)random.Next(-180, 180);
            Vector3 randomAddition = new Vector3(x, 0, y);
            Vector3 randomRotation = new Vector3(xR, zR, yR);
            GameObject newShinyResource = Instantiate(resourceMesh, resourceInvoker.position + randomAddition, resourceInvoker.rotation * Quaternion.Euler(randomRotation));

            Destroy(newShinyResource, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
