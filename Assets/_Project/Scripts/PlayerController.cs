using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ActionManager m_ActionManager;
    [SerializeField] private float m_MoveSpeed = 5f;
    [SerializeField] private float m_JumpForce = 10f;
    [SerializeField] private Rigidbody2D m_Rigidbody;


    [Header("Interactions")]
    [SerializeField] private float m_InteractionRange = 2f;
    [SerializeField] private LayerMask m_InteractiveLayerMask; 
    private bool m_IsGamePlaying;
    private float m_Timer;
    private float m_TimeInterval = 1f; // Time interval between actions in seconds

    private int m_CurrentTimeIndex;
    private int m_MaxTimeIndex = 6;

    private bool m_IsMovingRight;
    private bool m_IsMovingLeft;

    private Vector2 m_StartLocation;

    void Start()
    {
        m_StartLocation = transform.position;
    }

    public void OnGamePlayStart()
    {
        m_IsGamePlaying = true;
        m_Timer = m_TimeInterval / 2;
        m_CurrentTimeIndex = 0;
    }
    private void Update()
    {
        if (!m_IsGamePlaying) return;

        UpdateTimer();
        if (m_Timer >= m_TimeInterval)
        {
            if (m_CurrentTimeIndex == m_MaxTimeIndex)
            {
                EndTurn();
                return;
            }
            m_Timer = 0;
            GameManager.Instance.UIManagerRef.OnTimeUpdated(0f);
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

    private void UpdateTimer()
    {
        m_Timer += Time.deltaTime;
        GameManager.Instance.UIManagerRef.OnTimeUpdated(m_CurrentTimeIndex + m_Timer);
    }

    private void EndTurn()
    {
        m_IsGamePlaying = false;
        m_IsMovingLeft = false;
        m_IsMovingRight = false;
        GameManager.Instance.UIManagerRef.OnTimeUpdated(0f);
        StartCoroutine(WaitForEndStuff());
    }

    private IEnumerator WaitForEndStuff()
    {
        yield return new WaitForSeconds(0.1f);
        ResetPlayer();
        GameManager.Instance.PlayEnded();
    }

    private void HandleMovement()
    {
        if (m_IsMovingLeft)
        {
            transform.Translate(Vector3.left * m_MoveSpeed * Time.deltaTime, Space.World);
        }
        
        if(m_IsMovingRight)
        {
            transform.Translate(Vector3.right * m_MoveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void ResetPlayer()
    {
        transform.position = m_StartLocation;
    }

    private void ProcessPlayerAction(ActionState actionState)
    {
        switch (actionState.type)
        {
            case ActionTypeEnum.Left:
                m_IsMovingLeft = actionState.isActive;
                break;
            case ActionTypeEnum.Right:
                m_IsMovingRight = actionState.isActive;
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, m_InteractionRange, transform.up, 0f, m_InteractiveLayerMask);
        if (hits.Length == 0)
        {
            Debug.Log("No interactable object found");
            return; // No interactable object found
        }
        hits[0].transform.GetComponent<IInteractable>().Interact();
    }

    private void Jump()
    {
        m_Rigidbody.linearVelocityY = m_JumpForce;
    }

}
