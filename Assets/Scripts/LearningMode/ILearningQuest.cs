using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILearningQuest
{
    public int questID { get; set; }

    public void ActivateQuestSequence();
}
