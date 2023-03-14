using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    [SerializeField] float timeDestroy;
    Coroutine destroyCoroutine;

    public float TimeDestroy { get { return timeDestroy; } set { timeDestroy = value; } }
    // Start is called before the first frame update
    void Start()
    {
        InitiateDestruction();
    }

    public void InitiateDestruction()
    {
        if (destroyCoroutine != null) { StopCoroutine(destroyCoroutine); }
        destroyCoroutine = StartCoroutine(AutoDestroy());
    }

    IEnumerator AutoDestroy()
    {
        if (timeDestroy == 0)
        {
            timeDestroy = 1.4f;
        }
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
