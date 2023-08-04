using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFocus : MonoBehaviour
{
    [Tooltip("Objects to disable when special Canvases are at use")]
    [SerializeField] List<GameObject> focus;
    bool status;

    void Start() {
        status = true;
    }

    public void ToggleFocus() {
        foreach(GameObject go in focus) {
            status = !status;
            go.SetActive(status);
        }
    }
}
