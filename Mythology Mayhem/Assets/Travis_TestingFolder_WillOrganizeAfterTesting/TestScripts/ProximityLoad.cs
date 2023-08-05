using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProximityLoad
{
    public string sceneToLoad;
    public SceneTransitionPoint transitionPoint;
    public float proximityDistance;
    public bool loaded;

    public ProximityLoad(string _scene, SceneTransitionPoint _transitionPoint, float _dist) 
    {
        sceneToLoad = _scene;
        transitionPoint = _transitionPoint;
        proximityDistance = _dist;
    }
}
