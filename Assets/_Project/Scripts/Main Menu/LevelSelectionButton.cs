using Ekalaivan.Persistant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ekalaivan.Menu
{
    public class LevelSelectionButton : MonoBehaviour
    {
        [SerializeField] private Button m_LevelSelectionButton;
        [SerializeField] private Image m_LevelCompletedImage;
        [SerializeField] private TextMeshProUGUI m_LevelNumberText;
        private int m_LevelSceneIndex;

        void Awake()
        {
            m_LevelSelectionButton.onClick.AddListener(OnClick);
            m_LevelSelectionButton.interactable = true;
        }

        public void SetUpLevelData(int levelNumber ,int levelSceneIndex, bool isCompleted)
        {
            m_LevelNumberText.text = levelNumber.ToString();
            m_LevelSceneIndex = levelSceneIndex;
            m_LevelCompletedImage.gameObject.SetActive(isCompleted);

        }

        public void OnClick()
        {
            PersistantServiceLocator.Instance.SceneManagerRef.SwitchScene(m_LevelSceneIndex);
            MenuServiceLocator.Instance.AudioManager.OnButtonClick();
        }
    }
}
