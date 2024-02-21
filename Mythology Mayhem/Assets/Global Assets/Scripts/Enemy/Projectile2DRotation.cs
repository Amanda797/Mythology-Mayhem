using UnityEngine;

public class Projectile2DRotation : MonoBehaviour
{
    public float RotationSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }
}
