using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVFXInstantiator : MonoBehaviour
{
    [SerializeField] ParticleSystem transmutationCirclePS;
    Coroutine transmutationCircleCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowTransmutationCirlce();
        }
    }

    void ShowTransmutationCirlce()
    {
        var newParticleSystem = Instantiate(transmutationCirclePS, transmutationCirclePS.transform.position, transmutationCirclePS.transform.rotation);
        newParticleSystem.gameObject.SetActive(true);
        newParticleSystem.Play();
        StartCoroutine(TransmutationCircleCoroutine(newParticleSystem.gameObject));
        /*
        if (transmutationCircleCoroutine == null)
        {
            StopCoroutine(TransmutationCircleCoroutine());
            transmutationCircleCoroutine = null;
        }

        transmutationCircleCoroutine = StartCoroutine(TransmutationCircleCoroutine());
        */
    }

    IEnumerator TransmutationCircleCoroutine(GameObject gameObjectDelete)
    {
        /*
        transmutationCirclePS.gameObject.SetActive(true);
        transmutationCirclePS.Play();
        yield return new WaitForSeconds(0.35f);
        transmutationCirclePS.Stop();
        transmutationCircleCoroutine = null;
        */
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObjectDelete);
    }
}
