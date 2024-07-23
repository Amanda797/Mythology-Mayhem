using System.Collections;
using TMPro;
using UnityEngine;

public class ScrollPopup : MonoBehaviour
{
    GameManager gameManager;
    public KeyCode key;
    public float activeDistance = 6f;
    public float hoverDistance;
    float hover;
    public GameplayUILink gameplayUI;
    public GameObject popup;
    public TMP_Text textMeshObject;
    public GameObject pressText;
    public GameObject supriseObject;
    public bool isPopupActive = false;
    public bool surpriseSpawned = false;
    [TextArea(4, 9)]
    public string scrollText;


    bool activeStatus = false;
    GameObject player = null;

    void Start()
    {
        // try to find the GameManager object
        if (GameManager.instance != null)
        {
            // if found set variable(s)
            gameManager = GameManager.instance;
        }
        // else display a warning that it is missing
        else Debug.LogWarning("GameManager Missing or Inactive.");

        // try to find the GameplayUILink object
        if (FindObjectOfType<GameplayUILink>() != null)
        {
            // if found set variable(s)
            gameplayUI = FindObjectOfType<GameplayUILink>();
            popup = gameplayUI.scrollPanel;
            textMeshObject = gameplayUI.scrollDisplayText;
            pressText = gameplayUI.pressEText;
        }
        // else display a warning that it is missing
        else Debug.LogWarning("GameplayUILink Missing or Inactive.");

        hover = hoverDistance;

        StartCoroutine(Hover());
    }
    void Update()
    {
        if (!activeStatus) return;

        if (Input.GetKeyDown(key))
        {
            if (isPopupActive)
            {
                HidePopup();
                isPopupActive = false;
            }
            else
            {
                ShowPopup();
                //StopCoroutine(Hover());
                isPopupActive = true;
                if (supriseObject != null && !surpriseSpawned)
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
            activeStatus = true;
            player = other.gameObject;
        }
        //if (!other.gameObject.CompareTag("Player")) return;

        //if (GameManager.instance != null) GameManager.instance.Popup("Press E to Read");

        //if (Input.GetKeyDown(key))
        //{
        //    if (isPopupActive)
        //    {
        //        HidePopup();
        //        isPopupActive = false;
        //    }
        //    else
        //    {
        //        ShowPopup();
        //        //StopCoroutine(Hover());
        //        isPopupActive = true;
        //        if (supriseObject != null && !surpriseSpawned)
        //            supriseObject.SetActive(true);
        //        surpriseSpawned = true;
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        // if the object entering the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            // deactivate the popup ui
            gameManager.Popup("Press E to Read", false);
            activeStatus = false;
            player = null;
        }
    }
    public void ShowPopup()
    {
        textMeshObject.text = scrollText;
        popup.SetActive(true);
        hover = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    public void HidePopup()
    {
        popup.SetActive(false);
        hover = hoverDistance;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
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
