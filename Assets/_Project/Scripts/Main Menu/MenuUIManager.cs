using System;
using System.Collections;
using DG.Tweening;
using Ekalaivan.Persistant;
using UnityEngine;
using UnityEngine.UI;

namespace Ekalaivan.Menu
{
    public class MenuUIManager : MonoBehaviour
	{
		[Header("Title")]
		[SerializeField] private Transform m_Title;
		[SerializeField] private Vector2 m_TitleKeyPositionsY;
		[SerializeField] private Transform m_CreditTitle;
		[SerializeField] private Vector2 m_CreditTitleKeyPositionsY;

		[Header("Buttons")]
		[SerializeField] private Button m_PlayButton;
		[SerializeField] private Button m_ExitButton;
		[SerializeField] private Button m_CreditButton;
		[SerializeField] private Button m_BackButton;
		[SerializeField] private Vector2 m_BackKeyPositionsY;
		[SerializeField] private LevelSelectionButton m_LevelSelectionButtonPrefab;
		[SerializeField] private Transform m_LevelSelectionParent;

		[Header("Panels")]
		[SerializeField] private Transform m_LevelSelectionPanel;
		[SerializeField] private Vector2 m_LevelPositionsY;
		[SerializeField] private Transform m_CreditPanel;
		[SerializeField] private Vector2 m_CreditKeyPositionsY;
		[SerializeField] private Transform m_MainPanel;
		[SerializeField] private Vector2 m_MainKeyPositionsY;
		[SerializeField] private float m_MainDuration;
		[SerializeField] private Ease m_MainEase;

		[Header("Other elements")]
		[SerializeField] private Transform m_RightSideThings;
		[SerializeField] private Transform m_LeftSideThings;
		[SerializeField] private float m_ThingsAppearPositionX;
		[SerializeField] private float m_thingsDuration;
		[SerializeField] private Ease m_ThingsEase;

		private WaitForSeconds m_WaitForMainDuration;

		internal void InitializeService()
		{
			m_WaitForMainDuration = new WaitForSeconds(m_MainDuration);

			m_PlayButton.onClick.AddListener(OnPlayClicked);
			m_CreditButton.onClick.AddListener(OnCreditClicked);
			m_BackButton.onClick.AddListener(OnBackClicked);
			m_ExitButton.onClick.AddListener(OnQuitClicked);
		}
		internal void StartService(PlayerSaveData saveData, int totalLevels)
		{
			SetUpLevelSelectionUI(saveData.CompletedLevels, totalLevels);

			m_LeftSideThings.DOMoveX(-m_ThingsAppearPositionX, m_thingsDuration).SetEase(m_ThingsEase);
			m_RightSideThings.DOMoveX(m_ThingsAppearPositionX, m_thingsDuration).SetEase(m_ThingsEase);
			OnBackClicked();
		}
        private void SetUpLevelSelectionUI(bool[] completedLevels, int totalLevels)
        {
			LevelSelectionButton newButton;
			
			for(int i = 0; i < completedLevels.Length; i++)
            {
                newButton = CreateLevelSelectionButton();
                newButton.SetUpLevelData(i+1 ,i + PersistantServiceLocator.Instance.LevelManagerRef.LevelStartingIndex, completedLevels[i]);
            }
        }

		private LevelSelectionButton CreateLevelSelectionButton()
		{
			return Instantiate(m_LevelSelectionButtonPrefab, m_LevelSelectionParent);
		}



		private void OnPlayClicked()
		{
			StartCoroutine(MainToLevel());
		}
        private void OnCreditClicked()
		{
			StartCoroutine(ShowCredits());
		}

		private void OnQuitClicked()
		{
			Application.Quit();
		}
		private void OnBackClicked()
		{
			AnimateY(m_CreditPanel, m_CreditKeyPositionsY.x);
			AnimateY(m_BackButton.transform, m_BackKeyPositionsY.x);
			AnimateY(m_CreditTitle, m_CreditTitleKeyPositionsY.x);
			AnimateY(m_LevelSelectionPanel, m_LevelPositionsY.x);
			AnimateY(m_BackButton.transform, m_BackKeyPositionsY.x);

			StartCoroutine(ActivateMainPanel());
		}

		private IEnumerator ActivateMainPanel()
		{

			yield return m_WaitForMainDuration;

			AnimateY(m_MainPanel, m_MainKeyPositionsY.y);
			AnimateY(m_Title, m_TitleKeyPositionsY.y);

		}
        private IEnumerator MainToLevel()
        {
			AnimateY(m_MainPanel, m_MainKeyPositionsY.x);
			yield return m_WaitForMainDuration;
			ActivateLevelPanel();
        }
        private void ActivateLevelPanel()
        {
			AnimateY(m_LevelSelectionPanel, m_LevelPositionsY.y);
			AnimateY(m_BackButton.transform, m_BackKeyPositionsY.y);
        }
        private IEnumerator ShowCredits()
		{
			AnimateY(m_MainPanel, m_MainKeyPositionsY.x);
			AnimateY(m_Title, m_TitleKeyPositionsY.x);
			
			yield return m_WaitForMainDuration;

			AnimateY(m_CreditPanel, m_CreditKeyPositionsY.y);
			AnimateY(m_BackButton.transform, m_BackKeyPositionsY.y);
			AnimateY(m_CreditTitle, m_CreditTitleKeyPositionsY.y);

        }

		
		// helper
        private void AnimateY(Transform item, float Position)
        {
            item.DOLocalMoveY(Position, m_MainDuration).SetEase(m_MainEase);
        }
    }
}
