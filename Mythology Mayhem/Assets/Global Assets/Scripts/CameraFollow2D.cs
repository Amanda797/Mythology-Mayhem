using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    GameManager gameManager;
    public Camera camera2D;
    public GameObject target;
    public float buffer;
    public float height;
    Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");

        start = transform.position;
        height = target.transform.position.y + 12;
    }

    private void OnEnable()
    {
        height = target.transform.position.y + 12;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.currentLocalManager.boundaries.Length > 1)
        {
            Transform[] border = gameManager.currentLocalManager.boundaries;
            float left = Mathf.Min(border[0].position.x, border[1].position.x) + camera2D.orthographicSize * camera2D.aspect + buffer;
            float right = Mathf.Max(border[0].position.x, border[1].position.x) - camera2D.orthographicSize * camera2D.aspect - buffer;
            height = Mathf.Min(target.transform.position.y + 4, height);
            transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, left, right), height, transform.position.z);
        }
        else
            transform.position = new Vector3(transform.position.x, start.y, transform.position.z);
    }
}
