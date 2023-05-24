using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeDLeverPuzzle : MonoBehaviour
{

    public bool threeDSwitchOn = false;
    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;
    public GameObject lever4;
    public GameObject lever5;
    public GameObject lever6;
    public GameObject lever7;
    public GameObject lever8;
    public GameObject lever9;
    public GameObject lever10;
    
    static public Quaternion lever1Pos;
    static public Quaternion lever2Pos;
    static public Quaternion lever3Pos;
    static public Quaternion lever4Pos;
    static public Quaternion lever5Pos;
    static public Quaternion lever6Pos;
    static public Quaternion lever7Pos;
    static public Quaternion lever8Pos;
    static public Quaternion lever9Pos;
    static public Quaternion lever10Pos;

    

    // Start is called before the first frame update
    void Start()
    {
       lever1Pos = lever1.transform.rotation;
       lever2Pos = lever2.transform.rotation;
       lever3Pos = lever3.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

}
