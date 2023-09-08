using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionControl : MythologyMayhem
{
    public superliminal sl;
    public KeyCode key;
    public GameObject vision;
    public float time;

    public GameObject gem;
    float timer;
    GameObject[] objs;
    List<GameObject> gems = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public float Dis;
    public Transform player;

    public static VisionControl instance;

    public WeaponSwitcher weaponSwitcher;
    // Start is called before the first frame update
    void Start()
    {
        gem.SetActive(false);
        vision.SetActive(false);
        instance = this;
        objs = GameObject.FindGameObjectsWithTag("Outline");
        // find gems in scene and add them to list
        gems.AddRange(GameObject.FindGameObjectsWithTag("Gem"));
        
        //gem = GameObject.FindGameObjectsWithTag("Gem");
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        
    }
    void Update()
    {
        GameObject[] gemInScene = GameObject.FindGameObjectsWithTag("Gem");

        // if there are more gems in scene than in list, add them to list
        if(gemInScene.Length > gems.Count)
        {
            foreach(GameObject gem in gemInScene)
            {
                if(!gems.Contains(gem))
                    gems.Add(gem);
            }
        }

        if(sl.isReady == true)
        {
            foreach(GameObject obj in objs)
            {
                if(obj.GetComponent<Outline>() != null)
                    obj.GetComponent<Outline>().enabled = true;
                
            }
            foreach(GameObject gem in gems)
            {
                if(gem.GetComponent<Outline>() != null)
                    gem.GetComponent<Outline>().enabled = true;
            }
        }
        else
        {
            foreach(GameObject obj in objs)
            {
                if(obj.GetComponent<Outline>() != null)
                    obj.GetComponent<Outline>().enabled = false;
            }
            foreach(GameObject gem in gems)
            {
                if(gem.GetComponent<Outline>() != null)
                    gem.GetComponent<Outline>().enabled = false;
            }
        }

        
    }

    public static void RemoveEnemy(GameObject enemy)
    {
        instance.enemies.Remove(enemy);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (weaponSwitcher.currentOffHand == OffHand.Crystal)
        {
            if (Input.GetMouseButtonDown(sl.mousebutton) && !sl.isReady)
            {
                sl.isReady = true;
                vision.SetActive(true);
                gem.SetActive(true);
                LeanTween.moveLocalY(gem, gem.transform.localPosition.y, gem.transform.localPosition.y + 5f).setEasePunch();
            }
            if (Input.GetMouseButton(sl.mousebutton) && sl.isReady)
            {
                timer += Time.deltaTime;
                if (timer >= time)
                {
                    timer = 0;
                    sl.isReady = false;
                    //sl.Reset();
                    sl.CheckForObjects();
                    vision.SetActive(false);
                    gem.SetActive(false);
                }
            }
            if (Input.GetMouseButtonDown(sl.mousebutton))
            {
                timer = 0;
            }

            if (Input.GetMouseButtonUp(sl.mousebutton))
            {
                timer = 0;
                sl.isReady = false;
                //sl.Reset();
                sl.CheckForObjects();
                vision.SetActive(false);
            }

            if (enemies.Count > 0)
            {
                foreach (GameObject enemy in enemies)
                {
                    if (enemy == null)
                        continue;
                    if (Vector3.Distance(enemy.transform.position, player.position) <= Dis)
                    {
                        Reset();
                    }
                }
            }
        }
    }

    public void Reset()
    {
        timer = 0;
        sl.isReady = false;
        //sl.Reset();
        sl.CheckForObjects();
        vision.SetActive(false);
    }
}
