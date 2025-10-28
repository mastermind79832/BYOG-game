using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ekalaivan.Persistant
{
    public class SceneManager : MonoBehaviour
    {

		[SerializeField] private int StartingSceneIndex;
		private int CurrentIndex;
		public int GetCurrentIndex() => CurrentIndex;	
		//[SerializeField] private GameObject LoadingScreen;

		internal void StartService()
		{
			MoveToNewScene(StartingSceneIndex);
		}

		private void MoveToNewScene(int index)
		{
			if (index > GetTotalScenes())
				index = StartingSceneIndex;

			UnityEngine.SceneManagement.SceneManager.LoadScene(index, LoadSceneMode.Additive);
			CurrentIndex = index;
		}

		public void SwitchScene(int index)
		{
			UnloadCurrentScene();
			MoveToNewScene(index);
		}

		private void UnloadCurrentScene()
		{
			UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(CurrentIndex);
		}

        internal int GetTotalScenes()
        {
			return UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        }

        internal void LoadNextScene()
        {
			SwitchScene(CurrentIndex + 1);
        }

        internal void ReloadCurrentScene()
        {
            SwitchScene(CurrentIndex);
        }
    }
}
