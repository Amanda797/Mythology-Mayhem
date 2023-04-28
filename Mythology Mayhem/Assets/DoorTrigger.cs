using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    ChangeNextSpawn changeNextSpawnPoint;
    public SaveScene SaveScene;
    // Start is called before the first frame update
    void Start()
    {
        changeNextSpawnPoint = GetComponent<ChangeNextSpawn>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            changeNextSpawnPoint.NextSpawn(1);
            SaveScene.Save();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            changeNextSpawnPoint.NextSpawn(1);
            //SaveScene.Save();
        }
    }
}
