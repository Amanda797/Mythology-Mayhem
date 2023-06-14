using UnityEngine;

namespace LeverSystem
{
    public class LeverRaycast : MonoBehaviour
    {
        [Header("Raycast Parameters")]
        [SerializeField] private int interactDistance = 5;
        private LeverItem examinableItem;
        private Camera _camera;

        [Header("Interaction Tag")]
        [SerializeField] private string interactionTag = "LeverObject";

        public static LeverRaycast instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            _camera = Camera.main;
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out hit, interactDistance))
            {
                var selectedItem = hit.collider.GetComponent<LeverItem>();
                if (selectedItem != null && selectedItem.CompareTag(interactionTag))
                {
                    examinableItem = selectedItem;
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }

            if (examinableItem != null)
            {
                if (Input.GetKey(LeverInputManager.instance.interactKey))
                {
                    examinableItem.ObjectInteract();
                }
            }
        }

        private void ClearExaminable()
        {
            if (examinableItem != null)
            {
                HighlightCrosshair(false);
                examinableItem = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            LeverUIManager.instance.HighlightCrosshair(on);
        }
    }
}
