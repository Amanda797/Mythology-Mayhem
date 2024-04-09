using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectID : MythologyMayhem
{
    [SerializeField]
    [Tooltip("ID must NOT be equal to 0.")]
    public int PersonalID = 0;

    private void Start()
    {
        if(PersonalID == 0)
        {
            Debug.LogWarning("Object ID for " + gameObject.name + " has not been set / is invalid (0), destroying game object to avoid errors.");
            Destroy(gameObject);
        }
    }
}
