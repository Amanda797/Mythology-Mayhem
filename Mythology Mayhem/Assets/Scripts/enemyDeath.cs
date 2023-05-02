using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDeath : MonoBehaviour
{
    void OnDisable()
    {
        VisionControl.RemoveEnemy(gameObject);
    }
}
