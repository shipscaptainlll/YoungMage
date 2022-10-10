using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{
    [SerializeField] ShootingSoldier shootingSoldier;
    [SerializeField] SkeletonsStack skeletonsStack;
    [SerializeField] Animator soldierAnimator;
    SoldierState soldierState;
    Coroutine currentCoroutine;

    System.Random rand;
    public enum SoldierState
    {
        shooting,
        shouting,
        showing,
        emotions,
        waiting
    }

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        soldierState = SoldierState.waiting;
        shootingSoldier.ShootedTarget += ContinueShooting;
        skeletonsStack.SkeletonArenaAdded += StartShooting;
        shootingSoldier.TargetsUnavailable += StartWaiting;
        shootingSoldier.TargetEliminated += StartEmotions;
    }

    void DecideNextMove()
    {
        if (soldierState == SoldierState.shooting) { if (currentCoroutine != null) { StopCoroutine(currentCoroutine); } currentCoroutine = StartCoroutine(ShootSkeleton()); }
    }

    IEnumerator ShootSkeleton()
    {
        
        yield return new WaitForSeconds(3);
        shootingSoldier.Shoot();
        currentCoroutine = null;
    }

    void ContinueShooting()
    {
        //Debug.Log("shooting");
        if (soldierState != SoldierState.waiting)
        {
            soldierState = SoldierState.shooting;
            DecideNextMove();
        }
        
    }

    void StartWaiting()
    {
        soldierState = SoldierState.waiting;
        //Debug.Log("waiting");
    }

    void StartShooting()
    {
        if (soldierState == SoldierState.waiting)
        {
            //Debug.Log("started shooting");
            soldierState = SoldierState.shooting;
            DecideNextMove();
        }
    }

    void StartEmotions()
    {
        StopCoroutine(currentCoroutine);
        int randomNumber = rand.Next(0, 10);
        if (randomNumber < 2) { soldierAnimator.Play("SoldierShowingSmthg"); }
        else if (randomNumber > 1 && randomNumber < 5) { soldierAnimator.Play("Shouting"); }
        else
        {
            soldierState = SoldierState.shooting;
            DecideNextMove();
        }
    }

    public void StopEmotions()
    {
        Debug.Log("stopped emotions");
        soldierState = SoldierState.shooting;
        DecideNextMove();
    }
}
