using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPoint : MythologyMayhem
{
    public LocalGameManager localGameManager;
    public Level sceneToTransition;
    public bool keyPress;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckInput()
    {
        if (localGameManager.inScene == localGameManager.mainGameManager.currentScene && isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                keyPress = true;
                StartCoroutine(ButtonPress());
            }
        }
    }

    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(0.25f);
        keyPress = false;
        yield return null;
    }
}
