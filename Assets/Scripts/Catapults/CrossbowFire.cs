using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowFire : MonoBehaviour
{
    [SerializeField] Transform catapultAmmo;
    [SerializeField] Transform shootingStart;
    CastleHealthDecreaser castleHealthDecreaser;
    Animator catapultAnimator;
    bool isFiring;

    public CastleHealthDecreaser CastleHealthDecreaser { get { return castleHealthDecreaser; } set { castleHealthDecreaser = value; } }
    // Start is called before the first frame update
    void Start()
    {
        catapultAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (!isFiring)
        {
            isFiring = true;
            catapultAnimator.Play("CrossbowFire");
            ShootAmmo();
        }
        
    }

    void ShootAmmo()
    {
        Transform newAmmo = Instantiate(catapultAmmo);
        newAmmo.position = shootingStart.position;
        newAmmo.rotation = shootingStart.rotation;
        newAmmo.GetComponent<Rigidbody>().velocity = -transform.parent.up * 25 + transform.parent.forward * 195;
        newAmmo.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -15));
        newAmmo.GetComponent<CrossbowCatapultAmmo>().CastleHealthDecreaser = castleHealthDecreaser;
    }

    public void CheckNotFiring()
    {
        isFiring = false;
    }
}
