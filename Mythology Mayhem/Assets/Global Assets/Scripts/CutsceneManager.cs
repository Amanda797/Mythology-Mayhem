using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    float cutsceneTimer;
    [SerializeField] string nextScene;
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
            SceneManager.LoadScene(nextScene);
        } 
        
        // Change scene after video is done playing
        if (cutsceneTimer > 0) {
            cutsceneTimer -= 1 * Time.deltaTime;
        } else {
            SceneManager.LoadScene(nextScene);
        }
    }
}
