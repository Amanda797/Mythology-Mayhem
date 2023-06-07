using UnityEngine;

namespace LeverSystem
{
    public class LeverInputManager : MonoBehaviour
    {
        [Header("Raycast Interact Input")]
        public KeyCode interactKey;

        public static LeverInputManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
