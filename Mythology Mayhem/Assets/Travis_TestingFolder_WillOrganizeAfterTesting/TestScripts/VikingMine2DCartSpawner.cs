using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingMine2DCartSpawner : MonoBehaviour
{
    public Transform frontTrackSpawn;
    public Transform backTrackSpawn;

    public int frontOrderLayer;
    public int backOrderLayer;

    public int currentFront;
    public int currentBack;

    public GameObject coalCartPrefab;
    public GameObject goldCartPrefab;
    public GameObject[] gemCartPrefab;

    public List<CartType> pattern;

    public float minTime;
    public float maxTime;

    public float currentNextTimeFront;
    public float currentNextTimeBack;

    public enum CartType 
    {
        Coal,
        Gold,
        Gem,
        Gem2,
        Gem3,
        Gem4
    }
    // Start is called before the first frame update
    void Start()
    {
        currentFront = 0;
        currentBack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNextTimeBack > 0) 
        {
            currentNextTimeBack -= Time.deltaTime;
            if (currentNextTimeBack <= 0) 
            {
                switch (pattern[currentBack]) 
                {
                    case CartType.Coal:
                        SpawnCart(coalCartPrefab, backTrackSpawn, false);
                        break;
                    case CartType.Gold:
                        SpawnCart(goldCartPrefab, backTrackSpawn, false);
                        break;
                    case CartType.Gem:
                        SpawnCart(gemCartPrefab[0], backTrackSpawn, false);
                        break;
                    case CartType.Gem2:
                        SpawnCart(gemCartPrefab[1], backTrackSpawn, false);
                        break;
                    case CartType.Gem3:
                        SpawnCart(gemCartPrefab[2], backTrackSpawn, false);
                        break;
                    case CartType.Gem4:
                        SpawnCart(gemCartPrefab[3], backTrackSpawn, false);
                        break;
                }

                currentBack++;
                if (currentBack >= pattern.Count) 
                {
                    currentBack = 0;
                }

                currentNextTimeBack = Random.Range(minTime, maxTime);
            }
        }

        if (currentNextTimeFront > 0)
        {
            currentNextTimeFront -= Time.deltaTime;
            if (currentNextTimeFront <= 0)
            {
                switch (pattern[currentFront])
                {
                    case CartType.Coal:
                        SpawnCart(coalCartPrefab, frontTrackSpawn, true);
                        break;
                    case CartType.Gold:
                        SpawnCart(goldCartPrefab, frontTrackSpawn, true);
                        break;
                    case CartType.Gem:
                        SpawnCart(gemCartPrefab[0], frontTrackSpawn, true);
                        break;
                    case CartType.Gem2:
                        SpawnCart(gemCartPrefab[1], frontTrackSpawn, true);
                        break;
                    case CartType.Gem3:
                        SpawnCart(gemCartPrefab[2], frontTrackSpawn, true);
                        break;
                    case CartType.Gem4:
                        SpawnCart(gemCartPrefab[3], frontTrackSpawn, true);
                        break;
                }

                currentFront++;
                if (currentFront >= pattern.Count)
                {
                    currentFront = 0;
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
