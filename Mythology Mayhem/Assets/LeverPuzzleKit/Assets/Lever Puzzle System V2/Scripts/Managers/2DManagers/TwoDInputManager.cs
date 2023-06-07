using UnityEngine;

namespace LeverSystemTwo
{
public class TwoDInputManager : MonoBehaviour
{
        [Header("Raycast Interact Input")]
        public KeyCode interactKey;

        public static TwoDInputManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
