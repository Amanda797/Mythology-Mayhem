using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScene : MonoBehaviour
{
    public bool LoadScene;
    public SceneObjects sceneObjects;

    public static SaveScene instance;
    // Start is called before the first frame update
    void Awake()
    {
        //Save();
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    void Start()
    {
        foreach (var item in sceneObjects.spawnedObjs)
        {
            // instantiate the object and set the position and the active state
            item.gameObject = Instantiate(item.prefab, item.position, Quaternion.identity);
                
        }
        if(LoadScene) Load();
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in sceneObjects.objects)
        {
            if(item.gameObject != null)
            {
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
    void OnDestroy()
    {
        Save();
    }

    public void Save()
    {
        //Save the scene name by writing it in a string variable
        //write each object in the list if it null or not and the position of the object
        
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        
        string sceneObjectsString = JsonUtility.ToJson(sceneObjects);
        print(sceneObjectsString);
        PlayerPrefs.SetString(sceneName, sceneObjectsString);

    }
    public void Load()
    {
        //Load the scene name by reading it from a string variable
        //read each object in the list if it null or not and the position of the object
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string sceneObjectsString = PlayerPrefs.GetString(sceneName);
        sceneObjects = JsonUtility.FromJson<SceneObjects>(sceneObjectsString);
        //print(sceneObjects.objects[1].transform.position);
        // print(sceneObjects.objects[1].position);

        foreach (var item in sceneObjects.objects)
        {
            if(!item.isNull)
            {
                item.gameObject.transform.position = item.position;
                item.gameObject.transform.localScale = item.scale;
                item.gameObject.SetActive(item.isEnabled);
            }
            else
            {
                Destroy(item.gameObject);
            }
            
        }
        foreach (var item in sceneObjects.spawnedObjs)
        {
            if(!item.isNull)
            {
                item.gameObject = Instantiate(item.prefab, item.position, Quaternion.identity);
                item.gameObject.transform.localScale = item.scale;
                item.gameObject.SetActive(item.isEnabled);
            }
            else
            {
                Destroy(item.gameObject);
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
    public GameObject gameObject;
    [HideInInspector]
    public GameObject prefab;
    public Vector3 position;
    public Vector3 scale;
    public bool isNull;
    public bool isEnabled;
    public SceneObject(GameObject gameObject, Vector3 position, GameObject prefab = null)
    {
        this.gameObject = gameObject;
        this.position = position;
        this.prefab = prefab;
    }
}
