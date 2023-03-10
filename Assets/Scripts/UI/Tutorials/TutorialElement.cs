using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialElement : MonoBehaviour
{
    [SerializeField] Transform panel;
    [SerializeField] int id;
    bool isFinished;

    public Transform Panel { get { return panel; } }
    public int ID { get { return id; } }
    public bool IsFinished { get { return isFinished; } set { isFinished = value; } }

}
