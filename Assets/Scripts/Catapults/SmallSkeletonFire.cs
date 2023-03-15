using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSkeletonFire : MonoBehaviour
{
    [SerializeField] Transform ammo;
    [SerializeField] Transform shootingStart;
    [SerializeField] Transform finalPositionTransform;
    [SerializeField] Transform shootedAmmoHolder;
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;
    Animator catapultAnimator;

    // Start is called before the first frame update
    void Start()
    {
        catapultAnimator = transform.GetComponent<Animator>();
    }

    public void Fire()
    {
        Vector2 calculatedVelocities = CalculateVelocities();
        ShootAmmo(calculatedVelocities.x, calculatedVelocities.y);
    }

    void ShootAmmo(float xVelocity, float yVelocity)
    {
        Transform newAmmo = Instantiate(ammo);
        newAmmo.GetComponent<SmallSkeletonAmmo>().CastleHealthDecreaser = castleHealthDecreaser;
        newAmmo.GetComponent<SmallSkeletonAmmo>().ActivateVFX();
        newAmmo.position = shootingStart.position;
        newAmmo.rotation = shootingStart.rotation;
        newAmmo.parent = shootedAmmoHolder;
        newAmmo.GetComponent<Rigidbody>().velocity = transform.up * 25 + transform.forward * xVelocity * 4.5f;
        newAmmo.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -15));
    }

    Vector2 CalculateVelocities()
    {
        Vector2 calculatedVelocities;
        Vector3 initialPosition = shootingStart.position;
        Vector3 finalPosition = finalPositionTransform.position;
        float Distance = Vector3.Distance(initialPosition, finalPosition);
        float gravity = Physics.gravity.y;
        float tangAngle = Mathf.Tan(45 * Mathf.Deg2Rad);
        float height = finalPosition.y - initialPosition.y;
        float xVelocity = Mathf.Sqrt(gravity * Distance * Distance / (2f * (height - Distance * tangAngle)));
        float yVelocity = tangAngle * xVelocity;
        calculatedVelocities = new Vector2(xVelocity, yVelocity);
        //Debug.Log(calculatedVelocities);
        return calculatedVelocities;
    }

}
