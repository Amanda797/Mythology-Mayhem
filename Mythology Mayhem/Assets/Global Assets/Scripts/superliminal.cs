using UnityEngine;
 
public class superliminal : MonoBehaviour
{
    [Header("Components")]
    public Transform target;  
    GameObject targetObject;         // The target object we picked up for scaling
 
    [Header("Parameters")]
    public LayerMask targetMask;        // The layer mask used to hit only potential targets with a raycast
    public LayerMask ignoreTargetMask;  // The layer mask used to ignore the player and target objects while raycasting
    public LayerMask ignorePlayerMask;  // The layer mask used to ignore the player and target objects while raycasting
    public float offsetFactor;          // The offset amount for positioning the object so it doesn't clip into walls
 
    float originalDistance;             // The original distance between the player camera and the target
    float originalScale;                // The original scale of the target objects prior to being resized
    Vector3 targetScale;
    public Vector3 scale;          // The scale we want our object to be set to each frame
    public int mousebutton;
    //public KeyCode key;
    public bool isReady;
    public bool isUsing;

    //void Start()
    //{
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    void Update()
    {
        HandleInput();
        ResizeTarget();
    }
    
    void HandleInput()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(mousebutton) && isReady)
        {
            isUsing = true;
            if (target == null)
            {
                // Fire a raycast with the layer mask that only hits potential targets
                RaycastHit hit;

                if(Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // Set our target variable to be the Transform object we hit with our raycast
                    target = hit.transform;
                    targetObject = hit.transform.gameObject;
                    //targetObject.layer = 11;
                    Debug.Log(targetObject.GetComponent<AudioSource>() != null);
                    if (targetObject.GetComponent<AudioSource>() != null) targetObject.GetComponent<AudioSource>().Play();

                    // Disable physics for the object
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    target.GetComponent<Collider>().isTrigger = true;
                    //target.GetComponent<Rigidbody>().useGravity = false;
 
                    // Calculate the distance between the camera and the object
                    originalDistance = Vector3.Distance(transform.position, target.position);
 
                    // Save the original scale of the object into our originalScale Vector3 variabble
                    originalScale = target.localScale.x;
 
                    // Set our target scale to be the same as the original for the time being
                    targetScale = target.localScale;
                }
                // if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                // {
                //     // Set our target variable to be the Transform object we hit with our raycast
                //     target = hit.transform;
 
                //     // Disable physics for the object
                //     target.GetComponent<Rigidbody>().isKinematic = true;
                //     //target.GetComponent<Rigidbody>().useGravity = false;
 
                //     // Calculate the distance between the camera and the object
                //     originalDistance = Vector3.Distance(transform.position, target.position);
 
                //     // Save the original scale of the object into our originalScale Vector3 variabble
                //     originalScale = target.localScale.x;
 
                //     // Set our target scale to be the same as the original for the time being
                //     targetScale = target.localScale;
                // }
            }
            // If we DO have a target
            else
            {
                // Reactivate physics for the target object
                target.GetComponent<Rigidbody>().isKinematic = false;
                target.GetComponent<Collider>().isTrigger = false;
                //targetObject.layer = 10;
                targetObject = null;
                offsetFactor = 0f;
                //target.GetComponent<Rigidbody>().useGravity = true;
 
                // Set our target variable to null
                target = null;
                isUsing = false;
            }
        }
    }

    public void CheckForObjects()
    {
        if (target == null)
            {
                // Fire a raycast with the layer mask that only hits potential targets
                RaycastHit hit;

                if(Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // Set our target variable to be the Transform object we hit with our raycast
                    target = hit.transform;
                    targetObject = hit.transform.gameObject;
                    //targetObject.layer = 11;
                    if (targetObject.GetComponent<AudioSource>() != null) targetObject.GetComponent<AudioSource>().Play();
                    // Disable physics for the object
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    target.GetComponent<Collider>().isTrigger = true;
                    //target.GetComponent<Rigidbody>().useGravity = false;
 
                    // Calculate the distance between the camera and the object
                    originalDistance = Vector3.Distance(transform.position, target.position);
 
                    // Save the original scale of the object into our originalScale Vector3 variabble
                    originalScale = target.localScale.x;
 
                    // Set our target scale to be the same as the original for the time being
                    targetScale = target.localScale;
                }
                // if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                // {
                //     // Set our target variable to be the Transform object we hit with our raycast
                //     target = hit.transform;
 
                //     // Disable physics for the object
                //     target.GetComponent<Rigidbody>().isKinematic = true;
                //     //target.GetComponent<Rigidbody>().useGravity = false;
 
                //     // Calculate the distance between the camera and the object
                //     originalDistance = Vector3.Distance(transform.position, target.position);
 
                //     // Save the original scale of the object into our originalScale Vector3 variabble
                //     originalScale = target.localScale.x;
 
                //     // Set our target scale to be the same as the original for the time being
                //     targetScale = target.localScale;
                // }
            }
            // If we DO have a target
            else
            {
                // Reactivate physics for the target object
                target.GetComponent<Rigidbody>().isKinematic = false;
                target.GetComponent<Collider>().isTrigger = false;
                //targetObject.layer = 10;
                targetObject = null;
                offsetFactor = 0f;
                //target.GetComponent<Rigidbody>().useGravity = true;
 
                // Set our target variable to null
                target = null;
            }
    }
 
    void ResizeTarget()
    {
        // If our target is null
        if (target == null)
        {
            // Return from this method, nothing to do here
            return;
        }
 
        // Cast a ray forward from the camera position, ignore the layer that is used to acquire targets
        // so we don't hit the attached target with our ray
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            // Set the new position of the target by getting the hit point and moving it back a bit
            // depending on the scale and offset factor
            //Rigidbody rb = target.GetComponent<Rigidbody>();
            // Vector3 targetPos = hit.point + -transform.forward * offsetFactor * targetScale.x;
            // Vector3 DirectionToPoint = targetPos - target.position;
            // float DistanceToPoint = DirectionToPoint.magnitude;
            // rb.velocity = DirectionToPoint.normalized * DistanceToPoint * 20f;
            
            target.position = hit.point + transform.TransformDirection(new Vector3 (0, 0, -offsetFactor));
            //target.position = hit.point + -transform.forward * offsetFactor * targetScale.x/scale.x;

            //use overlap sphere to check if there is any object in the way
            //if there is, move the target towards the player
            
            // Collider[] hitColliders = Physics.OverlapSphere(target.position, target.localScale.x/scale);
            // if(hitColliders.Length > 1)
            // {
            //     //Debug.Log("hit");
            //     offsetFactor += 0.1f;
            // }

            //use overlap Box to check if there is any object in the way
            //if there is, move the target towards the player
            
            Collider[] hitColliders = Physics.OverlapBox(target.position, new Vector3(target.localScale.x/scale.x, target.localScale.y/scale.y, target.localScale.z/scale.z), Quaternion.identity, ignorePlayerMask);
            if(hitColliders.Length > 1)
            {
                //Debug.Log("hit");
                offsetFactor += 0.1f;
            }

            if(offsetFactor > 5)
            {
                offsetFactor = 5;
            }

            
            
            
            
            // Calculate the current distance between the camera and the target object
            float currentDistance = Vector3.Distance(transform.position, target.position);
 
            // Calculate the ratio between the current distance and the original distance
            float s = currentDistance / originalDistance;
 
            // Set the scale Vector3 variable to be the ratio of the distances
            targetScale.x = targetScale.y = targetScale.z = s;
            
            Vector3 tarScale = targetScale * originalScale;
            if(targetScale.x * originalScale > 60f)
            {
                tarScale = new Vector3(60f, 60f, 60f);
            } else if (targetScale.x * originalScale < 2f)
            {
                tarScale = new Vector3(2f, 2f, 2f);
            }

            // Set the scale for the target objectm, multiplied by the original scale
            target.localScale = tarScale;
        }
    }
    public void Reset()
    {
        // Reactivate physics for the target object
               
                //targetObject.layer = 10;
                target = null;
                targetObject = null;
                offsetFactor = 0f;
                //target.GetComponent<Rigidbody>().useGravity = true;
 
                // Set our target variable to null
                
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(target != null)
        {
            Gizmos.DrawWireCube(target.position, new Vector3(target.localScale.x/scale.x, target.localScale.y/scale.y, target.localScale.z/scale.z));
        }
        
       // Gizmos.DrawWireSphere(target.position, target.localScale.x/scale.x);
    }
}
