using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerSelectable", menuName = "playerSelectable", order = 1)]
public class playerSelectable : ScriptableObject
{
    public List<GameObject> playerPrefabs = new List<GameObject>();
}
