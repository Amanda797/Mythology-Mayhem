using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegirWaveColliderScript : MonoBehaviour
{
    public AegirWaveScript aegirWaveScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        aegirWaveScript.ColliderTrigger(other);
    }
}
