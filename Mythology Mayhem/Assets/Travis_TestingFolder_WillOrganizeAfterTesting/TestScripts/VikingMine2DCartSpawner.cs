using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingMine2DCartSpawner : MonoBehaviour
{
    public Transform frontTrackSpawn;
    public Transform backTrackSpawn;

    public int frontOrderLayer;
    public int backOrderLayer;

    public GameObject coalCartPrefab;
    public GameObject goldCartPrefab;
    public GameObject gemCartPrefab;

    public float minTime;
    public float maxTime;

    public float currentNextTimeFront;
    public float currentNextTimeBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNextTimeBack > 0) 
        {
            currentNextTimeBack -= Time.deltaTime;
            if (currentNextTimeBack <= 0) 
            {
                int which = (int)Random.Range(0, 3);

                switch (which) 
                {
                    case 0:
                        SpawnCart(coalCartPrefab, backTrackSpawn, false);
                        break;
                    case 1:
                        SpawnCart(goldCartPrefab, backTrackSpawn, false);
                        break;
                    case 2:
                        SpawnCart(gemCartPrefab, backTrackSpawn, false);
                        break;
                }
                currentNextTimeBack = Random.Range(minTime, maxTime);
            }
        }

        if (currentNextTimeFront > 0)
        {
            currentNextTimeFront -= Time.deltaTime;
            if (currentNextTimeFront <= 0)
            {
                int which = (int)Random.Range(0, 3);

                switch (which)
                {
                    case 0:
                        SpawnCart(coalCartPrefab, frontTrackSpawn, true);
                        break;
                    case 1:
                        SpawnCart(goldCartPrefab, frontTrackSpawn, true);
                        break;
                    case 2:
                        SpawnCart(gemCartPrefab, frontTrackSpawn, true);
                        break;
                }

                currentNextTimeFront = Random.Range(minTime, maxTime);
            }
        }
    }

    void SpawnCart(GameObject preab, Transform trackSpawn, bool front) 
    {
        GameObject obj = Instantiate(preab, trackSpawn.position, trackSpawn.rotation, trackSpawn);
        VikingMine2DCart cartScript = obj.GetComponent<VikingMine2DCart>();
        if (cartScript != null)
        {
            if (front) 
            {
                cartScript.speed = cartScript.speed * -1;
                cartScript.rend.sortingOrder = frontOrderLayer;
            }
            else 
            {
                cartScript.rend.sortingOrder = backOrderLayer;
            }

            cartScript.parentTrack = trackSpawn;
        }
    }

}
