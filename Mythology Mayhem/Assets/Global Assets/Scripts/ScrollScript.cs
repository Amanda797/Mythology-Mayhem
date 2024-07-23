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
    GameplayUILink gameplayUI;
    GameObject scrollPanel;
    TMP_Text scrollDisplayText;
    AudioSource audioSource;
    GameManager gameManager;

    bool activeStatus = false;
    bool scrollOpen = false;
    bool scrollClosed = true;

    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        if (FindObjectOfType<GameplayUILink>() != null)
        {
            gameplayUI = FindObjectOfType<GameplayUILink>();
            scrollPanel = gameplayUI.scrollPanel;
            scrollDisplayText = gameplayUI.scrollDisplayText;
        }
        else Debug.LogWarning("GameplayUILink Missing or Inactive.");

        if (GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        else Debug.LogWarning("AudioSource Missing or Inactive.");
    }

    void Update()
    {
        if (!activeStatus) return;

        if (Input.GetKeyUp(KeyCode.E))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            audioSource.Play();
            scrollDisplayText.text = text;
            scrollPanel.SetActive(true);
            gameManager.Popup("Press E to Read", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Read", true);
            activeStatus = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Read", false);
            activeStatus = false;
        }
    }
}
