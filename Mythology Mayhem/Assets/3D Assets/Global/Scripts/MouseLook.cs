using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f;
    public PlayerMovement3D playerMovement3D;
    // Start is called before the first frame update
    void Start()
    {
        if (playerBody == null)
        {
            playerBody = transform.parent;
        }
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerMovement3D.frozen)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //This is to stop the camera jutter that sends the Y into the ground and the X to the right when loading into the level
            if (Mathf.Abs(mouseY) > 20)
            {
                mouseY = 0;
            }
            if (Mathf.Abs(mouseX) > 20)
            {
                mouseX = 0;
            }

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

    }
}
