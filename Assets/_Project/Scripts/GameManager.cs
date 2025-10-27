using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    [SerializeField] private PlayerController m_PlayerControllerRef;
    public PlayerController PlayerControllerRef { get { return m_PlayerControllerRef; } }
    [SerializeField] private UIManager m_UiManagerRef;
    public UIManager UIManagerRef { get { return m_UiManagerRef; } }

    private bool m_IsKeyCollected;
    public bool IsKeyCollected { get { return m_IsKeyCollected; } }

    [SerializeField] private AudioManager m_AudioManagerRef;
    public AudioManager AudioManagerRef { get { return m_AudioManagerRef; } }

    [SerializeField] private DoorHandler m_DoorHandlerRef;


    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        UIManagerRef.gameObject.SetActive(true);
    }

    public void OnPlayClicked()
    {
        PlayerControllerRef.OnGamePlayStart();
    }

    public void PlayEnded()
    {
        UIManagerRef.UnlockUI();
        m_UiManagerRef.PopUp();
    }

    public void ResetAll()
    {
        m_PlayerControllerRef.ResetPlayer();
    }

    internal void KeyCollected()
    {
        m_IsKeyCollected = true;
        m_DoorHandlerRef.OpenDoor();
        Debug.Log("Key Collected");
    }

    internal void GameWin()
    {
        m_UiManagerRef.GameOver(true);
    }

    internal void GameOver()
    {
        m_UiManagerRef.GameOver(false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
