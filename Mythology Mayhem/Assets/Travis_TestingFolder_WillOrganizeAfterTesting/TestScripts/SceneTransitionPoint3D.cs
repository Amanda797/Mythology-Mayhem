using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPoint3D : SceneTransitionPoint
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
        //CheckInput();

        // this should be placed in the script that completes the condition i.e. TwoDLever.cs
        // isActive = CheckConditionsMeet();

        // if the door is not active, stop
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isActive)
            {
                gameManager.Popup("Press E to Enter", true);
                canTransition = true;
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Enter", false);
            canTransition = false;
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (GameManager.instance != null)
    //        {
    //            GameManager.instance.Popup("Press E to Enter");
    //        }
    //        PlayerAttach player = other.gameObject.GetComponent<PlayerAttach>();
    //        if (player != null)
    //        {
    //            if (keyPress)
    //            {
    //                if (countAsLevelComplete)
    //                {
    //                    if (GameManager.instance != null)
    //                    {
    //                        GameManager.instance.gameData.UpdateLevelComplete(completedChapter, completedLevel);
    //                    }
    //                }

    //                localGameManager.SceneTransition(sceneToTransition, spawnpointNameOverride);

    //                keyPress = false;
    //            }

    //        }
    //    }
    //}
}
