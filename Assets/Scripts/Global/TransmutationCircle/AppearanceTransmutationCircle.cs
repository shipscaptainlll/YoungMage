using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceTransmutationCircle : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] ParticleSystem outerCirclePS;
    [SerializeField] ParticleSystem innerCirclePS;

    [Header("AppearanceSettings")]
    [SerializeField] Gradient appearanceGradient;
    [SerializeField] Gradient disappearanceGradient;
    Coroutine circleCoroutine;

    ParticleSystem.Particle[] outerParticles = new ParticleSystem.Particle[1];
    ParticleSystem.Particle[] innerParticles = new ParticleSystem.Particle[1];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CircleAppearance();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CircleDisappearance();
        }
    }

    public void CircleAppearance()
    {
        if (circleCoroutine != null) { StopCoroutine(circleCoroutine); }
        circleCoroutine = StartCoroutine(AppearCircle());
    }

    public void CircleDisappearance()
    {
        if (circleCoroutine != null) { StopCoroutine(circleCoroutine); }
        circleCoroutine = StartCoroutine(DisappearCircle());
    }

    IEnumerator AppearCircle()
    {
        outerCirclePS.gameObject.SetActive(false);
        outerCirclePS.gameObject.SetActive(true);
        var outerColor = outerCirclePS.colorOverLifetime;
        outerColor.color = appearanceGradient;
        if (innerCirclePS != null)
        {
            var innerColor = innerCirclePS.colorOverLifetime;
            innerColor.color = appearanceGradient;
        }
        outerCirclePS.Play();
        yield return null;
    }

    IEnumerator DisappearCircle()
    {
        outerParticles = new ParticleSystem.Particle[1];
        outerCirclePS.GetParticles(outerParticles);
        if (innerCirclePS != null)
        {
            innerParticles = new ParticleSystem.Particle[1];
            innerCirclePS.GetParticles(innerParticles);
        }
        outerCirclePS.gameObject.SetActive(false);

        if (outerParticles.Length > 0)
        {
            var smthg = outerCirclePS.main;
            smthg.startRotationZ = outerParticles[0].rotation3D.z * Mathf.Deg2Rad;
        }
        if (innerCirclePS != null)
        {
            if (innerParticles.Length > 0)
            {
                var smthg = innerCirclePS.main;
                smthg.startRotationZ = innerParticles[0].rotation3D.z * Mathf.Deg2Rad;
            }
        }

        outerCirclePS.gameObject.SetActive(true);

        var color = outerCirclePS.colorOverLifetime;
        color.color = disappearanceGradient;
        if (innerCirclePS != null)
        {
            var innerColor = innerCirclePS.colorOverLifetime;
            innerColor.color = disappearanceGradient;
        }
        outerCirclePS.Play();
        yield return new WaitForSeconds(1f);
        outerCirclePS.gameObject.SetActive(false);
        yield return null;
    }
}
