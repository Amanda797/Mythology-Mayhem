using System.Collections;
using TMPro;
using UnityEngine;

public class ScrollPopup : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;

    // Hover Vars
    public float activeDistance = 6f;
    public float hoverDistance;
    float hover;


    public GameplayUILink gameplayUI;
    public GameObject popup;
    public TMP_Text textMeshObject;
    public GameObject supriseObject;
    public bool isPopupActive = false;
    public bool surpriseSpawned = false;
    [TextArea(4, 9)]
    public string scrollText;
    bool canRead = false;

    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        if (FindObjectOfType<GameplayUILink>() != null)
        {
            gameplayUI = FindObjectOfType<GameplayUILink>();
            popup = gameplayUI.scrollPanel;
            textMeshObject = gameplayUI.scrollDisplayText;
        }
        else Debug.LogWarning("GameplayUILink Missing or Inactive.");

        if (GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        else Debug.LogWarning("AudioSource Missing or Inactive.");

        hover = hoverDistance;

        StartCoroutine(Hover());
    }
    void Update()
    {
        if (!canRead) return;
        if (Time.timeScale == 0) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            textMeshObject.text = scrollText;
            popup.SetActive(true);
            hover = 0;
            audioSource.Play();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;

            if (supriseObject != null && !surpriseSpawned)
            {
                supriseObject.SetActive(true);
                surpriseSpawned = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // if the object entering the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            // activate and display text on the popup ui
            gameManager.Popup("Press E to Read", true);
            canRead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if the object entering the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Read", false);
            canRead = false;
        }
    }

    // Ienumerator that makes the scroll hover up and down with lerping
    IEnumerator Hover()
    {
        while (true)
        {
            float t = 0f;
            while (t <= 0.9f)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * hover, t);
                yield return null;
            }
            t = 0f;
            while (t <= 0.9f)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.up * hover, t);
                yield return null;
            }
        }
    }
}
