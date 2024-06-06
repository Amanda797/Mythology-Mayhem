using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCaveModularSystem : MonoBehaviour
{

    public Vector3 movement;
    public bool debugChangeMovement;
    Vector3 currentMovement;
    public int startingCount;

    public List<IceCaveSectionScript> iceCaveParts;
    public Vector3 offset;
    public GameObject[] iceCavePrefabs;

    public float lifetime;
    float currentLife;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMovementSpeed(movement);

        iceCaveParts = new List<IceCaveSectionScript>();
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
            currentLife = lifetime;
        }

        if (debugChangeMovement) 
        {
            ChangeMovementSpeed(movement);
            debugChangeMovement = false;
        }
    }

    void GenerateNewSection() 
    {
        int randomIndex = Random.Range(0, iceCavePrefabs.Length);
        GameObject tempPart = Instantiate(iceCavePrefabs[randomIndex], this.transform);
        IceCaveSectionScript tempSectionScript = tempPart.GetComponent<IceCaveSectionScript>();
        if (tempSectionScript != null)
        {
            tempSectionScript.movement = currentMovement;

            if (iceCaveParts.Count > 0)
            {
                tempSectionScript.transform.localPosition = iceCaveParts[iceCaveParts.Count - 1].transform.localPosition + offset;
            }
            iceCaveParts.Add(tempSectionScript);
        }
    }

    public void ChangeMovementSpeed(Vector3 newMovement) 
    {
        foreach (IceCaveSectionScript section in iceCaveParts) 
        {
            section.movement = newMovement;
        }
        currentMovement = newMovement;
    }
}
