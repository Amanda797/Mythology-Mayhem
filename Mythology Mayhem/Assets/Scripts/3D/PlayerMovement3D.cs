using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float speed = 6;
    public float sensitivity = 2;
    Rigidbody rb;

    public Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = transform.Find("Player Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //Move Forward
        float xMove = Input.GetAxisRaw("Vertical");
        if(xMove != 0) {
            rb.velocity = transform.forward * xMove * speed;
        } else {
            rb.velocity = Vector3.zero;
        }

        //Rotate Around Y Axis
        float yMove = Input.GetAxisRaw("Horizontal");
        float rotationX = yMove * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -30, 30);
        transform.Rotate(new Vector3(0.0f, rotationX, 0.0f));   
    }
}
