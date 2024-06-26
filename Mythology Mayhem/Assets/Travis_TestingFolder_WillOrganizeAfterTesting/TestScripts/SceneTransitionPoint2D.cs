using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPoint2D : SceneTransitionPoint
{
    GameManager gameManager;
    void Start()
    {
        // try to find the GameManager object
        if (GameManager.instance != null) gameManager = GameManager.instance;
        // else display a warning that it is missing
        else Debug.LogWarning("GameManager Missing or Inactive.");
    }
    void Update()
    {
        // if the player is not near the door, stop
        if (!canTransition) return;

        // if the E key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // if this is the last door in the level, update the game manager
            if (countAsLevelComplete) gameManager.gameData.UpdateLevelComplete(completedChapter, completedLevel);

            // transition to the next scene/level
            localGameManager.SceneTransition(sceneToTransition, spawnpointNameOverride);
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
    }
}
