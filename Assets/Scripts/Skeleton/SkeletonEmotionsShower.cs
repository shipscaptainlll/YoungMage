using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEmotionsShower : MonoBehaviour
{
    [SerializeField] Transform confusionTransform;
    [SerializeField] AnimationCurve animationCurve;

    Coroutine emotionCoroutine;

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShowConfusion();
        }
        */
    }
    public void ShowConfusion()
    {
        ShowEmotion(confusionTransform);
    }

    void ShowEmotion(Transform emotion)
    {
        if (emotionCoroutine != null) { StopCoroutine(emotionCoroutine); }
        emotionCoroutine = StartCoroutine(EmotionDynamic(emotion, 3));
        ChangeTransparency(emotion, 1);
    }

    IEnumerator EmotionDynamic(Transform emotion, float duration)
    {
        Transform rotatedTransform = emotion.GetChild(0).GetChild(0);
        float elapsed = 0;
        Vector3 startRotation = new Vector3(0, 0, 0);
        Vector3 currentRotation = rotatedTransform.localRotation.eulerAngles;
        float zRotation;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            zRotation = Mathf.Lerp(-90, 90, animationCurve.Evaluate(elapsed / duration));
            currentRotation = new Vector3(currentRotation.x, currentRotation.y, zRotation);
            rotatedTransform.localRotation = Quaternion.Euler(currentRotation);
            yield return null;
        }
        rotatedTransform.localRotation = Quaternion.Euler(startRotation);
        ChangeTransparency(emotion, 0);
        yield return null;
    }

    void ChangeTransparency(Transform emotion, float value)
    {
        emotion.GetComponent<CanvasGroup>().alpha = value;
    }
}
