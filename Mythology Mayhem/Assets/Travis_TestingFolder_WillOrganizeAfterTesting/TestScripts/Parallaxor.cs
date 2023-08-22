using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxor : MythologyMayhem
{

    public LocalGameManager localGameManager;

    public GameObject[] Layer0Holders;
    public GameObject[] Layer1Holders;
    public GameObject[] Layer2Holders;
    public GameObject[] Layer3Holders;
    public GameObject[] Layer4Holders;
    public GameObject[] Layer5Holders;
    public GameObject[] Layer6Holders;
    public GameObject[] Layer7Holders;
    public GameObject[] Layer8Holders;
    public GameObject[] Layer9Holders;
    public GameObject[] Layer10Holders;

    public float[] layerSpeeds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (localGameManager.player && localGameManager.sceneType == Dimension.TwoD && localGameManager.inScene == localGameManager.mainGameManager.currentScene) 
        {
            float sceneX = localGameManager.sceneOrigin.position.x;
            float playerX = localGameManager.player.gameObject.transform.position.x;

            float diffX = (sceneX - playerX) * -1;

            float[] modifierX = new float[layerSpeeds.Length];

            for (int i = 0; i < layerSpeeds.Length; i++) 
            {
                modifierX[i] = diffX * layerSpeeds[i];
            }

            LayerPositionSet(Layer0Holders, modifierX[0]);
            LayerPositionSet(Layer1Holders, modifierX[1]);
            LayerPositionSet(Layer2Holders, modifierX[2]);
            LayerPositionSet(Layer3Holders, modifierX[3]);
            LayerPositionSet(Layer4Holders, modifierX[4]);
            LayerPositionSet(Layer5Holders, modifierX[5]);
            LayerPositionSet(Layer6Holders, modifierX[6]);
            LayerPositionSet(Layer7Holders, modifierX[7]);
            LayerPositionSet(Layer8Holders, modifierX[8]);
        }
    }

    void LayerPositionSet(GameObject[] layer, float newX) 
    {
        foreach (GameObject obj in layer) 
        {
            float sceneX = localGameManager.sceneOrigin.position.x;
            float modX = sceneX + newX;
            Vector3 currentPos = obj.transform.position;
            currentPos.x = modX;
            obj.transform.position = currentPos;

        }
    }
}
