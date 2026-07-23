using UnityEngine;

namespace Tetris
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindFirstObjectByType(typeof(T));
                    if (instance == null)
                    {
                        GameObject gameObj = new GameObject();
                        gameObj.name = typeof(T).Name;
                        instance = gameObj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
    
        protected virtual void Awake()
        {
            RemoveDuplicates();
        }
    
        private void RemoveDuplicates()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
    
}

