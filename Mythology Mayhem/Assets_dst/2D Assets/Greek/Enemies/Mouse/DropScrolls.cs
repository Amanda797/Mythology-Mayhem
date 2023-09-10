using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScrolls : MonoBehaviour
{
    float countdown;
    public GameObject fallingScroll;
    // Start is called before the first frame update
    void Start()
    {
        countdown = 4f;
    }//end start

    // Update is called once per frame
    void Update()
    {
        if(countdown <= 0) {
            //spawn fallingscroll prefab
            Instantiate(fallingScroll, transform.position, Quaternion.identity);
            countdown = Random.Range(2.5f,5f);
        } else {
            countdown -= 1 * Time.deltaTime;
        }
    }//end update

}
