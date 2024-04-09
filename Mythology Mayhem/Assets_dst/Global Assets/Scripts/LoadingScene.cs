using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] GameObject LoadingPanel;
    [SerializeField] Image LoadingBarFill;
    [SerializeField] bool testTransition;
    [SerializeField] string testingScene;

    void Update() {
        //Testing the Loading Screen
        if(testTransition) {
            testTransition = false;
            LoadScene(testingScene);
        }
    }
    
    public void LoadScene(string sceneName) {
        //Take the scene name and retrive the build index. Give the build index to LoadSceneAsync
        //TODO: This cannot find the buildIndex from the name because the scene has to be loaded; may create a list of all scenes and extract data fom it
        print(SceneManager.GetSceneByName(sceneName).buildIndex);
        StartCoroutine(LoadSceneAsync(SceneManager.GetSceneByName(sceneName).buildIndex));
    }

    IEnumerator LoadSceneAsync(int sceneID) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingPanel.SetActive(true);

        while(!operation.isDone) {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }

    }//end LoadSceneAsync
}
