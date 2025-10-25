using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GuideSystem m_GuideSystem; // Reference to the GuideSystem script
    [SerializeField] private ActionManager m_ActionManager;
    [SerializeField] private float m_MoveSpeed = 5f;
    [SerializeField] private float m_JumpForce = 10f;
    [SerializeField] private float m_GroundCheckDistance;
    [SerializeField] private LayerMask m_GroundLayer;
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

    [SerializeField] private Animator m_Anim;
    private bool m_IsJumping;
    private bool m_IsGrounded;

    private float m_GroundCheckDelayTimer;

    private bool m_IsGameOver;
  
    void Start()
    {
        m_StartLocation = transform.position;
        m_Anim.SetBool("IsRunning", false); // Set the initial animation state
        m_Anim.SetFloat("Falling", 0);
        m_Anim.ResetTrigger("Jump");
        m_Anim.SetBool("IsGrounded", true);
        m_IsGameOver = false;
    }

    public void OnGamePlayStart()
    {
        m_IsGamePlaying = true;
        m_Timer = m_TimeInterval / 2;
        m_CurrentTimeIndex = 0;
        transform.localScale = Vector3.one; // Reset scale to normal size
    }
    private void Update()
    {
        
        if(m_IsGameOver) return;

        HandleJumpAnimations();

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

    private void HandleJumpAnimations()
    {
        if (m_IsJumping)
        {
            m_Anim.SetFloat("Falling", m_Rigidbody.linearVelocityY > 0 ? 0: 1);
            GroundCheck();
        }
    }

    private void GroundCheck()
    {
        // delay for ground check
        if (m_Timer >= m_GroundCheckDelayTimer + 0.1f)
        {
            m_GroundCheckDelayTimer = 0;
        }
        else
        {
            return;
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, m_GroundCheckDistance, m_GroundLayer))
        {
            m_IsJumping = false;
            m_Anim.SetBool("IsGrounded", true);
        }
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
        HandleMovement();
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

        m_MoveSpeed = m_GuideSystem.Offset;
        if (m_IsMovingLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.Translate(Vector3.left * m_MoveSpeed * Time.deltaTime, Space.World);
            m_Anim.SetBool("IsRunning", true);
            GameManager.Instance.AudioManagerRef.PlayPlayerWalk(true);
        }
        else if (m_IsMovingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.Translate(Vector3.right * m_MoveSpeed * Time.deltaTime, Space.World);
            m_Anim.SetBool("IsRunning", true);
            GameManager.Instance.AudioManagerRef.PlayPlayerWalk(true);
        }
        else
        {
            m_Anim.SetBool("IsRunning", false);
            GameManager.Instance.AudioManagerRef.PlayPlayerWalk(false);
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
                if (actionState.isActive) Jump();
                break;
            case ActionTypeEnum.Interact:
                if (actionState.isActive) Interact();
                break;
        }

        GameManager.Instance.AudioManagerRef.PlayBeatTune(actionState.BeatIndex, actionState.Pitch);
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
        m_IsJumping = true;
        m_Anim.SetTrigger("Jump");
        m_Anim.SetBool("IsGrounded", false);
        m_GroundCheckDelayTimer = m_Timer;
        GameManager.Instance.AudioManagerRef.PlayPlayerJump();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            if (GameManager.Instance.IsKeyCollected)
            {
                GameManager.Instance.GameWin();
                m_IsGameOver = true;
            }
        }

        if (other.CompareTag("Spike"))
        {
            GameManager.Instance.GameOver();
            m_IsGameOver = true;
        }

    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue; // Set the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, m_InteractionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2.down * m_GroundCheckDistance));
    }
}
