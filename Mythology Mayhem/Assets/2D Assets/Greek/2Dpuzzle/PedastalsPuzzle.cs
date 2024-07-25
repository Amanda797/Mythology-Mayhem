using UnityEngine;

public class PedastalsPuzzle : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    public PedastalsPuzzleManager puzzleManager;
    public ElementalPuzzleItem.Item item;


    public SpriteRenderer elementalIcon;
    public bool hasItem = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");

        if (FindObjectOfType<PedastalsPuzzleManager>() != null) puzzleManager = FindObjectOfType<PedastalsPuzzleManager>();
        else Debug.LogWarning("PedastalsPuzzleManager Missing.");

        switch (item)
        {
            case ElementalPuzzleItem.Item.Apple:
                if (puzzleManager.appleDone) elementalIcon.color = Color.white;
                else elementalIcon.color = Color.black;
                break;
            case ElementalPuzzleItem.Item.Torch:
                if (puzzleManager.torchDone) elementalIcon.color = Color.white;
                else elementalIcon.color = Color.black;
                break;
            case ElementalPuzzleItem.Item.Fish:
                if (puzzleManager.fishDone) elementalIcon.color = Color.white;
                else elementalIcon.color = Color.black;
                break;
            case ElementalPuzzleItem.Item.Air:
                if (puzzleManager.airDone) elementalIcon.color = Color.white;
                else elementalIcon.color = Color.black;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (!hasItem) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (item)
            {
                case ElementalPuzzleItem.Item.Apple:
                    if (!puzzleManager.appleDone)
                    {
                        elementalIcon.color = Color.white;
                        puzzleManager.appleDone = true;
                        audioSource.Play();
                    }
                    break;
                case ElementalPuzzleItem.Item.Torch:
                    if (!puzzleManager.torchDone)
                    {
                        elementalIcon.color = Color.white;
                        puzzleManager.torchDone = true;
                        audioSource.Play();
                    }
                    break;
                case ElementalPuzzleItem.Item.Fish:
                    if (!puzzleManager.fishDone)
                    {
                        elementalIcon.color = Color.white;
                        puzzleManager.fishDone = true;
                        audioSource.Play();
                    }
                    break;
                case ElementalPuzzleItem.Item.Air:
                    if (!puzzleManager.airDone)
                    {
                        elementalIcon.color = Color.white;
                        puzzleManager.airDone = true;
                        audioSource.Play();
                    }
                    break;
                default:
                    break;
            }
            puzzleManager.UpdateDoor();
        }        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (item)
            {
                case ElementalPuzzleItem.Item.Apple:
                    if (puzzleManager.apple)
                    {
                        gameManager.Popup("Press E to Place Apple", true);
                        hasItem = true;
                    }
                    else gameManager.Popup("Item Missing\nReturn to the Labyrinth", true);
                    break;
                case ElementalPuzzleItem.Item.Torch:
                    if (puzzleManager.torch)
                    {
                        gameManager.Popup("Press E to Place Torch", true);
                        hasItem = true;
                    }
                    else gameManager.Popup("Item Missing\nReturn to the Labyrinth", true);
                    break;
                case ElementalPuzzleItem.Item.Fish:
                    if (puzzleManager.fish)
                    {
                        gameManager.Popup("Press E to Place Fish", true);
                        hasItem = true;
                    }
                    else gameManager.Popup("Item Missing\nReturn to the Labyrinth", true);
                    break;
                case ElementalPuzzleItem.Item.Air:
                    if (puzzleManager.air)
                    {
                        gameManager.Popup("Press E to Place Air", true);
                        hasItem = true;
                    }
                    else gameManager.Popup("Item Missing\nReturn to the Labyrinth", true);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("", false);

            hasItem = false;
        }
    }
}
