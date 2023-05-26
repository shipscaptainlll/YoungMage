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
    void Awake()
    {
        volume = gameObject.GetComponent<Volume>();
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            
            
        }
    }
    
    
    public void  BlurScreen()
    {
        StartCoroutine(BlurAfterDelay());
        
    }

    IEnumerator BlurAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        depthOfField.focusDistance.Override(0.1f);
        depthOfField.focalLength.Override(300f);
        depthOfField.aperture.Override(32f);
        depthOfField.bladeCount.Override(5);
        depthOfField.bladeCurvature.Override(1f);
        depthOfField.bladeRotation.Override(-5f);
    }

    public void UnBlurScreen()
    {
        StartCoroutine(UnblurAfterDelay());
    }

    IEnumerator UnblurAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        depthOfField.focusDistance.Override(10f);
        depthOfField.focalLength.Override(50f);
        depthOfField.aperture.Override(5.6f);
        depthOfField.bladeCount.Override(5);
        depthOfField.bladeCurvature.Override(1f);
        depthOfField.bladeRotation.Override(0f);
    }
}
