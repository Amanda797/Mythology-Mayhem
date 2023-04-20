using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionControl : MonoBehaviour
{
    public superliminal sl;
    public KeyCode key;
    public GameObject vision;
    public float time;
    float timer;
    GameObject[] objs;
    // Start is called before the first frame update
    void Start()
    {
        vision.SetActive(false);
        objs = GameObject.FindGameObjectsWithTag("Outline");
        
    }
    void Update()
    {
        if(sl.isReady == true)
        {
            foreach(GameObject obj in objs)
            {
                if(obj.GetComponent<Outline>() != null)
                    obj.GetComponent<Outline>().enabled = true;
                
            }
        }
        else
        {
            foreach(GameObject obj in objs)
            {
                if(obj.GetComponent<Outline>() != null)
                    obj.GetComponent<Outline>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKeyDown(sl.key) && !sl.isReady)
        {
            sl.isReady = true;
            vision.SetActive(true);
        }
        if(Input.GetKey(sl.key))
        {
            timer += Time.deltaTime;
            if(timer >= time)
            {
                timer = 0;
                sl.isReady = false;
                //sl.Reset();
                sl.CheckForObjects();
                vision.SetActive(false);
            }
        }
        if(Input.GetKeyUp(sl.key))
        {
            timer = 0;
        }

        if(Input.GetKeyDown(key))
        {
            timer = 0;
            sl.isReady = false;
            //sl.Reset();
            sl.CheckForObjects();
            vision.SetActive(false);
        }
    }
}
