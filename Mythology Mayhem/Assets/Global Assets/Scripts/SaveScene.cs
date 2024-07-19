using System.Collections.Generic;
using UnityEngine;


public class SaveScene : MonoBehaviour
{
    public SaveDataTest saveData;
    public static SaveScene instance;
    public bool LoadScene;
    public bool Loaded;

    void Start()
    {
        Debug.Log("SaveScene: " + this.gameObject.name);
        instance = this;
        Loaded = false;
        Load();
    }

    void OnDestroy()
    {
        this.SaveNow();
    }

    public void Save()
    {
        this.SaveNow();
    }
    
    void SaveNow()
    {        
        //Save the scene name by writing it in a string variable
        //write each object in the list if it null or not and the position of the object
        
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        string sceneObjectsString = JsonUtility.ToJson(saveData, true);

        //write the string in a file
        System.IO.File.WriteAllText(Application.persistentDataPath + sceneName + ".json", sceneObjectsString);
    }
    public void Load()
    {
        //Load the scene name by reading it from a string variable
        //read each object in the list if it null or not and the position of the object

        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //string sceneObjectsString = PlayerPrefs.GetString(sceneName);

        if(!System.IO.File.Exists(Application.persistentDataPath + sceneName + ".json"))
        {
            print("No save data");
            this.SaveNow();
            return;
        }
        string sceneObjectsString = System.IO.File.ReadAllText(Application.persistentDataPath + sceneName + ".json");

        saveData = JsonUtility.FromJson<SaveDataTest>(sceneObjectsString);

        Loaded = true;
    }

    public void UpdateLeverPuzzle(List<bool> levers, bool mirror, bool completed) 
    {
        saveData.TwoDLabyrinthLevers = new List<bool>();
        for (int i = 0; i < levers.Count; i++)
        {
            saveData.TwoDLabyrinthLevers.Add(levers[i]);
        }
        saveData.collectedMirror = mirror;
        saveData.TwoDLabyrinthCompleted = completed;
        Save();
    }
}

[System.Serializable]
public class SaveDataTest
{
    [Header("2D Labyrinth")]
    public bool TwoDLabyrinthCompleted;
    public bool collectedMirror;
    public List<bool> TwoDLabyrinthLevers;
}

