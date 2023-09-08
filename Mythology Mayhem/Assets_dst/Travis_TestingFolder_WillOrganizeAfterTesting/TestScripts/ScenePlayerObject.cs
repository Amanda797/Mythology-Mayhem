using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenePlayerObject
{
    public string _name;
    public MythologyMayhem.Dimension type;
    public MythologyMayhem.Level inScene;

    public PlayerAttach player;

    public ScenePlayerObject(PlayerAttach _player, MythologyMayhem.Dimension _type, MythologyMayhem.Level scene)
    {
        player = _player;
        type = _type;
        inScene = scene;

        _name = type.ToString() + " Character in " + inScene.ToString();
    }
}
