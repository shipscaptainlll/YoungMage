using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseUpgradeEffect : MonoBehaviour
{
    ParticleSystem houseUpgradeEffect;

    // Start is called before the first frame update
    void Start()
    {
        houseUpgradeEffect = transform.Find("HouseUpgradeEffect").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) { houseUpgradeEffect.Play(); }
    }
}
