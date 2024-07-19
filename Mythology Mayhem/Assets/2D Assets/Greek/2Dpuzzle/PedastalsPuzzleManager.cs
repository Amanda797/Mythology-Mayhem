using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedastalsPuzzleManager : MonoBehaviour
{
    GameManager gameManager;
    public  bool fish;
    public  bool apple;
    public  bool torch;
    public  bool air;

    public bool fishDone;
    public bool appleDone;
    public bool torchDone;
    public bool airDone;

    public SceneTransitionPoint2D door;
    public string nextSceneName = "GreekAthens_2D";
    public GameObject itemBow;
    public bool bowCollected;

    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }
    public void UpdateDoor()
    {
        if (door == null)
        {
            foreach (SceneTransitionPoint2D d in FindObjectsOfType<SceneTransitionPoint2D>())
            {
                if (d.gameObject.scene.name == gameManager.currentScene.ToString())
                {
                    if (d.sceneToTransition.ToString() == nextSceneName)
                    {
                        door = d;
                        door.enabled = false;
                        break;
                    }
                }
            }
        }

        if (fishDone && appleDone && torchDone && airDone)
        {
            door.enabled = true;
            
            if(itemBow.GetComponent<TwoDBow>().pickedUp == false)
            {
                itemBow.SetActive(true);
            }
        }
    }

}
