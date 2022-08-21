using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultFire : MonoBehaviour
{
    [SerializeField] Transform catapultAmmo;
    [SerializeField] Transform shootingStart;
    Animator catapultAnimator;
    bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        catapultAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (!isFiring)
        {
            isFiring = true;
            catapultAnimator.Play("CatapultFire");
            ShootAmmo();
        }
        
    }

    void ShootAmmo()
    {
        Transform newAmmo = Instantiate(catapultAmmo);
        newAmmo.position = shootingStart.position;
        newAmmo.GetComponent<Rigidbody>().velocity = transform.forward * 25 + -transform.right * 25;
    }

    public void CheckNotFiring()
    {
        isFiring = false;
    }
}
