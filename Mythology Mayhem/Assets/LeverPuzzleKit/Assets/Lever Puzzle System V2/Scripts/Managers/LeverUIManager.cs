using UnityEngine;
using UnityEngine.UI;

namespace LeverSystem
{
    public class LeverUIManager : MonoBehaviour
    {
        [Header("Crosshair UI")]
        [SerializeField] private Image crosshair = null;

        public static LeverUIManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void HighlightCrosshair(bool on)
        {
            if (on)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
            }
        }
    }
}
