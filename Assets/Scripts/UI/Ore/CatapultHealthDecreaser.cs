using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultHealthDecreaser : MonoBehaviour
{
    [SerializeField] Transform masterObject;
    [SerializeField] Transform partsObject;
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            DealDamage(currentDamage);
        }
    }

    public void CalculateDamage()
    {
        currentDamage = 50;
    }

    public void DealDamage(float damage)
    {
        //Debug.Log(currentDamage);
        currentHealth -= currentDamage;
        float leftHealthPercent = ((currentHealth - damage) / maximumWidth) * 100;
        //Debug.Log("hello there");
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
            DestroyCatapult();
        }
        yield return null;
    }

    public void DestroyCatapult()
    {
        Transform parts = Instantiate(partsObject, transform.parent.parent.position, transform.parent.parent.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
        parts.position = transform.parent.parent.position + new Vector3(0, 2f, 0);
        Destroy(masterObject.gameObject);
    }
}
