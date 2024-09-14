using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    Vector3 position;
    Vector3 scale;
    Quaternion rotation;
    bool queueReset = false;
    LocalGameManager currentScene;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPlayerAlive)
            queueReset = true;
        if (GameManager.instance.isPlayerAlive && queueReset)
            ResetSelf();
        if (currentScene != GameManager.instance.currentLocalManager)
            ResetSelf();
    }

    private void ResetSelf()
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
        queueReset = false;
        currentScene = GameManager.instance.currentLocalManager;
    }
}
