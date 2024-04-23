using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    float cutsceneTimer;
    [SerializeField] MythologyMayhem.Level nextScene;
    // Start is called before the first frame update
    void Start()
    {
        cutsceneTimer = (float) GetComponent<VideoPlayer>().clip.length + 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for early player skipping
        if(Input.GetMouseButtonDown(0)) {
            LoadNextScene();
            /*
            if(nextScene == "Library of Alexandria")
            {
                PlayerPrefs.SetString("spawningScene", nextScene);
            }
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            */
        } 
        
        // Change scene after video is done playing
        if (cutsceneTimer > 0) {
            cutsceneTimer -= 1 * Time.deltaTime;
        } else
        {
            LoadNextScene();
            /*
            if (nextScene == "Library of Alexandria")
            {
                PlayerPrefs.SetString("spawningScene", nextScene);
            }
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            */
        }
    }

    void LoadNextScene() 
    {
        if (GameManager.instance != null) 
        {
            if (nextScene == MythologyMayhem.Level.GreekLibrary_2D)
            {
                GameManager.instance.currentScene = MythologyMayhem.Level.GreekLibrary_2D;
                GameManager.instance.gameplayUI.SetActive(true);
                GameManager.instance.cutscenePlaying = false;
            }
            else 
            {
                GameManager.instance.currentScene = MythologyMayhem.Level.CutScene2;
            }
            GameManager.instance.LoadScene(nextScene, true);
        }
    }
}
