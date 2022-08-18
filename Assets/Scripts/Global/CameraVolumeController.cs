using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraVolumeController : MonoBehaviour
{
    Volume volume;
    static DepthOfField depthOfField;

    // Start is called before the first frame update
    void Start()
    {
        volume = gameObject.GetComponent<Volume>();
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void  BlurScreen()
    {
        depthOfField.focusDistance.Override(0.1f);
        depthOfField.focalLength.Override(300f);
        depthOfField.aperture.Override(32f);
        depthOfField.bladeCount.Override(5);
        depthOfField.bladeCurvature.Override(1f);
        depthOfField.bladeRotation.Override(-5f);
    }

    public static void UnBlurScreen()
    {
        depthOfField.focusDistance.Override(10f);
        depthOfField.focalLength.Override(50f);
        depthOfField.aperture.Override(5.6f);
        depthOfField.bladeCount.Override(5);
        depthOfField.bladeCurvature.Override(1f);
        depthOfField.bladeRotation.Override(0f);
    }
}
