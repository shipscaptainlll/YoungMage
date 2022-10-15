using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSoldier : MonoBehaviour
{
    [SerializeField] Transform cacheTarget;
    Transform target;
    Coroutine destroyObject;

    public Transform Target { get { return target; } set { target = value; } }
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
        if (target != null) { target.GetComponent<SkeletonHealthDecreaser>().DecreaseHealth(); 
            //Debug.Log("Arrow missed " + target);
        }
        Destroy(gameObject);
    }
}
