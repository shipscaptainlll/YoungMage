using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealthDecreaser : MonoBehaviour
{
    [SerializeField] DoorTacklingManager doorTacklingManager;
    [SerializeField] Transform destroyableDoor;
    [SerializeField] ParticleSystem destroyParticleSystem;
    [SerializeField] GameObject motherGameObject;
    [SerializeField] Transform instantiationPoint;
    [SerializeField] Transform PSInstantiationPoint;
    [SerializeField] float healthRegeneration;
    [SerializeField] float maximumWidth;
    [SerializeField] float currentHealth;

    [Header("Audio Connection")]
    [SerializeField] SoundManager soundManager;
    AudioSource doorBlastSound;
    

    float minimalWidth = 0;
    

    RectTransform healthTransform;
    Coroutine healthDecreasingCoroutine;
    Coroutine healthRegenerationCoroutine;
    public event Action HealthReachedZero = delegate { };

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    // Start is called before the first frame update
    void Start()
    {
        healthTransform = transform.Find("Borders").Find("Foreground").GetComponent<RectTransform>();
    }

    public void CalculateDamage(SkeletonBehavior skeleton)
    {
        //currentDamage = skeleton.SkeletonDamage;
    }

    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            currentHealth += healthRegeneration;
            int updatedWidth = CalculateTargetWidth(healthRegeneration);
            if (healthDecreasingCoroutine != null)
            {
                StopCoroutine(healthDecreasingCoroutine);
                healthDecreasingCoroutine = null;
            }
            healthDecreasingCoroutine = StartCoroutine(SmoothHealthDecrease(updatedWidth));

            //Debug.Log("health is being regenerated " + currentHealth);
            if (currentHealth >= 1000) {
                currentHealth = 1000;
                StopRegeneration();
            }


            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    void StopRegeneration()
    {
        if (healthRegenerationCoroutine != null)
        {
            StopCoroutine(healthRegenerationCoroutine);
        }
        
        healthRegenerationCoroutine = null;
    }

    public void DealDamage(float damage)
    {
        //Debug.Log("health before dealing damage is " + currentHealth);
        currentHealth -= damage;
        //Debug.Log("dealt damage " + damage + " current health is " + currentHealth);
        int updatedWidth = CalculateTargetWidth(damage);
        if (healthDecreasingCoroutine != null)
        {
            StopCoroutine(healthDecreasingCoroutine);
            healthDecreasingCoroutine = null;
        }
        healthDecreasingCoroutine = StartCoroutine(SmoothHealthDecrease(updatedWidth));

        if (healthRegenerationCoroutine == null)
        {
            //Debug.Log("instantiated new one");
            healthRegenerationCoroutine = StartCoroutine(RegenerateHealth());
        }
    }

    int CalculateTargetWidth(float damage)
    {
        float leftHealthPercent = ((currentHealth - damage) / maximumWidth) * 100;
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        return (int)(leftHealthPercent * maximumWidth / 100);
    }

    IEnumerator SmoothHealthDecrease(float updatedWidth)
    {
        float counter = 0;
        float smoothingDuration = 0.15f;
        float initialWidth = healthTransform.rect.width;
        float currentWidth = initialWidth;
        while(counter < smoothingDuration)
        {
            counter += Time.deltaTime;
            currentWidth = Mathf.Lerp(initialWidth, updatedWidth, counter / smoothingDuration);
            //Debug.Log(currentWidth);
            healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }
        healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
        if (currentHealth <= 0)
        {
            DestroyDoor();
        }
        yield return null;
    }

    public void UploadDoorsHealth(float uploadedHealth)
    {
        if (healthDecreasingCoroutine != null)
        {
            StopCoroutine(healthDecreasingCoroutine);
            healthDecreasingCoroutine = null;
        }
        currentHealth = uploadedHealth;
        healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalculateTargetWidth(0));
    }

    void DestroyDoor()
    {
        doorTacklingManager.DisconnectSkeleton();
        doorTacklingManager.ConnectedSkeleton.NavigationTarget = null;
        Transform destroyableDoorNew = Instantiate(destroyableDoor, instantiationPoint.position, transform.rotation);
        ParticleSystem destroyPSNew = Instantiate(destroyParticleSystem, PSInstantiationPoint.position, transform.rotation);
        doorBlastSound = soundManager.LocateAudioSource("DoorCaveBlast", destroyPSNew.transform);
        doorBlastSound.Play();
        destroyPSNew.gameObject.SetActive(true);
        destroyPSNew.gameObject.AddComponent<DestroyableParticleSystem>();
        destroyPSNew.gameObject.GetComponent<DestroyableParticleSystem>().TimeDestruction = 7; 
        destroyableDoorNew.Rotate(0, 90, 0);
        motherGameObject.SetActive(false);
    }

    IEnumerator SmoothHealthIncrease(float updatedWidth)
    {
        float counter = 0;
        float smoothingDuration = 0.15f;
        float initialWidth = healthTransform.rect.width;
        float currentWidth = initialWidth;
        while (counter < smoothingDuration)
        {
            counter += Time.deltaTime;
            currentWidth = Mathf.Lerp(0, updatedWidth, counter / smoothingDuration);
            //Debug.Log(currentWidth);
            healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }
        healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
        yield return null;
    }

    void UpdateOreHealth(float healthPercent)
    {
        
    }
}
