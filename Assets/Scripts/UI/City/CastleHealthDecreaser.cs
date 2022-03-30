using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthDecreaser : MonoBehaviour
{

    float minimalWidth = 0;
    float maximumWidth = 1000;
    float currentHealth = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        float leftHealthPercent = ((currentHealth - damage) / maximumWidth) * 100;
        Debug.Log(leftHealthPercent);
        leftHealthPercent = Mathf.Clamp(leftHealthPercent, 0, 100);
        UpdateCastleHealth(leftHealthPercent);
    }

    void UpdateCastleHealth(float healthPercent)
    {
        int updatedWidth = (int) (healthPercent * maximumWidth / 100);
        transform.Find("Borders").Find("Foreground").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updatedWidth);
    }
}
