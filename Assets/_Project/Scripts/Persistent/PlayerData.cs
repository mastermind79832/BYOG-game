using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ekalaivan.Persistant
{
	[Serializable]
	public struct PlayerSaveData
	{
		public bool[] CompletedLevels;
	}

	public class PlayerData : MonoBehaviour
	{
		private const string SAVEDATA = "SaveData";

		private PlayerSaveData m_SaveData;
		public PlayerSaveData SaveData { get { return m_SaveData; } }

		internal void Initialize(int totalLevels)
		{
			string JsonSave = PlayerPrefs.GetString(SAVEDATA, string.Empty);

			if (string.IsNullOrEmpty(JsonSave))
			{
				CreateNewSave(totalLevels);
			}
			else
			{
				m_SaveData = JsonUtility.FromJson<PlayerSaveData>(JsonSave);
			}

			if(m_SaveData.CompletedLevels.Length != totalLevels)
            {
				AddNewLevels(totalLevels);
            }
		}

        private void AddNewLevels(int totalLevels)
		{
			int previousLength = m_SaveData.CompletedLevels.Length;
			m_SaveData.CompletedLevels = m_SaveData.CompletedLevels.Concat(new bool[totalLevels - m_SaveData.CompletedLevels.Length]).ToArray();
			
			for (int i = previousLength; i < totalLevels; i++)
			{
				m_SaveData.CompletedLevels[i] = false;
			}
        }

        private void CreateNewSave(int totalLevels)
		{

			m_SaveData = new PlayerSaveData();
			m_SaveData.CompletedLevels = new bool[totalLevels];

			for (int i = 0; i < m_SaveData.CompletedLevels.Length; i++)
			{
				m_SaveData.CompletedLevels[i] = false;
			}

			SaveDataToPlayerPrefs();
		}

		private void SaveDataToPlayerPrefs()
		{
			string JsonSave = JsonUtility.ToJson(m_SaveData, true);
			PlayerPrefs.SetString(SAVEDATA, JsonSave);
			PlayerPrefs.Save();

		}

		public void SaveNewLevelData(int LevelNumber, bool isCompleted)
		{
			m_SaveData.CompletedLevels[LevelNumber] = isCompleted;
			SaveDataToPlayerPrefs();
		}

	}
}