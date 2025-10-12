using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_PopupWindow;
    [SerializeField] private ActionManager m_ActionManager;

    [SerializeField] private float m_PoppedPosition;
    [SerializeField] private float m_HiddenPosition;

    [SerializeField] private Button m_PopupButton;

    [SerializeField] private Slider m_TimerSlider;
    private bool m_IsPopped = false;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NextLevelButton;
    [SerializeField] private TextMeshProUGUI GameOverText;

    void Start()
    {
        OnClearClicked();
        HidePopup();
        GameOverPanel.SetActive(false);
    }
    public void PopUpToggle()
    {
        if (m_IsPopped)
            HidePopup();
        else
            PopUp();
    }

    public void PopUp()
    {
        m_IsPopped = true;
        m_PopupWindow.anchoredPosition = new Vector2(m_PopupWindow.anchoredPosition.x, m_PoppedPosition);
    }

    public void HidePopup()
    {
        m_IsPopped = false;
        m_PopupWindow.anchoredPosition = new Vector2(m_PopupWindow.anchoredPosition.x, m_HiddenPosition);
    }

    public void OnPlayClicked()
    {
        GameManager.Instance.OnPlayClicked();
        HidePopup();
        LockUI();
    }

    public void OnClearClicked()
    {
        List<AudioClip> clip = new List<AudioClip>(GameManager.Instance.AudioManagerRef.BeatboxTunes);
        m_ActionManager.OnActionReset(clip);
    }

    private void LockUI()
    {
        m_PopupButton.interactable = false;
    }

    public void UnlockUI()
    {
        m_PopupButton.interactable = true;
    }

    public void ResetAll()
    {
        GameManager.Instance.ResetAll();
        OnClearClicked();
    }

    public void OnTimeUpdated(float time)
    {
        m_TimerSlider.value = time;
    }

    public void GameOver(bool isWin)
    {
        if (isWin)
        {
            GameOverText.text = "You Win!";
            NextLevelButton.SetActive(true);
        }
        else
        {
            GameOverText.text = "Game Over!";
            NextLevelButton.SetActive(false);
        }
        GameOverPanel.SetActive(true);
    }
}
