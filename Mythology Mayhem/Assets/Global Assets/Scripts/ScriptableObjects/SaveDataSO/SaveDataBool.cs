using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSaveDataBool", menuName = "SaveData/Bool")]
public class SaveDataBool : ScriptableObject
{
    public MythologyMayhem.Level currentLevel;
    public string dataTag;
    public bool boolData;
}
