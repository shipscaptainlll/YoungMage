using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreLevitator : MonoBehaviour
{
    [SerializeField] ParticleSystem levitationPS;
    [SerializeField] Transform body;
    [SerializeField] MidasResource midasResource;
    Animator levitationAnimator;
    Rigidbody transformRigibody;

    bool levitationActivated;

    public bool LevitationActivated { get { return levitationActivated; } }

    void Start()
    {
        levitationAnimator = transform.GetComponent<Animator>();
        levitationAnimator.enabled = false;
    }

    public void ActivateLevitation()
    {
        if (!levitationActivated && !midasResource.BeingDissolved)
        {
            EaseUpRigidbody();
            levitationActivated = true;
            levitationPS.gameObject.SetActive(true);
            Debug.Log("ready levitation");
            levitationPS.Play();
            levitationAnimator.enabled = true;
            levitationAnimator.Play("Levitation");
        }
    }

    public void DeactivateLevitation()
    {
        if (levitationActivated)
        {
            levitationActivated = false;
            levitationPS.Stop();
            Debug.Log("set dowwn");
            levitationPS.gameObject.SetActive(false);
            levitationAnimator.Play("None");
            levitationAnimator.enabled = false;
        }
    }

    void EaseUpRigidbody()
    {
        Rigidbody bodyRigidbody = body.GetComponent<Rigidbody>();
        bodyRigidbody.velocity = new Vector3(0, 0, 0);
        bodyRigidbody.angularVelocity = new Vector3(0, 0, 0);
        bodyRigidbody.isKinematic = true;
        //bodyRigidbody.useGravity = false;
        Debug.Log("we are here");
        body.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
