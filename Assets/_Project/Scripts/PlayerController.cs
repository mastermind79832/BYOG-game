using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ActionManager m_ActionManager;
    [SerializeField] private float m_MoveSpeed = 5f;
    [SerializeField] private float m_JumpForce = 10f;
    [SerializeField] private Rigidbody2D m_Rigidbody;

    private bool m_IsGamePlaying;
    private float m_Timer;
    private float m_TimeInterval = 1f; // Time interval between actions in seconds

    private int m_CurrentTimeIndex;
    private int m_MaxTimeIndex = 6;

    private bool isMovingRight;
    private bool isMovingLeft;

    public void OnGamePlayStart()
    {
        m_IsGamePlaying = true;
        m_Timer = 0;
        m_CurrentTimeIndex = 0;
    }

    private void Update()
    {
        if (!m_IsGamePlaying) return;
        
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_TimeInterval)
        {
            if (m_CurrentTimeIndex == m_MaxTimeIndex)
            {
                m_IsGamePlaying = false;
                isMovingLeft = false;
                isMovingRight = false;
            }
            m_Timer = 0;
            // Get the next action from the sequence
            ActionState[] actionSequence = m_ActionManager.GetActionsOfIndex(m_CurrentTimeIndex);
            for (int i = 0; i < actionSequence.Length; i++)
            {
                ProcessPlayerAction(actionSequence[i]);
            }

            m_CurrentTimeIndex++;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (isMovingLeft)
        {
            transform.Translate(Vector3.left * m_MoveSpeed * Time.deltaTime, Space.World);
        }
        
        if(isMovingRight)
        {
            transform.Translate(Vector3.right * m_MoveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void ResetPlayer()
    {

    }

    private void ProcessPlayerAction(ActionState actionState)
    {
        switch (actionState.type)
        {
            case ActionTypeEnum.Left:
                isMovingLeft = actionState.isActive;
                break;
            case ActionTypeEnum.Right:
                isMovingRight = actionState.isActive;
                break;
            case ActionTypeEnum.Jump:
                if(actionState.isActive) Jump();
                break;
            case ActionTypeEnum.Interact:
                if(actionState.isActive) Interact();
                break;
        }
    }

    private void Interact()
    {
        
    }

    private void Jump()
    {
        m_Rigidbody.linearVelocityY = m_JumpForce;
    }

}
