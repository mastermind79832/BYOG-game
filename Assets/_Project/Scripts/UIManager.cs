using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerController m_Player;
    [SerializeField] private RectTransform m_PopupWindow;
    [SerializeField] private ActionManager m_ActionManager;

    [SerializeField] private float m_PoppedPosition;
    [SerializeField] private float m_HiddenPosition;

    private bool m_IsPopped = false;

    public void PopUpToggle()
    {
        if(m_IsPopped)
            HidePopup();
        else
            PopUp();
    }

    private void PopUp()
    {
        m_IsPopped = true;
        m_PopupWindow.anchoredPosition = new Vector2(m_PopupWindow.anchoredPosition.x, m_PoppedPosition);
    }

    private void HidePopup()
    {
        m_IsPopped = false;
        m_PopupWindow.anchoredPosition = new Vector2(m_PopupWindow.anchoredPosition.x, m_HiddenPosition);
    }

    public void OnPlayClicked()
    {
        m_Player.OnGamePlayStart();
        HidePopup();
    }
}
