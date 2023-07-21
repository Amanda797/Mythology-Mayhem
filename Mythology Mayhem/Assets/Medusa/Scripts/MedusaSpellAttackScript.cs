using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaSpellAttackScript : MonoBehaviour
{

    public float speed;

    public GameObject damageSphere;
    public Light lightSource;

    public float startScale;
    public float endScale;

    public float startLightRange;
    public float endLightRange;

    public float timeWhenHit;
    public float timeToScale;
    public float timeReachedFullScale;
    public bool fullScale;
    public float timeAtFullScale;


    public bool stopMovement;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        damageSphere.SetActive(false);
        fullScale = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null && !stopMovement) 
        {
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }
        if (stopMovement)
        {
            float scaleAmount = (Time.time - timeWhenHit) / timeToScale;
            if (scaleAmount >= 1 && !fullScale)
            {
                scaleAmount = 1;
                fullScale = true;
                timeReachedFullScale = Time.time;
            }

            float finalScaleAmount = Mathf.Lerp(startScale, endScale, scaleAmount);
            damageSphere.transform.localScale = new Vector3(finalScaleAmount, finalScaleAmount, finalScaleAmount);
            float finalLightRangeAmount = Mathf.Lerp(startLightRange, endLightRange, scaleAmount);
            lightSource.range = finalLightRangeAmount;

            if (Time.time - timeReachedFullScale >= timeAtFullScale && fullScale)
            {
                Destroy(gameObject);
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ground")
        {
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                stopMovement = true;
                damageSphere.transform.localScale = new Vector3(startScale, startScale, startScale);
                damageSphere.SetActive(true);
                timeWhenHit = Time.time;
            }
        }
    }
}
