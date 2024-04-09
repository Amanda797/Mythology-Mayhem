using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflGhostScript : MonoBehaviour
{
    public Material mat;
    public Color startColor;
    public Color endColor;

    public float timer;
    float startTimer;

    public float rotSpeed;
    public float liftSpeed;
    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        startTimer = timer;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, rotSpeed * Time.deltaTime);
        transform.position += Vector3.up * liftSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        float t = timer / startTimer;

        if (t <= 1)
        {
            mat.color = Color.Lerp(startColor, endColor, t);
        }
        else 
        {
            Destroy(this.gameObject);
        }

    }
}
