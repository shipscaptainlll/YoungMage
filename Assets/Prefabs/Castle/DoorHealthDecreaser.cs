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
    float minimalWidth = 0;
    float maximumWidth = 1000;
    float maximumHealth = 1000;
    float currentHealth = 1000;

    int currentDamage;
    RectTransform healthTransform;
    public event Action HealthReachedZero = delegate { };

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    // Start is called before the first frame update
    void Start()
    {
        currentDamage = 50;
        healthTransform = transform.Find("Borders").Find("Foreground").GetComponent<RectTransform>();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DealDamage(100);
        }
    }

    public void CalculateDamage(SkeletonBehavior skeleton)
    {
        currentDamage = 100;
    }

    public void DealDamage(float damage)
    {
        currentHealth -= currentDamage;
        float leftHealthPercent = ((currentHealth - damage) / maximumWidth) * 100;
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        int updatedWidth = (int)(leftHealthPercent * maximumWidth / 100);
        StartCoroutine(SmoothHealthDecrease(updatedWidth));
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

    void DestroyDoor()
    {
        doorTacklingManager.ConnectedSkeleton.NavigationTarget = null;
        Transform destroyableDoorNew = Instantiate(destroyableDoor, instantiationPoint.position, transform.rotation);
        ParticleSystem destroyPSNew = Instantiate(destroyParticleSystem, PSInstantiationPoint.position, transform.rotation);

        destroyPSNew.gameObject.SetActive(true);
        destroyPSNew.gameObject.AddComponent<DestroyableParticleSystem>();
        destroyPSNew.gameObject.GetComponent<DestroyableParticleSystem>().TimeDestruction = 3;
        destroyableDoorNew.Rotate(0, 90, 0);
        Destroy(motherGameObject);
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
