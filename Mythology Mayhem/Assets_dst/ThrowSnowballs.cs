using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSnowballs : MonoBehaviour
{
    [SerializeField] Transform throwLocation;
    [SerializeField] GameObject snowballPrefab;
    [SerializeField] float throwVelocity;
    [SerializeField] float throwOffsetX, throwOffsetY;
    [SerializeField] float throwAngle = 15f;
    [SerializeField] float timer = 4f;
    float counter = 0f;

    //Testing
    [SerializeField] bool testThrow;

    // Start is called before the first frame update
    void Start()
    {
        testThrow = false;

        if(snowballPrefab == null) {
            Debug.LogWarning("Snowball prefab is not loaded.");
        }
    }//end start

    void Update() {
        if(testThrow) {
            testThrow = false;
            ThrowSnowball();
        }

        counter += 1 * Time.deltaTime;
        if(counter > timer) {
            counter = 0;
            ThrowSnowball();
        }
    }//end update

    public void ThrowSnowball() {
        Vector3 offsetPosition = new Vector3(throwLocation.position.x + throwOffsetX, throwLocation.position.y + throwOffsetY, throwLocation.position.z);

        Vector3 offsetRotation = new Vector3(0,0,0);

        GameObject snowball = Instantiate(snowballPrefab, offsetPosition, throwLocation.rotation);

        snowball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(throwVelocity * Mathf.Sign(throwLocation.rotation.y),0,0));
    }//end throw snowball
}
