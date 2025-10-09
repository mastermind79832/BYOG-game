using UnityEngine;


public struct ActionState
{
    public ActionTypeEnum type;
    public bool isActive;
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
        }

        return actions;
    }

    public void OnActionReset()
    {
        for (int i = 0; i < actionTimelineControllers.Length; i++)
        {
            actionTimelineControllers[i].OnActionReset();
        }
    }
}
