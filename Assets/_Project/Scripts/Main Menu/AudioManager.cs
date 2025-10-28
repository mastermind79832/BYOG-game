using UnityEngine;

namespace Ekalaivan.Menu
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_UiSoundSource;

		[SerializeField] private AudioClip m_ButtonClickClip;

		internal void StartService()
		{
			m_UiSoundSource.loop = false;
		}

		public void OnButtonClick()
		{
			m_UiSoundSource.clip = m_ButtonClickClip;
			m_UiSoundSource.Play();
		}
	}
}
