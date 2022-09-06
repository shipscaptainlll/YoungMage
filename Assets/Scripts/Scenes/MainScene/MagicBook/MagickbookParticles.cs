using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagickbookParticles : MonoBehaviour
{
    [SerializeField] Transform magickbookParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableParticleSystem()
    {
        magickbookParticleSystem.gameObject.SetActive(true);
        magickbookParticleSystem.GetComponent<ParticleSystem>().Play();
    }
}
