using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] public string sceneName;
    public float distance;
    //public SaveScene saveScene;
    [SerializeField] Transform player;
    itemSlot itemSlot;
    //SaveScene saveScene;

    //void Start()
    //{
    //    itemSlot = FindObjectOfType<itemSlot>();
    //    //saveScene = GameObject.FindGameObjectWithTag("SaveScene").GetComponent<SaveScene>();
    //}

    //void Update()
    //{
    //    if(player == null) {
    //        player = GameObject.FindGameObjectWithTag("Player").transform;
    //    }

    //    //print(Vector3.Distance(player.position, transform.position) < distance);

    //    if(Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.position, transform.position) < distance) {
    //        //PlayerPrefs.SetInt("spwanPointIndex", spawnPointIndex);

    //        SceneManager.LoadScene(sceneName, LoadSceneMode.Single); 
    //    }

    //    /*if(Vector3.Distance(player.position, transform.position) < distance)
    //    {
    //        //print("Right Distance: " + itemSlot.gameObject.activeSelf);
    //        if(itemSlot != null && itemSlot.item != null)
    //        {
    //            if(itemSlot.gameObject != null)
    //            {
    //                // foreach (var item in saveScene.sceneObjects.objects)
    //                // {
    //                //     if(item.gameObject == itemSlot.gameObject)
    //                //     {
    //                //         item.isEnabled = false;
    //                //     }
    //                // }
    //                // saveScene.sceneObjects.objects[IndexOfScene(itemSlot.gameObject)].isEnabled = false;
    //                // //Destroy(itemSlot.gameObject);
    //                // itemSlot.NotSave();

    //                PlayerPrefs.SetString("TranstionItem", itemSlot.item.name);
    //                //print(PlayerPrefs.GetString("TranstionItem"));

    //                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);  
    //            }
    //        }

    //    }*/
    //}

    public void LoadScene() {
        GameManager gameManager = GameManager.instance;

        gameManager.cutscenePlaying = false;
        gameManager.inMainMenu = false;
        gameManager.backgroundMusic.Stop();
        gameManager.gameplayUI.SetActive(true);

        gameManager.playerControllers = new List<ScenePlayerObject>();
        gameManager.gameData.highestChapterCompleted = MythologyMayhem.Chapter.Greek;
        gameManager.gameData.highestLevelCompleted = MythologyMayhem.Level.GreekMedusa_3D;
        gameManager.gameData.SetStartScene(false);

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        DontDestroyOnLoad(gameManager);
        gameManager.startSceneLoaded = false;
        gameManager.checkStart = false;
        gameManager.checkProx = false;
        gameManager.checkUnneeded = false;
        gameManager.pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
