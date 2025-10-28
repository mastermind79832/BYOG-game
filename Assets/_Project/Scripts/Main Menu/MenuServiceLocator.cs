using Ekalaivan.Core;
using Ekalaivan.Persistant;
using UnityEngine;

namespace Ekalaivan.Menu
{
    public class MenuServiceLocator : MonoSingletonGeneric<MenuServiceLocator>
    {
		private PersistantServiceLocator m_PersistantServiceLocator;
		[SerializeField] private MenuUIManager m_MenuUIManager;
		[SerializeField] private AudioManager m_AudioManager;

		public MenuUIManager MenuUIManager {  get { return m_MenuUIManager; } }
		public AudioManager AudioManager {  get { return m_AudioManager; } }

		protected override void Awake()
		{
			base.Awake();
			m_PersistantServiceLocator = PersistantServiceLocator.Instance;

			InitializeServices();
		}

		private void Start()
		{
			StartServices();
		}
		private void InitializeServices()
		{
			MenuUIManager.InitializeService();
		}

		private void StartServices()
		{
			AudioManager.StartService();

			MenuUIManager.StartService(m_PersistantServiceLocator.PlayerDataRef.SaveData, m_PersistantServiceLocator.LevelManagerRef.TotalLevels);
		}

	}
}
