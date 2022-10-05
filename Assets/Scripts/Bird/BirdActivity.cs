using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdActivity : MonoBehaviour
{

    [Header("Main Part")]
    [SerializeField] Animator birdAnimator;
    [SerializeField] ParticleSystem feathersPS;
    [SerializeField] SkinnedMeshRenderer bodyMeshRenderer;
    [SerializeField] Transform mageBody;
    Coroutine landThenFlyCoroutine;
    Coroutine birdReturnCoroutine;
    Collider birdCollider;
    Collider mageCollider;

    [Header("Timings")]
    [SerializeField] float returnDelay;

    // Start is called before the first frame update
    void Start()
    {
        birdCollider = transform.GetComponent<Collider>();
        mageCollider = mageBody.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        BirdFlyAway();
    }

    void BirdFlyAway()
    {
        if (!birdAnimator.GetCurrentAnimatorStateInfo(0).IsName("BirdReturn"))
        {
            birdAnimator.Play("FlyAway");
        }
        else
        {
            if (landThenFlyCoroutine == null) { landThenFlyCoroutine = StartCoroutine(LandThenFly()); }
        }
    }

    public void BirdReturn()
    {
        if (birdReturnCoroutine == null) { birdReturnCoroutine = StartCoroutine(ReturnToDefault()); }
    }

    public void CheckPlayerNearby()
    {
        if (birdCollider.bounds.Intersects(mageCollider.bounds)) { BirdFlyAway(); }
    }
    
    IEnumerator LandThenFly()
    {
        yield return new WaitUntil(() => birdAnimator.GetCurrentAnimatorStateInfo(0).IsName("BirdReturn") == false);
        birdAnimator.Play("FlyAway");
        landThenFlyCoroutine = null;
        yield return null;
    }

    IEnumerator ReturnToDefault()
    {
        yield return new WaitForSeconds(returnDelay);
        UnhideBird();
        birdAnimator.Play("BirdReturn");
        birdReturnCoroutine = null;
        yield return null;
    }

    public void LaunchFeathers()
    {
        feathersPS.Play();
    }

    public void HideBird()
    {
        bodyMeshRenderer.enabled = false;
    }

    void UnhideBird()
    {
        bodyMeshRenderer.enabled = true;
    }
}
