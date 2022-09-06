using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MagibookMainmenu : MonoBehaviour
{
    [SerializeField] ParticleSystem bookOpeningPS;
    [SerializeField] ParticleSystem OpeningPSSecond;
    [SerializeField] ParticleSystem floatingParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateVFX()
    {
        bookOpeningPS.gameObject.SetActive(true);
        bookOpeningPS.Play();
        OpeningPSSecond.gameObject.SetActive(true);
        OpeningPSSecond.Play();
        floatingParticles.gameObject.SetActive(true);
        floatingParticles.Play();
    }
}
