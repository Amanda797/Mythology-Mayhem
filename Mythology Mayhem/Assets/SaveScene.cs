using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScene : MonoBehaviour
{
    public bool LoadScene;
    public SceneObjects sceneObjects;

    bool saving;

    public static SaveScene instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(LoadScene) Load();
        //Save();
        //if(LoadScene) Load();
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            
        }
        foreach (var item in sceneObjects.spawnedObjs)
        {
            // instantiate the object and set the position and the active state
            item.gameObject = Instantiate(item.prefab, item.position, Quaternion.identity);
                
        }
        
        saving = true;

        //print("Loaded Game");

        
    }
    void Start()
    {
        
        // foreach (var item in sceneObjects.spawnedObjs)
        // {
        //     // instantiate the object and set the position and the active state
        //     item.gameObject = Instantiate(item.prefab, item.position, Quaternion.identity);
                
        // }
        // if(LoadScene) Load();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Load();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            Save();
        }
        if(saving)
        {
            foreach (var item in sceneObjects.objects)
        {
            if(item.gameObject != null)
            {
                item.name = item.gameObject.name;
                item.position = item.gameObject.transform.position;
                item.scale = item.gameObject.transform.localScale;
                item.isEnabled = item.gameObject.activeSelf;
            }
            else
            {
                item.isNull = true;
            }
                
        }

        foreach (var item in sceneObjects.spawnedObjs)
        {
            if(item.gameObject != null)
            {
                item.name = item.gameObject.name;
                item.position = item.gameObject.transform.position;
                item.scale = item.gameObject.transform.localScale;
                item.isEnabled = item.gameObject.activeSelf;
            }
            else
            {
                item.isNull = true;
            }
                
        }
        }
        
        
    }
    void OnDestroy()
    {
        saving = false;
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
        
        string sceneObjectsString = JsonUtility.ToJson(sceneObjects);
        print(sceneObjectsString);
        //PlayerPrefs.SetString(sceneName, sceneObjectsString);

        //write the string in a file
        System.IO.File.WriteAllText(Application.dataPath + "/_SceneData/" + sceneName + ".json", sceneObjectsString);
        //refresh the project to see the file
        UnityEditor.AssetDatabase.Refresh();

    }
    public void Load()
    {
        //Load the scene name by reading it from a string variable
        //read each object in the list if it null or not and the position of the object
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //string sceneObjectsString = PlayerPrefs.GetString(sceneName);
        if(!System.IO.File.Exists(Application.dataPath + "/_SceneData/" + sceneName + ".json"))
        {
            print("No save data");
            this.SaveNow();
            return;
        }
        string sceneObjectsString = System.IO.File.ReadAllText(Application.dataPath + "/_SceneData/" + sceneName + ".json");

        sceneObjects = JsonUtility.FromJson<SceneObjects>(sceneObjectsString);
        //print(sceneObjects.objects[1].transform.position);
        // print(sceneObjects.objects[1].position);

        foreach (var item in sceneObjects.objects)
        {
            if(item.gameObject != null)
            {
                item.gameObject.transform.position = item.position;
                item.gameObject.transform.localScale = item.scale;
                item.gameObject.SetActive(item.isEnabled);
            }
            else
            {
                //Destroy(item.gameObject);
                // find gameobject by name
                GameObject obj = GameObject.Find(item.name);
                if(obj != null)
                {
                    item.gameObject = obj;
                    obj.transform.position = item.position;
                    obj.transform.localScale = item.scale;
                    obj.SetActive(item.isEnabled);
                }
            }
            
        }
        foreach (var item in sceneObjects.spawnedObjs)
        {
            if(item.gameObject != null)
            {
                item.gameObject = Instantiate(item.prefab, item.position, Quaternion.identity);
                item.gameObject.transform.localScale = item.scale;
                item.gameObject.SetActive(item.isEnabled);
            }
            else
            {
                //Destroy(item.gameObject);
                GameObject obj = GameObject.Find(item.name);
                if(obj != null)
                {
                    item.gameObject = obj;
                    obj.transform.position = item.position;
                    obj.transform.localScale = item.scale;
                    obj.SetActive(item.isEnabled);
                }
            }
            
        }
    }
    public static void AddObject(GameObject gameObject, GameObject prefab = null)
    {
        instance.sceneObjects.spawnedObjs.Add(new SceneObject(gameObject, gameObject.transform.position, prefab));
    }
}
[System.Serializable]
public class SceneObjects
{
    public List<SceneObject> objects = new List<SceneObject>();

    public List<SceneObject> spawnedObjs = new List<SceneObject>();
    public SceneObjects(string sceneName, List<SceneObject> objects)
    {
        this.objects = objects;
    }
}
[System.Serializable]
public class SceneObject
{
    [HideInInspector]
    public string name;
    public GameObject gameObject;
    [HideInInspector]
    public GameObject prefab;
    //[HideInInspector]
    public Vector3 position;
    //[HideInInspector]
    public Vector3 scale;
    [HideInInspector]
    public bool isNull;
    //[HideInInspector]
    public bool isEnabled;
    public SceneObject(GameObject gameObject, Vector3 position, GameObject prefab = null)
    {
        this.gameObject = gameObject;
        this.position = position;
        this.prefab = prefab;
    }
}
