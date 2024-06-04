using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCaveModularSystem : MonoBehaviour
{

    public Vector3 moveDirection;
    public float moveSpeed;

    public int startingCount;

    public List<Transform> iceCaveParts;
    public Vector3 offset;
    public GameObject[] iceCavePrefabs;

    public float lifetime;
    float currentLife;

    // Start is called before the first frame update
    void Start()
    {
        iceCaveParts = new List<Transform>();
        for (int i = 0; i < startingCount; i++)
        {
            GenerateNewSection();
        }
        currentLife = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        currentLife -= Time.deltaTime;
        if (currentLife <= 0) 
        {
            GenerateNewSection();
            RemoveOldest();
            currentLife = lifetime;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void GenerateNewSection() 
    {
        int randomIndex = Random.Range(0, iceCavePrefabs.Length);
        GameObject tempPart = Instantiate(iceCavePrefabs[randomIndex], this.transform);
        Transform tempPartTransform = tempPart.transform;
        if (iceCaveParts.Count > 0)
        {
            tempPartTransform.localPosition = iceCaveParts[iceCaveParts.Count - 1].localPosition + offset;
        }
        iceCaveParts.Add(tempPartTransform);
    }

    void RemoveOldest() 
    {
        GameObject oldestObj = iceCaveParts[0].gameObject;
        iceCaveParts.RemoveAt(0);
        Destroy(oldestObj);
    }
}
