using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionPickup : MonoBehaviour
{
    public LocalGameManager localGameManager;

    public Conditional conditional;

    public string tagToUse;
    public bool destroyOnPickup;
    private bool pickedUp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedUp)
        {
            if (other.gameObject.tag == tag)
            {
                for (int i = 0; i < localGameManager.mainGameManager.loadedLocalManagers.Count; i++)
                {
                    for (int j = 0; j < localGameManager.mainGameManager.loadedLocalManagers[i].transitionPoints.Count; j++)
                    {
                        conditional.SetConditionValue(localGameManager.mainGameManager.loadedLocalManagers[i].transitionPoints[j].conditions);
                    }
                }

                pickedUp = true;

                if (destroyOnPickup)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
