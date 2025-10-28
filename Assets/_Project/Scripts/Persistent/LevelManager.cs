using System;
using UnityEngine;

namespace Ekalaivan.Persistant
{

	public class LevelManager : MonoBehaviour
	{
        public int TotalLevels { get; internal set; }
		public int LevelStartingIndex;

		public int CurrentLevel;

		// function that gets if the level is completed and updated the player save data
		public void UpdateLevelData(bool isCompleted)
		{
			CurrentLevel = PersistantServiceLocator.Instance.SceneManagerRef.GetCurrentIndex() - LevelStartingIndex; // get the current level index from scene manager and subtract the starting index to get the level number

			if(CurrentLevel < 0)
				return; // if the current level is less than 0, return as it means that the player is not in a level scene

			PersistantServiceLocator.Instance.PlayerDataRef.SaveNewLevelData(CurrentLevel, isCompleted);
		}

        public void SetTotalLevel()
        {
			int totalScenes = PersistantServiceLocator.Instance.SceneManagerRef.GetTotalScenes();
			TotalLevels = totalScenes - LevelStartingIndex;
        }

		internal void Initialize()
		{
			SetTotalLevel();
			CurrentLevel = -1;
		}

    }
}
