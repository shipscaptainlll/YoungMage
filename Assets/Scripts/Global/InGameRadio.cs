using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRadio : MonoBehaviour
{
    [SerializeField] private SoundManager m_soundManager;
    private Coroutine m_waitingSoundCoroutine;
    private AudioSource m_nextSound;
    private AudioSource m_firstBackgroundSound;
    private AudioSource m_secondBackgroundSound;
    private AudioSource m_thirdBackgroundSound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_firstBackgroundSound = m_soundManager.FindSound("FirstBackground");
        m_secondBackgroundSound = m_soundManager.FindSound("SecondBackground");
        m_thirdBackgroundSound = m_soundManager.FindSound("ThirdBackground");
        TurnOnRadio();
    }

    void TurnOnRadio()
    {
        if (m_nextSound == null || m_nextSound == m_firstBackgroundSound)
        {
            m_firstBackgroundSound.Play();
            m_nextSound = m_secondBackgroundSound;
            if (m_waitingSoundCoroutine != null)
            {
                StopCoroutine(m_waitingSoundCoroutine);
                m_waitingSoundCoroutine = null;
            }
            m_waitingSoundCoroutine = StartCoroutine(WaitUntilEnd(568f));
        } else if (m_nextSound == m_secondBackgroundSound)
        {
            m_secondBackgroundSound.Play();
            m_nextSound = m_thirdBackgroundSound;
            if (m_waitingSoundCoroutine != null)
            {
                StopCoroutine(m_waitingSoundCoroutine);
                m_waitingSoundCoroutine = null;
            }
            m_waitingSoundCoroutine = StartCoroutine(WaitUntilEnd(210f));
        } else if (m_nextSound == m_thirdBackgroundSound)
        {
            m_thirdBackgroundSound.Play();
            m_nextSound = m_firstBackgroundSound;
            if (m_waitingSoundCoroutine != null)
            {
                StopCoroutine(m_waitingSoundCoroutine);
                m_waitingSoundCoroutine = null;
            }
            m_waitingSoundCoroutine = StartCoroutine(WaitUntilEnd(580f));
        } 
        
    }

    IEnumerator WaitUntilEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_waitingSoundCoroutine = null;
        TurnOnRadio();
    }

    public void SetVolumeOn(int requiredVolume)
    {
        m_firstBackgroundSound.volume = 0.025f * requiredVolume / 50;
        m_secondBackgroundSound.volume = 0.025f * requiredVolume / 50;
        m_thirdBackgroundSound.volume = 0.025f * requiredVolume / 50;
    }
}
