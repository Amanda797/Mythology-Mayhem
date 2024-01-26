using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kubb
{
    public Rigidbody rb;
    public Type type;
    public enum Type 
    { 
        Baseline,
        Field
    }

    public Kubb(Rigidbody rigidbody, Type _type) 
    {
        rb = rigidbody;
        type = _type;
    }
}
