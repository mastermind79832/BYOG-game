using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionTimelineController : MonoBehaviour
{
    [SerializeField] private Toggle[] m_ActionSequence;

    [SerializeField] private ActionTypeEnum m_ActionType;
    public ActionTypeEnum ActionType => m_ActionType;

    public AudioClip BeatIndex;

    void Awake()
    {
        foreach (Toggle toggle in m_ActionSequence)
        {
            toggle.gameObject.AddComponent<ToggleSound>().actionTimelineController = this;
        }
    }

    public bool[] GetActionSequence()
    {
        bool[] actionSequence = new bool[m_ActionSequence.Length];
        for (int i = 0; i < m_ActionSequence.Length; i++)
        {
            actionSequence[i] = m_ActionSequence[i].isOn;
        }
        return actionSequence;
    }

    public float[] GetPitchofSequence()
    {
        float[] pitchofSequence = new float[m_ActionSequence.Length];
        for (int i = 0; i < m_ActionSequence.Length; i++)
        {
            pitchofSequence[i] = m_ActionSequence[i].GetComponent<ToggleSound>().pitch;
        }
        return pitchofSequence;
    }

    internal void OnActionReset()
    {
        for (int i = 0; i < m_ActionSequence.Length; i++)
        {
            m_ActionSequence[i].isOn = false;
            m_ActionSequence[i].GetComponent<ToggleSound>().pitch = UnityEngine.Random.Range(-3f, 3f);
        }
    }

}
