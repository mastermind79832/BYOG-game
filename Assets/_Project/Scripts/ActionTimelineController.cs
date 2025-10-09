using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionTimelineController : MonoBehaviour
{
    [SerializeField] private Toggle[] m_ActionSequence;

    [SerializeField] private ActionTypeEnum m_ActionType;
    public ActionTypeEnum ActionType => m_ActionType;

    public bool[] GetActionSequence()
    {
        bool[] actionSequence = new bool[m_ActionSequence.Length];
        for (int i = 0; i < m_ActionSequence.Length; i++)
        {
            actionSequence[i] = m_ActionSequence[i].isOn;
        }
        return actionSequence;
    }

    internal void OnActionReset()
    {
        for (int i = 0; i < m_ActionSequence.Length; i++)
        {
            m_ActionSequence[i].isOn = false;
        }
    }
}
