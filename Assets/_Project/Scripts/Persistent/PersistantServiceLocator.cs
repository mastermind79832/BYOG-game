using Ekalaivan.Core;
using UnityEngine;

namespace Ekalaivan.Persistant
{
    public class PersistantServiceLocator : MonoSingletonGeneric<PersistantServiceLocator>
    {

		[SerializeField] private SceneManager m_SceneManager;
		[SerializeField] private PlayerData m_PlayerData;
		[SerializeField] private LevelManager m_LevelManager;

		public SceneManager SceneManagerRef {  get { return m_SceneManager; } }
		public PlayerData PlayerDataRef {  get { return m_PlayerData; } }
		public LevelManager LevelManagerRef {  get { return m_LevelManager; } }

	// Unity Functions
		protected override void Awake() 
		{
			base.Awake();
			InitializeServices();
		}

		void Start()
        {
            StartServices();
        }

		void Update()
        {
			UpdateServices();
        }
	// ---------

        ///Create All services
		private void InitializeServices()
		{
			LevelManagerRef.Initialize();
			PlayerDataRef.Initialize(LevelManagerRef.TotalLevels);
		}

		// Setup All services
		private void StartServices()
		{
			SceneManagerRef.StartService();
		}

		private void UpdateServices()
		{
			
		}
	}
}
