using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate2D : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    float randomFrequency;
    private Vector2 posOffset = new Vector2();
    private Vector2 tempPos = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        randomFrequency = Random.Range(0.5f, frequency + .5f);
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * randomFrequency) * amplitude;
        transform.position = tempPos;
    }
}
