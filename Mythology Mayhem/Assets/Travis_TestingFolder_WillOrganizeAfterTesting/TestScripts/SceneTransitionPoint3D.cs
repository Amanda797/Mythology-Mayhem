using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPoint3D : SceneTransitionPoint
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        isActive = CheckConditionsMeet();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerAttach player = other.gameObject.GetComponent<PlayerAttach>();
            if (player != null)
            {
                if (keyPress)
                {
                    localGameManager.SceneTransition(sceneToTransition);
                    keyPress = false;
                }

            }
        }
    }
}