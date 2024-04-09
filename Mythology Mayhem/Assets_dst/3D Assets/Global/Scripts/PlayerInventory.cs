using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool earthCollected {get; private set; }

    public void EarthCollect()
    {
        earthCollected = true;
    }
}
