using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_PopupWindow;
    [SerializeField] private ActionManager m_ActionManager;

    [SerializeField] private float m_PoppedPosition;
    [SerializeField] private float m_HiddenPosition;

    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_PopupButton;
    [SerializeField] private Transform popImage;
    [SerializeField] private float m_PopupDuration;
    [SerializeField] private Ease m_PopEase;
    [SerializeField] private Slider m_TimerSlider;
    private bool m_IsPopped = false;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NextLevelButton;

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
        m_PopupWindow.DOAnchorPosY(m_PoppedPosition, m_PopupDuration).SetEase(m_PopEase); // Adjust the duration and easing as needed
        //m_PopupWindow.anchoredPosition = new Vector2(m_PopupWindow.anchoredPosition.x, m_PoppedPosition);
        //popImage.localScale = -Vector3.one; 
        popImage.DOScaleY(-1, m_PopupDuration);
    }

    public void HidePopup()
    {
        m_IsPopped = false;
        m_PopupWindow.DOAnchorPosY(m_HiddenPosition, m_PopupDuration).SetEase(m_PopEase);
        // m_PopupWindow.anchoredPosition = new Vector2(m_PopupWindow.anchoredPosition.x, m_HiddenPosition);
        //popImage.localScale = Vector3.one;
        popImage.DOScaleY(1, m_PopupDuration);
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
        //m_PlayButton.interactable = false;
    }

    public void UnlockUI()
    {
        m_PopupButton.interactable = true;
       // m_PlayButton.interactable = true;
    }

    public void ResetAll()
    {
        GameManager.Instance.BackToMenu();
    }

    public void OnTimeUpdated(float time)
    {
        m_TimerSlider.value = time;
    }

    public void GameOver(bool isWin)
    {
        if (isWin)
        {
            NextLevelButton.SetActive(true);
        }
        else
        {
            NextLevelButton.SetActive(false);
        }
        GameOverPanel.SetActive(true);
    }
}
