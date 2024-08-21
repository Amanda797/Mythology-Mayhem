using UnityEngine;

public class TwoDMirror : MonoBehaviour
{
    GameManager gameManager;
    [TextArea(7, 10)]
    [SerializeField] string text;
    [SerializeField] AudioClip clip;
    public bool pickUpAllowed = false;
    public bool pickedUp = false;

    public bool isEquipped;
    public bool isInRangeOfEnemy;

    public float slowingValue;

    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }
    void Update()
    {
        if (!pickUpAllowed) return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pick up", true);

            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pick up", false);

            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        AudioSource source = gameManager.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        pickedUp = true;
        gameManager.gameData.collectedMirror = true;
        gameManager.SaveGame();
        gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        gameManager.pauseMenuManager.scrollDisplayText.text = text;
        gameManager.pauseMenuManager.scrollDisplayText.rectTransform.anchoredPosition = Vector2.zero;
        gameManager.pauseMenuManager.TogglePause(true);
        gameManager.Popup("Press E to Read", false);
    }

    private void SetMinotaurSpeed()
    {
        MouseAI[] enemies = FindObjectsOfType<MouseAI>();

        foreach(MouseAI enemy in enemies)
        {
            enemy.SetMovementSpeed(slowingValue);
        }
    }

}
