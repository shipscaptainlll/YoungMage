using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.VFX;

public class SmallSkeletonAmmo : MonoBehaviour
{
    [SerializeField] ParticleSystem blowEffect;
    [SerializeField] int damage;
    [SerializeField] VisualEffect movementVFX;
    CastleHealthDecreaser castleHealthDecreaser;
    ParticleSystem instantiatedBlowEffect;

    Coroutine movingCoroutine;
    Coroutine destructionCoroutine;
    public CastleHealthDecreaser CastleHealthDecreaser { get { return castleHealthDecreaser; } set { castleHealthDecreaser = value; } }

    public ParticleSystem BlowEffect { get { return blowEffect; } }
    public int Damage { get { return damage; } }

    void Start()
    {
        
    }

    public void ActivateVFX()
    {
        movingCoroutine = StartCoroutine(MovingDustSpawner());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 18)
        {
            if (destructionCoroutine == null)
            {
                destructionCoroutine = StartCoroutine(SelfDestructTimer(3));
            }
            castleHealthDecreaser.DealDamage(damage);
            StopCoroutine(movingCoroutine);
            instantiatedBlowEffect = Instantiate(blowEffect, transform.position, transform.rotation);
            //Debug.Log("hit castle ");
            instantiatedBlowEffect.Play();
            transform.GetComponent<MeshRenderer>().enabled = false;
            //transform.gameObject.SetActive(false);
            if (blowEffect != null)
            {
                blowEffect.gameObject.SetActive(true);
                blowEffect.Play();
            }
            
        }
    }

    IEnumerator MovingDustSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            movementVFX.SendEvent("CharacterMoved");
        }
    }

    IEnumerator SelfDestructTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopCoroutine(movingCoroutine);
        //Debug.Log("destroyed " + transform + " " + instantiatedBlowEffect.gameObject);
        if (instantiatedBlowEffect != null)
        {
            Destroy(instantiatedBlowEffect.gameObject);
        }
        
        Destroy(gameObject);
    }
}
