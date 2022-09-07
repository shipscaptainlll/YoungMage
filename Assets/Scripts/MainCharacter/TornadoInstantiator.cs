using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
using UnityEngine.Events;
using System;

public class TornadoInstantiator : MonoBehaviour
{
    [SerializeField] VisualEffect tornadoVFX;
    [SerializeField] Transform body;
    [SerializeField] Vector3 offsetRotation;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] BookSpellsActivator bookSpellsActivator;
    ParticleSystem windParticles;
    ParticleSystem dustParticles;
    ParticleSystem.MainModule windParticlesMainmodule;
    ParticleSystem.MainModule dustParticlesMainmodule;
    RaycastHit[] hitObjects;
    RaycastHit hit;

    Coroutine turnOffCoroutine;
    int tornadoCountQuests;
    public int TornadoCountQuests { get { return tornadoCountQuests; } }
    public event Action<int> TornadoInstantiatedQuests = delegate { };
// Start is called before the first frame update
void Start()
    {
        tornadoVFX.transform.gameObject.SetActive(true);
        windParticles = tornadoVFX.transform.Find("WindParticles").GetComponent<ParticleSystem>();
        dustParticles = tornadoVFX.transform.Find("DustParticles").GetComponent<ParticleSystem>();
        windParticlesMainmodule = tornadoVFX.transform.Find("WindParticles").GetComponent<ParticleSystem>().main;
        dustParticlesMainmodule = tornadoVFX.transform.Find("DustParticles").GetComponent<ParticleSystem>().main;
        tornadoVFX.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            cameraShake.Activated = true;
            windParticles.Play();
            dustParticles.Play();
            //bookSpellsActivator.CastTornadoSpell();

            Debug.Log("hello there");
            tornadoCountQuests++;
            if (TornadoInstantiatedQuests != null) { TornadoInstantiatedQuests(TornadoCountQuests); }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            hitObjects = Physics.RaycastAll(transform.position, transform.forward);
            foreach (var result in hitObjects)
            {
                if (result.transform.gameObject.layer == 6 && result.transform.name != "Terrain")
                {
                    Debug.Log("hello there");
                    if (turnOffCoroutine != null) { StopCoroutine(turnOffCoroutine); }
                    tornadoVFX.SetFloat("TornadoLayer1Dissolve1", 0.84f);
                    tornadoVFX.SetFloat("TornadoLayer1Dissolve2", 0.935f);
                    tornadoVFX.SetFloat("TornadoCoreDissolve", 0.61f);
                    tornadoVFX.transform.gameObject.SetActive(true);
                    

                    windParticlesMainmodule.loop = true;
                    dustParticlesMainmodule.loop = true;
                    tornadoVFX.Play();
                    return;
                }
            }
        }

        if (Input.GetKey(KeyCode.Y))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hitObjects = Physics.RaycastAll(transform.position, transform.forward);
            foreach (var result in hitObjects)
            {
                if (result.transform.gameObject.layer == 6 && result.transform.name != "Terrain")
                {
                    //Debug.Log(result.transform);
                    tornadoVFX.transform.position = result.point + new Vector3(0, 0.5f, 0);
                    tornadoVFX.transform.rotation = Quaternion.Euler(body.rotation.eulerAngles + offsetRotation);
                    return;
                } else { tornadoVFX.transform.position = tornadoVFX.transform.position; }
            }
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            TurnOffTornado();
            cameraShake.Activated = false;
            //tornadoVFX.gameObject.SetActive(false);

        }
    }

    void TurnOffTornado()
    {
        if (turnOffCoroutine != null) { StopCoroutine(turnOffCoroutine); }
        turnOffCoroutine = StartCoroutine(SmoothTurnOff(2.35f));
        windParticlesMainmodule.loop = false;
        dustParticlesMainmodule.loop = false;
    }

    IEnumerator SmoothTurnOff(float delay)
    {
        float elapsed = 0;
        float firstLayerStart = tornadoVFX.GetFloat("TornadoLayer1Dissolve1");
        float secondLayerStart = tornadoVFX.GetFloat("TornadoLayer1Dissolve2");
        float coreLayerStart = tornadoVFX.GetFloat("TornadoCoreDissolve");
        float firstLayerValue = 0;
        float secondLayerValue = 0;
        float coreLayerValue = 0;
        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            firstLayerValue = Mathf.Lerp(firstLayerStart, 1, elapsed / delay);
            secondLayerValue = Mathf.Lerp(secondLayerStart, 1, elapsed / delay);
            coreLayerValue = Mathf.Lerp(coreLayerStart, 1, elapsed / delay);
            tornadoVFX.SetFloat("TornadoLayer1Dissolve1", firstLayerValue);
            tornadoVFX.SetFloat("TornadoLayer1Dissolve2", secondLayerValue);
            tornadoVFX.SetFloat("TornadoCoreDissolve", coreLayerValue);
            yield return null;
        }
        tornadoVFX.SetFloat("TornadoLayer1Dissolve1", 1);
        tornadoVFX.SetFloat("TornadoLayer1Dissolve2", 1);
        tornadoVFX.SetFloat("TornadoCoreDissolve", 1);
        tornadoVFX.gameObject.SetActive(false);
    }
}
