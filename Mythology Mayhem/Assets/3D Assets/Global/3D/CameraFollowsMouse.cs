using UnityEngine;

public class CameraFollowsMouse : MonoBehaviour
{

    float rotationX;
    float rotationY;
    float sensitivity = 2f;

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationX += Input.GetAxis("Mouse Y") * -1 * sensitivity;

        rotationX = Mathf.Clamp(rotationX, -30, 30);
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
