using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPoint2D : SceneTransitionPoint
{
    GameManager gameManager;
    public TwoDLever lever;
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");
    }
    void Update()
    {
        if (!canTransition) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (countAsLevelComplete) gameManager.gameData.UpdateLevelComplete(completedChapter, completedLevel);

            localGameManager.SceneTransition(sceneToTransition, spawnpointNameOverride);
            gameManager.Popup("Press E to Enter", false);
            canTransition = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isActive)
            {
                gameManager.Popup("Press E to Enter", true);
                canTransition = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Enter", false);
            canTransition = false;
        }
        if (collision.gameObject.tag == "PushBlock")
        {
            lever.isBlocked = false;
        }
    }
}
