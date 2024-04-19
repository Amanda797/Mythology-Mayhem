using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSaveDataPosition", menuName = "SaveData/Position")]
public class SaveDataPosition : ScriptableObject
{
    public MythologyMayhem.Level currentLevel;
    public string dataTag;
    public Vector3 position;
}
