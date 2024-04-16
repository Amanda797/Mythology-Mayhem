using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif


public class SaveScene : MonoBehaviour
{
    public bool LoadScene;
    [Header("Greek - 2D Labyrinth")]
    public SaveData saveData;

    public static SaveScene instance;

    public bool Loaded;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        instance = this;
        Loaded = false;
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Load();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            
            Save();
        }
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
        print(sceneObjectsString);
        //PlayerPrefs.SetString(sceneName, sceneObjectsString);

        //write the string in a file
        System.IO.File.WriteAllText(Application.dataPath + "/Global Assets/Resources/SceneData/" + sceneName + ".json", sceneObjectsString);
        //refresh the project to see the file
       #if UNITY_EDITOR
          UnityEditor.AssetDatabase.Refresh();
       #endif


    }
    public void Load()
    {
        //Load the scene name by reading it from a string variable
        //read each object in the list if it null or not and the position of the object
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //string sceneObjectsString = PlayerPrefs.GetString(sceneName);
        if(!System.IO.File.Exists(Application.dataPath + "/Global Assets/Resources/SceneData/" + sceneName + ".json"))
        {
            print("No save data");
            this.SaveNow();
            return;
        }
        string sceneObjectsString = System.IO.File.ReadAllText(Application.dataPath + "/Global Assets/Resources/SceneData/" + sceneName + ".json");

        saveData = JsonUtility.FromJson<SaveData>(sceneObjectsString);
        print(sceneObjectsString);

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
public class SaveData
{
    [Header("2D Labyrinth")]
    public bool TwoDLabyrinthCompleted;
    public bool collectedMirror;
    public List<bool> TwoDLabyrinthLevers;
}

