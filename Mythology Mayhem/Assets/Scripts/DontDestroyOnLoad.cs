using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

     public GameObject apple;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(apple);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
