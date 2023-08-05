using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenePlayerObject
{
    public string name;
    public PlayerType type;
    public string inScene;

    public PlayerAttach player;


    public enum PlayerType
    {
        ThreeD,
        TwoD
    }

    public ScenePlayerObject(PlayerAttach _player, PlayerType _type, string sceneName)
    {
        player = _player;
        type = _type;
        inScene = sceneName;

        name = (type.ToString() + " Character in " + inScene);
    }
}
