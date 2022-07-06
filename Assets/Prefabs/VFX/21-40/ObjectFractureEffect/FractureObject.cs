using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FractureObject : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject fracturedObject;
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float explosionMinForce = 5;
    [SerializeField] float explosionMaxForce = 100;
    [SerializeField] float explosionForceRadius = 10;
    [SerializeField] float fragmentationScaleFactor = 1;

    GameObject fracturedObjectInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        } 
        if (Input.GetKeyDown(KeyCode.M))
        {
            ResetExplosion();
        }
    }

    void Explode()
    {
        if (originalObject != null)
        {
            originalObject.SetActive(false);

            if (fracturedObject != null)
            {
                fracturedObjectInstance = Instantiate(fracturedObject) as GameObject;

                fracturedObjectInstance.transform.position = originalObject.transform.position;

                foreach (Transform part in fracturedObjectInstance.transform)
                {
                    var partRigidbody = part.GetComponent<Rigidbody>();
                    if (partRigidbody != null)
                    {
                        partRigidbody.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), originalObject.transform.position, explosionForceRadius);

                    }

                    StartCoroutine(Shrink(part, 2));
                }

                Destroy(fracturedObjectInstance, 5);

                if (explosionParticles != null)
                {
                    GameObject explostionVFXInstance = Instantiate(explosionParticles) as GameObject;
                    explostionVFXInstance.transform.position = originalObject.transform.position;
                    explostionVFXInstance.SetActive(true);
                    explostionVFXInstance.GetComponent<ParticleSystem>().Play();

                    Destroy(explosionParticles, 7);
                }
            }
        }
        
    }

    void ResetExplosion()
    {
        if (fracturedObjectInstance != null)
        {
            Destroy(fracturedObjectInstance);
        }
        originalObject.SetActive(true);
    }

    IEnumerator Shrink(Transform objectShrinking, float delay)
    {

        yield return new WaitForSeconds(delay);

        Vector3 newScale = objectShrinking.localScale;

        while (newScale.x >= 0)
        {
            newScale -= new Vector3(fragmentationScaleFactor, fragmentationScaleFactor, fragmentationScaleFactor);

            objectShrinking.localScale = newScale;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
