using System;
using System.Collections.Generic;
using UnityEngine;


public struct ActionState
{
    public ActionTypeEnum type;
    public bool isActive;

    public int BeatIndex;
    public float Pitch;
}

public class ActionManager : MonoBehaviour
{
    [SerializeField] private ActionTimelineController[] actionTimelineControllers;


    public ActionState[] GetActionsOfIndex(int index)
    {
        ActionState[] actions = new ActionState[actionTimelineControllers.Length];
        for (int i = 0; i < actionTimelineControllers.Length; i++)
        {
            actions[i].type = actionTimelineControllers[i].ActionType;
            actions[i].isActive = actionTimelineControllers[i].GetActionSequence()[index];
            actions[i].BeatIndex = actionTimelineControllers[i].BeatIndex;
            actions[i].Pitch = actionTimelineControllers[i].GetPitchofSequence()[index];
        }

        return actions;
    }

    public void OnActionReset(List<AudioClip> beatTunes)
    {
        int index = 0;
        for (int i = 0; i < actionTimelineControllers.Length; i++)
        {
            if (beatTunes != null && beatTunes.Count > 0)
            {
                index = UnityEngine.Random.Range(0, beatTunes.Count);
                beatTunes.RemoveAt(index);
            }         
            actionTimelineControllers[i].OnActionReset();
            actionTimelineControllers[i].BeatIndex = index;
        }
    }
}
