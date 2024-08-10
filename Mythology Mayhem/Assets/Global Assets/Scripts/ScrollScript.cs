using System.Collections;
using UnityEngine;
using TMPro;

public class ScrollScript : MonoBehaviour
{
    // --------------------------
    // ***SECTIONS***
    // - PROPERTIES
    // - METHODS
    // - TESTS
    // --------------------------

    // --------------------------
    // ***PROPERTIES***
    // --------------------------

    [TextArea(7, 10)]
    [SerializeField] string text;
    AudioSource audioSource;
    GameManager gameManager;

    bool canRead = false;
    bool activeStatus = false;

    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        if (GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        else Debug.LogWarning("AudioSource Missing or Inactive.");
    }

    void Update()
    {
        if (!canRead) return;
        if (Time.timeScale == 0) return;

        if (Input.GetKeyUp(KeyCode.E))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            audioSource.Play();
            gameManager.pauseMenuManager.scrollDisplayText.text = text;
            gameManager.pauseMenuManager.scrollDisplayText.rectTransform.anchoredPosition = Vector2.zero;
            gameManager.pauseMenuManager.TogglePause(true);
            gameManager.Popup("Press E to Read", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Read", true);
            canRead = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Read", false);
            canRead = false;
        }
    }
}
