using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSoldier : MonoBehaviour
{
    [SerializeField] Transform cacheTarget;
    Coroutine destroyObject;
    // Start is called before the first frame update
    void Start()
    {
        destroyObject = StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 16)
        {
            //Debug.Log("Arrow hitted skeleton");
            other.GetComponent<SkeletonHealthDecreaser>().DecreaseHealth();
            StopCoroutine(destroyObject);
            Destroy(gameObject);
        }

    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2);
        //Debug.Log("Arrow missed");
        Destroy(gameObject);
    }
}
