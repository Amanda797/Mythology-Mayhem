using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate2D : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float zValue = 0f;
    float randomFrequency;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

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
        tempPos.z = zValue;
        transform.position = tempPos;
    }
}
