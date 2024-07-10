using UnityEngine;

public class TwoDMirror : MonoBehaviour
{
    GameManager gameManager;
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
        pickedUp = true;
        gameManager.gameData.saveData.playerData.collectedMirror = true;
        gameManager.gameData.saveData.Save();
        gameObject.SetActive(false);
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
