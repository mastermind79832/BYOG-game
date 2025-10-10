using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }


    [SerializeField] private PlayerController m_PlayerControllerRef;
    public PlayerController PlayerControllerRef { get { return m_PlayerControllerRef; } }
    [SerializeField] private UIManager m_UiManagerRef;
    public UIManager UIManagerRef { get { return m_UiManagerRef; } }

    private bool IsKeyCollected;
    

    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(this);
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
        IsKeyCollected = true;
        Debug.Log("Key Collected");
    }
}
