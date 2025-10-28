using UnityEngine;

namespace Ekalaivan.Core
{
    public abstract class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

		protected virtual void Awake()
		{
			CreateSingleton();
		}

		private void CreateSingleton()
		{
			if (instance == null) instance = (T)this;
			else Destroy(this.gameObject);
		}
	}
}
