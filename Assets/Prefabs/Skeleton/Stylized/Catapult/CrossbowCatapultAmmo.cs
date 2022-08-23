using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrossbowCatapultAmmo : MonoBehaviour, ICatapultAmmo
{
    [SerializeField] ParticleSystem blowEffect;
    [SerializeField] int damage;
    CastleHealthDecreaser castleHealthDecreaser;

    public CastleHealthDecreaser CastleHealthDecreaser { get { return castleHealthDecreaser; } set { castleHealthDecreaser = value; } }

    public ParticleSystem BlowEffect { get { return blowEffect; } }
    public int Damage { get { return damage; } }

    public event Action ammoHitCastle = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.GetComponent<CastleCatapultTarget>() != null)
        {
            castleHealthDecreaser.DealDamage(damage);
            ParticleSystem blowNewEffect = Instantiate(blowEffect, transform.position, transform.rotation);

            blowNewEffect.Play();
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            if (blowEffect != null)
            {
                blowEffect.gameObject.SetActive(true);
                blowEffect.Play();
            }
            StartCoroutine(SelfDestructTimer(3));
        }
    }

    IEnumerator SelfDestructTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
