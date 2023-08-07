using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenePlayerObject : MythologyMayhem
{
    public string _name;
    public PlayerType type;
    public Level inScene;

    public PlayerAttach player;


    public enum PlayerType
    {
        ThreeD,
        TwoD
    }

    public ScenePlayerObject(PlayerAttach _player, PlayerType _type, Level scene)
    {
        player = _player;
        type = _type;
        inScene = scene;

        _name = type.ToString() + " Character in " + inScene.ToString();
    }
}
