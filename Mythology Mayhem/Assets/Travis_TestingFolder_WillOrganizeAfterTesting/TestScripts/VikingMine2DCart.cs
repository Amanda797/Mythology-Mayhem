using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingMine2DCart : MonoBehaviour
{

    public Transform parentTrack;
    public SpriteRenderer rend;

    public float speed;

    public float deathTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);

        deathTimer -= Time.deltaTime;

        if (deathTimer <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
