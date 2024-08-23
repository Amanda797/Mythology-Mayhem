using System;
using System.Linq;
using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    public GameObject itemToDisplay;
    public bool[] currentLeverAnswer = new bool[10];
    public bool[] correctLeverAnswer = new bool[] { true, false, true, false, true, false, true, false, true, false };
    [TextArea(7, 10)]
    [SerializeField] string owlText;
    public enum Puzzle
    {
        mirror,
        owl
    }

    public Puzzle puzzle = Puzzle.mirror;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");
    }

    public void CheckPuzzel(int pos, bool isOn)
    {
        currentLeverAnswer[pos] = isOn;

        if (currentLeverAnswer.SequenceEqual(correctLeverAnswer))
        {
            audioSource.Play();
            switch (puzzle)
            {
                case Puzzle.mirror:
                    CollectMirror();
                    break;

                case Puzzle.owl:
                    CollectOwl();
                    break;
            }
        }
    }
    public void CollectMirror()
    {
        if (!gameManager.gameData.collectedMirror)
        {
            itemToDisplay.SetActive(true);
        }
    }
    public void CollectOwl()
    {
        if (!gameManager.gameData.collectedOwl)
        {
            gameManager.gameData.collectedOwl = true;
            gameManager.SaveGame();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            gameManager.pauseMenuManager.scrollDisplayText.text = owlText;
            gameManager.pauseMenuManager.scrollDisplayText.rectTransform.anchoredPosition = Vector2.zero;
            gameManager.pauseMenuManager.TogglePause(true);
            gameManager.Popup("Press E to Read", false);
        }
    }
}
